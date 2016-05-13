
using PinkBus.OfflineCustomer.Common;
using PinkBus.OfflineCustomer.Entity;
using PinkBus.OfflineCustomer.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.OfflineCustomer.DAL
{
    public class OfflineCustomerHelper
    {
        public static int ExecuteNonQuerySql(string sql)
        {
            int res = 0;
            OperationSqlite(e => res = e.ExecuteNonQuery(sql));
            return res;
        }

        public static int UploadRowCount(Guid eventKey)
        {
            string sqlCus = "select  count(*) from PB_Customer c  where c.EventKey='" + eventKey + "' and c.State=1 ";//and c.SyncTimes<3
            DataTable cusDT = new DataTable();
            OperationSqlite(e => cusDT = e.Select(sqlCus));

            int cusCount = int.Parse(cusDT.Rows[0][0].ToString());
            return cusCount;
        }

        public static void OperationSqlite(Action<SQLiteHelper> action)
        {
            using (SQLiteConnection conn = new SQLiteConnection(AppSetting.DataSource))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();
                    SQLiteHelper sh = new SQLiteHelper(cmd);
                    try
                    {
                        sh.BeginTransaction();
                        action(sh);
                        //sh.Insert(TName, dic);
                        sh.Commit();
                    }
                    catch (Exception ex)
                    {
                        sh.Rollback();
                    }
                    conn.Close();
                }
            }
        }

        public static void InsertEvent(BackgroundWorker worker, CheckTokenResponse res)
        {
            string TimeNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            using (SQLiteConnection conn = new SQLiteConnection(AppSetting.DataSource))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();
                    Event e = res.Event;
                    SQLiteHelper sqlLiteHelper = new SQLiteHelper(cmd);
                    try
                    {
                        sqlLiteHelper.BeginTransaction();
                        #region  insert event
                        var dic = new Dictionary<string, object>();
                        dic["EventKey"] = e.EventKey.ToString().ToLower();
                        dic["EventLocation"] = e.EventLocation;
                        dic["EventTitle"] = e.EventTitle;
                        dic["EventStartDate"] = e.EventStartDate;
                        dic["EventEndDate"] = e.EventEndDate;
                        dic["CheckInStartDate"] = e.CheckinStartDate;
                        dic["CheckInEndDate"] = e.CheckinEndDate;
                        dic["ApplyTicketStartDate"] = e.ApplyTicketStartDate;
                        dic["ApplyTicketEndDate"] = e.ApplyTicketEndDate;
                        dic["InvitationEndDate"] = e.InvitationEndDate;
                        dic["IsUpload"] = e.IsUpload;
                        dic["DownloadToken"] = e.DownloadToken;
                        dic["UploadToken"] = e.UploadToken;
                        dic["BCImport"] = e.BCImport;
                        dic["VolunteerImport"] = e.VolunteerImport;
                        dic["VipImport"] = e.VipImport;
                        dic["CreatedDate"] = e.CreatedDate;
                        dic["CreatedBy"] = e.CreatedBy;
                        dic["UpdatedDate"] = e.UpdatedDate;
                        dic["UpdatedBy"] = e.UpdatedBy;
                        dic["DownloadDate"] = TimeNow;

                        sqlLiteHelper.Insert("PB_Event", dic);
                        #endregion

                        sqlLiteHelper.Execute("insert into SyncStatusLog values ('" + e.EventKey.ToString().ToLower() + "','D',1,0,0,0,0,0,0 )");
                        // updateSyncStatusLog(sql);
                        worker.ReportProgress(8);
                        sqlLiteHelper.Commit();
                    }
                    catch (Exception ex)
                    {
                        sqlLiteHelper.Rollback();
                        throw ex;
                    }

                    conn.Close();
                }
            }
        }

        public static void InsertSessions(BackgroundWorker worker, CheckTokenResponse res)
        {
            string TimeNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            using (SQLiteConnection conn = new SQLiteConnection(AppSetting.DataSource))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();
                    Event e = res.Event;
                    SQLiteHelper sqlLiteHelper = new SQLiteHelper(cmd);

                    try
                    {
                        sqlLiteHelper.BeginTransaction();
                        #region insert sessions
                        foreach (EventSession s in res.EventSessions)
                        {
                            var dics = new Dictionary<string, object>();
                            dics["SessionKey"] = s.SessionKey.ToString();
                            dics["Eventkey"] = s.Eventkey.ToString().ToLower();
                            dics["DisplayOrder"] = s.DisplayOrder;
                            dics["SessionStartDate"] = s.SessionStartDate;
                            dics["SessionEndDate"] = s.SessionEndDate;
                            dics["CanApply"] = s.CanApply;
                            dics["TicketOut"] = s.TicketOut;
                            dics["VIPTicketQuantity"] = s.VIPTicketQuantity;
                            dics["NormalTicketQuantity"] = s.NormalTicketQuantity;

                            dics["CreatedDate"] = s.CreatedDate;
                            dics["CreatedBy"] = s.CreatedBy;
                            dics["UpdatedDate"] = s.UpdatedDate;
                            dics["UpdatedBy"] = s.UpdatedBy;
                            dics["DownloadDate"] = TimeNow;

                            sqlLiteHelper.Insert("PB_EventSession", dics);

                        }

                        #endregion
                        // updateSyncStatusLog(sql);
                        worker.ReportProgress(10);
                        string sql = "update SyncStatusLog set Session=1,Complete=1 where EventKey='" + e.EventKey.ToString().ToLower() + "' and SyncType='D'";
                        sqlLiteHelper.Execute(sql);
                        sqlLiteHelper.Commit();
                    }
                    catch (Exception ex)
                    {
                        sqlLiteHelper.Rollback();
                    }
                    conn.Close();
                }
            }
        }

        public static void InsertConsultants(BackgroundWorker worker, List<Consultant> consultants, Guid EventKey)
        {
            string TimeNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            using (SQLiteConnection conn = new SQLiteConnection(AppSetting.DataSource))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();
                    SQLiteHelper sh = new SQLiteHelper(cmd);
                    try
                    {
                        sh.BeginTransaction();
                        for (int i = 0; i < consultants.Count; i++)
                        {
                            var e = consultants[i];
                            #region  insert event
                            var dic = new Dictionary<string, object>();
                            dic["MappingKey"] = e.MappingKey.ToString();
                            dic["EventKey"] = e.EventKey.ToString().ToLower();
                            dic["UserType"] = e.UserType;
                            dic["NormalTicketQuantity"] = e.NormalTicketQuantity;
                            dic["VIPTicketQuantity"] = e.VIPTicketQuantity;
                            dic["NormalTicketSettingQuantity"] = e.NormalTicketSettingQuantity;
                            dic["VIPTicketSettingQuantity"] = e.VIPTicketSettingQuantity;
                            dic["ContactId"] = e.ContactId;
                            dic["DirectSellerId"] = e.DirectSellerId;
                            dic["FirstName"] = e.FirstName;
                            dic["LastName"] = e.LastName;
                            dic["PhoneNumber"] = e.PhoneNumber;
                            dic["Level"] = e.Level;
                            dic["ResidenceID"] = e.ResidenceID;

                            dic["Province"] = e.Province;
                            dic["City"] = e.City;
                            dic["OECountyId"] = e.OECountyId;
                            dic["CountyName"] = e.CountyName;
                            dic["Status"] = e.Status;
                            dic["IsConfirmed"] = e.IsConfirmed;

                            dic["CreatedDate"] = e.CreatedDate;
                            dic["CreatedBy"] = e.CreatedBy;
                            dic["UpdatedDate"] = e.UpdatedDate;
                            dic["UpdatedBy"] = e.UpdatedBy;
                            dic["DownloadDate"] = TimeNow;
                            dic["ChangeStatus"] = 0;
                            sh.Insert("PB_Event_Consultant", dic);
                            #endregion
                            //Thread.Sleep(100);
                            //updateSyncStatusLog(sql);
                            int per = int.Parse(Math.Round((i * 1.00 / consultants.Count * 1.00) * 25.00 + 15, 0).ToString());
                            worker.ReportProgress(per);
                        }
                        string sql = "update SyncStatusLog set Consultant=1 where EventKey='" + EventKey.ToString().ToLower() + "' and SyncType='D'";
                        sh.Execute(sql);

                        sh.Commit();

                    }
                    catch (Exception ex)
                    {
                        sh.Rollback();
                    }
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Get upload customer count
        /// </summary>
        /// <param name="eventKey"></param>
        /// <param name="conCount"></param>
        /// <param name="cusCount"></param>
        /// <param name="ticCount"></param>
        /// <param name="volCount"></param>
        /// <returns></returns>
        public static int GetUploadCustomerCount(Guid eventKey)
        {
            string sqlCus = "select  count(*) from PB_Customer WHERE EventKey='" + eventKey + "' and c.state=1 ";
            DataTable cusDT = new DataTable();
            OperationSqlite(e => cusDT = e.Select(sqlCus));
            int cusCount = int.Parse(cusDT.Rows[0][0].ToString());
            return cusCount;
        }

        //public static void InsertTickets(BackgroundWorker worker, List<Ticket> Tickets, Guid EventKey)
        //{
        //    string TimeNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        //    using (SQLiteConnection conn = new SQLiteConnection(AppSetting.DataSource))
        //    {
        //        using (SQLiteCommand cmd = new SQLiteCommand())
        //        {
        //            cmd.Connection = conn;
        //            conn.Open();

        //            SQLiteHelper sh = new SQLiteHelper(cmd);
        //            try
        //            {
        //                sh.BeginTransaction();
        //                for (int i = 0; i < Tickets.Count; i++)
        //                {
        //                    var e = Tickets[i];
        //                    #region  insert event
        //                    var dic = new Dictionary<string, object>();
        //                    dic["TicketKey"] = e.TicketKey.ToString();
        //                    dic["MappingKey"] = e.MappingKey.ToString();
        //                    dic["EventKey"] = e.EventKey.ToString().ToLower();
        //                    dic["SessionKey"] = e.SessionKey.ToString();
        //                    dic["CustomerKey"] = e.CustomerKey.ToString();
        //                    dic["TicketType"] = e.TicketType;
        //                    dic["TicketFrom"] = e.TicketFrom;
        //                    dic["TicketStatus"] = e.TicketStatus;
        //                    dic["SMSToken"] = e.SMSToken;
        //                    dic["SessionStartDate"] = e.SessionStartDate;
        //                    dic["SessionEndDate"] = e.SessionEndDate;
        //                    dic["CheckinDate"] = e.CheckinDate;

        //                    dic["CreatedDate"] = e.CreatedDate;
        //                    dic["CreatedBy"] = e.CreatedBy;
        //                    dic["UpdatedDate"] = e.UpdatedDate;
        //                    dic["UpdatedBy"] = e.UpdatedBy;
        //                    dic["DownloadDate"] = TimeNow;
        //                    dic["ChangeStatus"] = 0;
        //                    sh.Insert("PB_Ticket", dic);
        //                    #endregion

        //                    //updateSyncStatusLog(sql);
        //                    int per = int.Parse(Math.Round((i * 1.00 / Tickets.Count * 1.00) * 25.00 + 45, 0).ToString());
        //                    worker.ReportProgress(per);
        //                }
        //                string sql = "update SyncStatusLog set Ticket=1 where EventKey='" + EventKey.ToString().ToLower() + "' and SyncType='D'";
        //                sh.Execute(sql);
        //                sh.Commit();
        //            }
        //            catch (Exception ex)
        //            {
        //                sh.Rollback();
        //            }
        //            conn.Close();
        //        }
        //    }
        //}

        //public static void InsertCustomerByBackgroundWorker(BackgroundWorker worker, List<Customer> Customers, Guid EventKey)
        //{
        //    string TimeNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        //    using (SQLiteConnection conn = new SQLiteConnection(AppSetting.DataSource))
        //    {
        //        using (SQLiteCommand cmd = new SQLiteCommand())
        //        {
        //            cmd.Connection = conn;
        //            conn.Open();

        //            SQLiteHelper sh = new SQLiteHelper(cmd);
        //            try
        //            {
        //                sh.BeginTransaction();
        //                for (int i = 0; i < Customers.Count; i++)
        //                {
        //                    var e = Customers[i];
        //                    #region  insert event
        //                    var dic = new Dictionary<string, object>();
        //                    dic["CustomerKey"] = e.CustomerKey.ToString();
        //                    dic["CustomerName"] = e.CustomerName;
        //                    dic["CustomerPhone"] = e.CustomerPhone;

        //                    dic["AgeRange"] = e.AgeRange;
        //                    dic["IsHearMaryKay"] = e.IsHearMaryKay;
        //                    dic["InterestingTopic"] = e.InterestingTopic;
        //                    dic["CustomerType"] = e.CustomerType;
        //                    dic["Career"] = e.Career;
        //                    dic["IsJoinEvent"] = e.IsJoinEvent;

        //                    dic["CustomerResponse"] = e.CustomerResponse;
        //                    dic["UsedProduct"] = e.UsedProduct;
        //                    dic["BestContactDate"] = e.BestContactDate;
        //                    dic["AdviceContactDate"] = e.AdviceContactDate;
        //                    dic["Province"] = e.Province;
        //                    dic["City"] = e.City;
        //                    dic["County"] = e.County;
        //                    dic["State"] = e.State;
        //                    dic["CreatedDate"] = DateTime.Now;
        //                    dic["CreatedBy"] = e.CreatedBy;
        //                    dic["UpdatedDate"] = DateTime.Now;
        //                    dic["UpdatedBy"] = e.UpdatedBy;

        //                    sh.Insert("PB_Customer", dic);
        //                    #endregion

        //                    int per = int.Parse(Math.Round((i * 1.00 / Customers.Count * 1.00) * 20.00 + 75, 0).ToString());
        //                    worker.ReportProgress(per);
        //                }
        //                sh.Commit();
        //                string sql = "update SyncStatusLog set Customer=1 where EventKey='" + EventKey.ToString().ToLower() + "' and SyncType='D'";
        //                UpdateSyncStatusLog(sql);

        //                worker.ReportProgress(95);
        //            }
        //            catch (Exception ex)
        //            {
        //                sh.Rollback();
        //            }
        //            conn.Close();
        //        }
        //    }
        //}

        public static void UpdateCustomersByBackgroundWorker(BackgroundWorker worker, List<Customer> customers,Guid EventKey)
        {
            string TimeNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            using (SQLiteConnection conn = new SQLiteConnection(AppSetting.DataSource))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();

                    SQLiteHelper sh = new SQLiteHelper(cmd);
                    try
                    {
                        sh.BeginTransaction();
                        for (int i = 0; i < customers.Count; i++)
                        {
                            var customer = customers[i];
                            #region  insert event
                            var dic = new Dictionary<string, object>();
                            dic["DirectSellerId"] = customer.DirectSellerId;
                            dic["State"] = (int)DataState.Updated;
                            dic["UpdatedDate"] = DateTime.Now;
                            dic["UpdatedBy"] = "System";

                            sh.Update("PB_Customer", dic, "CustomerKey", customer.CustomerKey.ToString());
                            #endregion

                            int per = int.Parse(Math.Round((i * 1.00 / customers.Count * 1.00) * 20.00 + 60, 0).ToString());
                            worker.ReportProgress(per);
                        }

                        if (customers.Count> 0)
                        {
                            string sqlU = "update SyncStatusLog set Customer=1,Complete=1,SyncType='D' where Eventkey='" + EventKey.ToString().ToLower() + "' ";
                            sh.Execute(sqlU);
                        }
                        sh.Commit();


                        worker.ReportProgress(95);
                    }
                    catch (Exception ex)
                    {
                        sh.Rollback();
                    }
                    conn.Close();
                }
            }
        }
        public static void InsertCustomer(Customer customer, Guid EventKey)
        {
            string TimeNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            using (SQLiteConnection conn = new SQLiteConnection(AppSetting.DataSource))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();

                    SQLiteHelper sh = new SQLiteHelper(cmd);
                    try
                    {
                        sh.BeginTransaction();

                        #region  insert customer
                        var dic = new Dictionary<string, object>();
                        dic["CustomerKey"] = customer.CustomerKey.ToString();
                        dic["EventKey"] = customer.EventKey;
                        dic["CustomerName"] = customer.CustomerName;
                        dic["ContactInfo"] = customer.ContactInfo;
                        dic["ContactType"] = customer.ContactType;

                        dic["AgeRange"] = customer.AgeRange;
                        dic["IsHearMaryKay"] = customer.IsHearMaryKay;
                        dic["InterestingTopic"] = customer.InterestingTopic;
                        dic["CustomerType"] = customer.CustomerType;
                        dic["Career"] = customer.Career;
                        dic["IsJoinEvent"] = customer.IsJoinEvent;

                        dic["CustomerResponse"] = customer.CustomerResponse;
                        dic["UsedProduct"] = customer.UsedProduct;
                        dic["BestContactDate"] = customer.BestContactDate;
                        dic["AdviceContactDate"] = customer.AdviceContactDate;
                        dic["Province"] = customer.Province;
                        dic["City"] = customer.City;
                        dic["County"] = customer.County;
                        dic["State"] = (int)DataState.Added;
                        dic["CreatedDate"] = DateTime.Now.ToString();
                        dic["CreatedBy"] = customer.CreatedBy;
                        dic["UpdatedDate"] = DateTime.Now.ToString();
                        dic["UpdatedBy"] = customer.UpdatedBy;

                        sh.Insert("PB_Customer", dic);
                        #endregion

                        string sqlU = "update SyncStatusLog set Customer=1,Complete=1,SyncType='D' where Eventkey='" + EventKey.ToString().ToLower() + "' ";
                        sh.Execute(sqlU);
                        sh.Commit();
                        
                    }
                    catch (Exception ex)
                    {
                        sh.Rollback();
                    }
                    conn.Close();
                }
            }
        }

        public static List<Event> QueryEvents(string where = null)
        {
            string sql = "select * from PB_Event ";
            if (!string.IsNullOrEmpty(where)) sql += where;

            sql += " order by DownloadDate desc";
            List<Event> eventList = new List<Event>();
            OperationSqlite(e => eventList = e.Selects<Event>(sql));

            return eventList;

        }

        public static List<Province> QueryProvince()
        {
            string sql = "select Id,ProvinceId,Province as ProvinceName from PB_aProvince";
            List<Province> provinceList = new List<Entity.Province>();
            OperationSqlite(e => provinceList = e.Selects<Province>(sql));
            return provinceList;
        }

        public static List<City> QueryCity(string id)
        {
            string sql = "select Id,CityId,City as CityName,Father from PB_aCity where father ='" + id + "'";
            List<City> cityList = new List<Entity.City>();
            OperationSqlite(e => cityList = e.Selects<City>(sql));

            return cityList;
        }

        public static List<County> QueryCounty(string id)
        {
            string sql = "select Id,CountyId,County as CountyName,Father from PB_aCounty where father='" + id + "'";
            List<County> countyList = new List<Entity.County>();
            OperationSqlite(e => countyList = e.Selects<County>(sql));

            return countyList;
        }

        public static List<ConsultantsWhichHasUnUseTicket> QueryConsultantsWhichHasUnUseTickets(Guid EventKey)
        {
            string sql = "select t.MappingKey,c.EventKey,c.ContactId,c.DirectSellerId,c.UserType,c.LastName,c.FirstName, count(*) as Count from PB_Ticket t ";
            sql += " join PB_Event_Consultant c on t.MappingKey=c.MappingKey";
            sql += " where  t.EventKey='{0}' and (t.TicketStatus=0 or t.TicketStatus=1) ";
            sql += " group by t.MappingKey ";
            sql += "  having count>0 ";

            sql = string.Format(sql, EventKey.ToString().ToLower());

            List<ConsultantsWhichHasUnUseTicket> Consultants = new List<ConsultantsWhichHasUnUseTicket>();
            OperationSqlite(e => Consultants = e.Selects<ConsultantsWhichHasUnUseTicket>(sql));

            return Consultants;
        }


        public static void UpdateSyncStatusLog(string sql)
        {
            OperationSqlite(e => e.Execute(sql));
        }

        public static SyncStatusLog GetSyncStatusLog(Guid EventKey, SyncType SyncType)
        {
            string sql = "select * from SyncStatusLog where EventKey='" + EventKey.ToString().ToLower() + "' and SyncType='" + SyncType + "'";
            SyncStatusLog row = new SyncStatusLog();
            OperationSqlite(e => row = e.Select<SyncStatusLog>(sql));
            return row;
        }

        public static List<SyncStatusLog> QuerySyncStatusLogs(string where = null)
        {
            string sql = "select * from SyncStatusLog ";
            sql += where;
            List<SyncStatusLog> rows = new List<SyncStatusLog>();
            OperationSqlite(e => rows = e.Selects<SyncStatusLog>(sql));
            return rows;
        }

        public static List<Customer> QueryCustomers(string EventKey)
        {
            string sql = @"select CustomerKey,CustomerName,ContactInfo,ContactType,CustomerResponse,CustomerType,IsHearMaryKay,
            IsJoinEvent,AgeRange,Career,InterestingTopic,UsedProduct,AdviceContactDate,BestContactDate
            ,Province,City,County,DirectSellerId
             from PB_Customer where EventKey='" + EventKey.ToString().ToLower() + "' order by CreatedDate desc ";
            List<Customer> rows = new List<Customer>();

            OperationSqlite(e => rows = e.Selects<Customer>(sql));
            return rows;
        }

        //删除顾客
        public static void DeleteCustomer(string customerKey)
        {
            string sql = @"delete  from PB_Customer where CustomerKey ='"
                            + customerKey.ToString().ToLower() + "'";
            OperationSqlite(e=> e.Execute(sql));
        }

        //        public static List<Customer> SearchCustomer(string EventKey，string searrch)
        //        {
        //            string sql = @"select CustomerKey,CustomerName,CustomerPhone,CustomerResponse,CustomerType,IsHearMaryKay,
        //            IsJoinEvent,AgeRange,Career,InterestingTopic,UsedProduct,AdviceContactDate,BestContactDate
        //            ,Province,City,County,DirectSellerId
        //             from PB_Customer where EventKey='" + EventKey.ToString().ToLower() + "' order by CreatedDate desc ";
        //            List<Customer> rows = new List<Customer>();

        //            OperationSqlite(e => rows = e.Selects<Customer>(sql));
        //            return rows;
        //        }
        /// <summary>
        /// query customer list which need to upload to server
        /// </summary>
        /// <param name="EventKey"></param>
        /// <returns></returns>
        public static List<Customer> QueryUploadCustomers(string EventKey)
        {
            string sql = @"select CustomerKey,CustomerName,ContactType,ContactInfo,CustomerResponse,CustomerType,IsHearMaryKay,
            IsJoinEvent,AgeRange,Career,InterestingTopic,UsedProduct,AdviceContactDate,BestContactDate
            ,Province,City,County,DirectSellerId,EventKey,State
             from PB_Customer where EventKey='" + EventKey.ToString().ToLower() + "' and State in (0,1)";
            List<Customer> customers = new List<Customer>();

            OperationSqlite(e => customers = e.Selects<Customer>(sql));
            return customers;
        }

        public static List<Customer> QueryUnAssignedCustomers(string EventKey)
        {
            //            string sql = @"select CustomerKey,CustomerName,CustomerPhone,CustomerResponse,CustomerType,IsHearMaryKay,
            //            IsJoinEvent,AgeRange,Career,InterestingTopic,UsedProduct,AdviceContactDate,BestContactDate,Province,City,County
            //             from PB_Customer where DirectSellerId is null and EventKey='" + EventKey.ToString().ToLower() + "' ";
            string sql = @"select CustomerKey,CustomerName,ContactType,ContactInfo,CustomerType,Province,City,County
                         from PB_Customer where DirectSellerId is null and EventKey='" + EventKey.ToString().ToLower() + "' ";
            List<Customer> rows = new List<Customer>();
            //Dictionary<string, string> dic = new Dictionary<string, string>();
            //dic.Add("@EventKey", EventKey);

            OperationSqlite(e => rows = e.Selects<Customer>(sql));
            return rows;
        }

        public static int QueryUnAssignedCustomersQuantity(string EventKey)
        {
            //            string sql = @"select CustomerKey,CustomerName,CustomerPhone,CustomerResponse,CustomerType,IsHearMaryKay,
            //            IsJoinEvent,AgeRange,Career,InterestingTopic,UsedProduct,AdviceContactDate,BestContactDate,Province,City,County
            //             from PB_Customer where DirectSellerId is null and EventKey='" + EventKey.ToString().ToLower() + "' ";
            string sql = @"select count(*)
                         from PB_Customer where DirectSellerId is null and EventKey='" + EventKey.ToString().ToLower() + "' ";
            int quantity = 0;

            using (SQLiteConnection conn = new SQLiteConnection(AppSetting.DataSource))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();

                    SQLiteHelper sh = new SQLiteHelper(cmd);
                    quantity = sh.ExecuteScalar<int>(sql);
                }
            }
            return quantity;
        }

        public static int QueryCustomersQuantity(string EventKey)
        {
            string sql = @"select count(*)
                         from PB_Customer where EventKey='" + EventKey.ToString().ToLower() + "' ";
            int quantity = 0;
            using (SQLiteConnection conn = new SQLiteConnection(AppSetting.DataSource))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();

                    SQLiteHelper sh = new SQLiteHelper(cmd);
                    quantity = sh.ExecuteScalar<int>(sql);
                }
            }
            return quantity;
        }
        public static List<ConsultantTickets> QueryVolunteers(string EventKey)
        {
            string sql = "select * from PB_Event_Consultant c left join " +
                        "(select MappingKey,count(MappingKey) CheckinCount ,EventKey " +
                        "from PB_VolunteerCheckin  GROUP BY MappingKey) a " +
                        "on c.MappingKey=a.MappingKey " +
                        "where c.UserType=3 and c.EventKey='" + EventKey.ToString().ToLower() + "' ";
            List<ConsultantTickets> rows = new List<ConsultantTickets>();
            OperationSqlite(e => rows = e.Selects<ConsultantTickets>(sql));
            return rows;
        }

        public static List<object> QuerySellersContractId(Guid EventKey)
        {
            //string sql = "select ContactId from PB_Event_Consultant where EventKey='" + EventKey.ToString().ToLower() + "'";
            //List<object> rows = new List<object>();
            //OperationSqlite(e => rows = e.Select(sql).AsEnumerable().Select(c => c.ItemArray[0]).ToList());

            //return rows;
            return null;
        }


        public static Consultant GetConsultant(string MappingKey, Guid EventKey)
        {
            string sql = "select * from PB_Event_Consultant where MappingKey='" + MappingKey + "' and EventKey='" + EventKey + "'";
            Consultant C = new Consultant();
            OperationSqlite(e => C = e.Select<Consultant>(sql));

            return C;
        }


        public static CustomerInfo GetCustomer(string CustomerKey, Guid EventKey)
        {
            string sql = "select * from PB_Customer c join PB_Ticket t on c.CustomerKey=t.CustomerKey " +
                "join PB_Event_Consultant ec on t.MappingKey=ec.MappingKey " +
                "where c.CustomerKey='" + CustomerKey + "' and t.EventKey='" + EventKey + "'";
            CustomerInfo C = new CustomerInfo();
            OperationSqlite(e => C = e.Select<CustomerInfo>(sql));

            return C;
        }



        public static int CancelVolunter(string MappingKey)
        {
            string sql = "update PB_Event_Consultant set Status=1 ,ChangeStatus=1 where MappingKey='" + MappingKey + "' ";
            int C = 0;
            OperationSqlite(e => C = e.ExecuteNonQuery(sql));
            return C;
        }
    }
}
