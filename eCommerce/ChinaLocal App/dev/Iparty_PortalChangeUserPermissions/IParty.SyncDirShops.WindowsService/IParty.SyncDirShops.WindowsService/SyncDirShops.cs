using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IParty.SyncDirShops.WindowsService
{
    public class SyncDirShops
    {
        private Logger _logger = LogManager.GetCurrentClassLogger();
        int PageSize = 10000;
        public SyncDirShops()
        {
            //_logger.Info(ConnectionStrings.APBeautyContest);
            if (!int.TryParse(ConnectionStrings.PageCount, out PageSize))
                PageSize = 10000;
        }

        public  void SyncDirShopsTask()
        {
            int AllRetry = 3;
            while (AllRetry > 0)
            {
                try
                {
                    string DateNow = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss.ffff");
                    #region   SyncDirShop  
                    //IsAllReady   是否资料齐全
                    //ISBDL  是否签署授权经销商合同
                    string DirShopGet = "select ds.Recordid,ds.CID,'0' as ContactID,ShopType,Province,City,County,ShopAddress,ShopZipCode,Contact,ContactTel,FixedTel,ShopLicenseName,FirstCheckOutDate,'" + DateNow + "' as SyncDate from DirShops ds with (nolock) left join Dealers d with(nolock) on ds.CID=d.CID where ShopStatus=N'正常' and IsAllReady=1 and ISBDL=1 and d.LevelCode>=60 and (ShopType=N'普通' or ShopType=N'新形象'or ShopType=N'新时尚'or ShopType=N'新标准'or ShopType=N'标准' or ShopType=N'标准（基本）'or ShopType=N'标准（特别）')";
                    DataSet ds = SqlHelper.ExecuteDataset(ConnectionStrings.DirShops, CommandType.Text, DirShopGet);

                    if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];
                        int RowsCount = dt.Rows.Count;
                        int Page = (RowsCount / PageSize) + 1;
                        for (int i = 0; i < Page; i++)
                        {
                            Console.WriteLine("开始同步工作室第" + i+"页");
                            _logger.Info("start sync workshops" + i + "Page");
                            string cids = "";

                            int pageSize = i == Page - 1 ? RowsCount % PageSize : PageSize;
                            if (pageSize == 0) break;
                            DataTable pageDt = dt.AsEnumerable().Skip(i * PageSize).Take(pageSize).CopyToDataTable();

                            object[] list = pageDt.AsEnumerable().Select(x => x["CID"]).ToArray();

                            cids = "'" + string.Join("','", list) + "'";

                            string sql = "select ContactID,ConsultantID from ContactsLite.dbo.Consultants with(nolock) where ConsultantID in (" + cids + ")";
                            // DataSet PageContactID = SqlHelper.ExecuteDataset(ConnectionStrings.Community, CommandType.StoredProcedure, "IPartySP_SyncDirShopContactId", new SqlParameter[] { new SqlParameter("@Cids", sql) });
                            DataSet PageContactID2 = SqlHelper.ExecuteDataset(ConnectionStrings.Community, CommandType.Text, sql);
                            if (PageContactID2 != null && PageContactID2.Tables != null && PageContactID2.Tables.Count > 0)
                            {
                                DataTable dt2 = PageContactID2.Tables[0];

                                foreach (DataRow dr in pageDt.Rows)
                                {
                                    DataRow ContactID = dt2.AsEnumerable().FirstOrDefault(x => x["ConsultantID"].ToString() == dr["CID"].ToString());
                                    if (ContactID != null)
                                    {
                                        dr["ContactID"] = ContactID["ContactID"];
                                    }

                                }

                            }
                            int res = SqlHelper.DataTableToSQLServer(ConnectionStrings.Community, "IParty_DirShops_Temp", pageDt, _logger);
                            if (res > 0)
                            {
                                Console.WriteLine("插入临时表第" + i + "页数据成功--" + res);
                                _logger.Info("insert temp table workshop" + i + "page success--" + res);
                            }
                        }
                        int SyncRes = SqlHelper.ExecuteNonQuery(ConnectionStrings.Community, CommandType.StoredProcedure, "IPartySP_SyncDirShopData", new SqlParameter[] { new SqlParameter("@DateNow", DateTime.Now) });
                        if (SyncRes < 0)
                        {
                            Console.WriteLine("IPartySP_SyncDirShopData has somthing wrong and trytimes" + AllRetry);
                            _logger.Info("IPartySP_SyncDirShopData has somthing wrong and trytimes" + AllRetry);
                            _logger.Error("IPartySP_SyncDirShopData has somthing wrong and trytimes" + AllRetry);
                            Thread.Sleep(3000);
                            AllRetry--;
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("同步工作室成功" + SyncRes);
                            _logger.Info("sync workshop data success"  + SyncRes);
                          
                        }

                        //int res = SqlHelper.DataTableToSQLServer(ConnectionStrings.Community, "IParty_DirShops_Temp", dt, _logger);


                    }

                    #endregion

                    #region  SyncShopPartners
                    //IsAllReady   是否资料齐全
                    //ISBDL  是否签署授权经销商合同
                    string PartnersGet = "select p.Recordid,p.CID,'0' as ContactID,p.ShopRID,'" + DateNow + "' as SyncDate from Partners p with(nolock)  left join DirShops d with(nolock) on p.ShopRID=d.RecordID  left join Dealers dd with(nolock) on dd.CID=d.CID  where  ShopStatus=N'正常' and  IsAllReady=1 and  ISBDL=1 and   dd.LevelCode>=60 and  (ShopType=N'普通' or  ShopType=N'新形象'or  ShopType=N'新时尚'or ShopType=N'新标准' or ShopType=N'标准' or ShopType=N'标准（基本）'or ShopType=N'标准（特别）')";
                    DataSet PartnersDs = SqlHelper.ExecuteDataset(ConnectionStrings.DirShops, CommandType.Text, PartnersGet);

                    if (PartnersDs != null && PartnersDs.Tables != null && PartnersDs.Tables.Count > 0)
                    {
                        DataTable dt = PartnersDs.Tables[0];
                        int RowsCount = dt.Rows.Count;
                        int Page = (RowsCount / PageSize) + 1;
                        for (int i = 0; i < Page; i++)
                        {
                            Console.WriteLine("开始同步合作人第" + i + "页");
                            _logger.Info("sync partners on " + i + "page");
                            string cids = "";

                            int pageSize = i == Page - 1 ? RowsCount % PageSize : PageSize;
                            if (pageSize == 0) break;
                            DataTable pageDt = dt.AsEnumerable().Skip(i * PageSize).Take(pageSize).CopyToDataTable();

                            object[] list = pageDt.AsEnumerable().Select(x => x["CID"]).ToArray();

                            cids = "'" + string.Join("','", list) + "'";

                            string sql = "select ContactID,ConsultantID from ContactsLite.dbo.Consultants where ConsultantID in (" + cids + ")";
                            // DataSet PageContactID = SqlHelper.ExecuteDataset(ConnectionStrings.Community, CommandType.StoredProcedure, "IPartySP_SyncDirShopContactId", new SqlParameter[] { new SqlParameter("@Cids", sql) });
                            DataSet PageContactID2 = SqlHelper.ExecuteDataset(ConnectionStrings.Community, CommandType.Text, sql);
                            if (PageContactID2 != null && PageContactID2.Tables != null && PageContactID2.Tables.Count > 0)
                            {
                                DataTable dt2 = PageContactID2.Tables[0];

                                foreach (DataRow dr in pageDt.Rows)
                                {
                                    DataRow ContactID = dt2.AsEnumerable().FirstOrDefault(x => x["ConsultantID"].ToString() == dr["CID"].ToString());
                                    if (ContactID != null)
                                    {
                                        dr["ContactID"] = ContactID["ContactID"];
                                    }

                                }

                            }
                            int res = SqlHelper.DataTableToSQLServer(ConnectionStrings.Community, "IParty_ShopPartners_Temp", pageDt, _logger);
                            if (res > 0)
                            {
                                Console.WriteLine("插入合作人第" + i + "页数据成功--" + res);
                                _logger.Info("insert partners on" + i + "page success--" + res);
                               
                            }
                        }

                       int SyncRes = SqlHelper.ExecuteNonQuery(ConnectionStrings.Community, CommandType.StoredProcedure, "IPartySP_SyncShopPartners", new SqlParameter[] { new SqlParameter("@DateNow", DateTime.Now) });
                       if (SyncRes < 0)
                        {
                            Console.WriteLine("IPartySP_SyncDirShopData has somthing wrong and trytimes" + AllRetry);
                            _logger.Info("IPartySP_SyncDirShopData has somthing wrong and trytimes" + AllRetry);
                            _logger.Error("IPartySP_SyncDirShopData has somthing wrong and trytimes" + AllRetry);
                            Thread.Sleep(3000);
                            AllRetry--;
                            continue;
                        }
                        

                        Console.WriteLine("查询合作人--" + dt.Rows.Count);
                        _logger.Info("查询合作人--" + dt.Rows.Count);

                        //int res = SqlHelper.DataTableToSQLServer(ConnectionStrings.Community, "IParty_DirShops_Temp", dt, _logger);


                    }
                    #endregion


                    #region SyncLessee
                    int SyncLesseeRes = SqlHelper.ExecuteNonQuery(ConnectionStrings.Community, CommandType.StoredProcedure, "IPartySP_SyncLessee", new SqlParameter[] { new SqlParameter("@DateNow", DateTime.Now.Date) });
                    if (SyncLesseeRes < 0)
                    {
                        Console.WriteLine("IPartySP_SyncLessee has somthing wrong and trytimes" + AllRetry);
                        _logger.Info("IPartySP_SyncLessee has somthing wrong and trytimes" + AllRetry);
                        _logger.Error("IPartySP_SyncLessee has somthing wrong and trytimes" + AllRetry);
                        Thread.Sleep(3000);
                        AllRetry--;
                        continue;
                    }
                    Console.WriteLine("主承租人同步完毕" + SyncLesseeRes);
                    _logger.Info("主承租人同步完毕" + SyncLesseeRes);
                    #endregion

                }
                catch (Exception ex)
                {
                    Console.WriteLine(" has somthing wrong and trytimes" + AllRetry);
                    AllRetry--;
                    if (AllRetry == 0)
                    {
                        _logger.Error("SyncDirShopsTask" + ex.ToString());
                        break;
                    }
                    else
                    {
                        _logger.Info("SyncDirShopsTask" + ex.ToString());

                        Thread.Sleep(3000);

                        continue;
                    }
                }
                break;
            }
            _logger.Info(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffff") + "__all done");
        }

    }
}
