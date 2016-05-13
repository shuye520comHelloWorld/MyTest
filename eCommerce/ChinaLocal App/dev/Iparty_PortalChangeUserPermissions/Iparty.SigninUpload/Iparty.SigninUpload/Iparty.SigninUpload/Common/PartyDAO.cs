using Iparty.SigninUpload.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Iparty.SigninUpload.Common
{
    public class PartyDAO
    {
        public static List<DirInfo> checkDir(string DirId)
        {
            try
            {
                string dirsql = "select c.LastName+c.FirstName Name,p.AppliedContactID , e.EventStartDate,e.Title,p.PartyKey,i.DirectSellerID " +
                                "from iParty_PartyApplication p (nolock) " +
                                "left join iParty_Event e (nolock) on e.EventKey=p.EventKey " +
                                "left join ContactsLite.dbo.InternationalConsultants i (nolock) on p.AppliedContactID=i.ContactID " +
                                "left join ContactsLite.dbo.Consultants c (nolock) on p.AppliedContactID=c.ContactID " +
                                "where c.ConsultantStatus not like '%x%' and i.DirectSellerID=@dirid order by e.EventStartDate";
                DataSet ds = SqlHelper.ExecuteDataset(AppSetting.Community, CommandType.Text, dirsql, new SqlParameter("@dirid", DirId));
                // if (ds.Tables.Count > 0&&ds.Tables[0].Rows.Count>0)
                // {
                string json = JsonConvert.SerializeObject(ds.Tables[0]);
                return JsonConvert.DeserializeObject<List<DirInfo>>(json);
                //return ds.Tables[0].Rows[0]["name"].ToString();
                // }

                // return null;
            }
            catch (Exception ex)
            {
                LogHelper.DebugErr(ex.ToString());
                return new List<DirInfo>();
            }
        }


        public static List<PartyInfo> getParty(Guid PartyKey)
        {
            string sql = "select p.PartyKey, AppliedName as Name,i.DirectSellerID,e.Title,d.Province,d.City,d.County,d.ShopAddress,p.DisplayStartDate,p.DisplayEndDate,p.StartDate,p.EndDate,pa.PartyStartTime ActualStart,pa.PartyEndTime ActualEnd " +
                        "from iParty_PartyApplication p (nolock) " +
                        "left join ContactsLite.dbo.InternationalConsultants i (nolock) on p.AppliedContactID=i.ContactID  " +
                        "left join iParty_Event e  (nolock)  on p.EventKey=e.EventKey " +
                        "left join IParty_DirShops d (nolock)  on p.WorkshopId=d.Recordid " +
                        "left join Iparty_PartyActualTime pa (nolock)  on p.PartyKey=pa.PartyKey " +
                        "where p.PartyKey=@partykey";

            DataSet ds = SqlHelper.ExecuteDataset(AppSetting.Community, CommandType.Text, sql, new SqlParameter("@partykey", PartyKey));
            string json = JsonConvert.SerializeObject(ds.Tables[0]);
            return JsonConvert.DeserializeObject<List<PartyInfo>>(json);
        }


        public static int addActualTime(Guid PartyKey, DateTime startTime, DateTime EndTime)
        {

            string sql = "insert into Iparty_PartyActualTime (PartyKey,PartyStartTime,PartyEndTime) values(@partykey,@starttime,@endtime)";
            int res = SqlHelper.ExecuteNonQuery(AppSetting.Community, CommandType.Text, sql,
                new SqlParameter("@partykey", PartyKey), new SqlParameter("@starttime", startTime), new SqlParameter("@endtime", EndTime));
            return res;
        }

        public static List<Customer> getCustomer(Guid partykey, string PhoneNumber)
        {
            string sql = "select c.Name,c.PhoneNumber,i.Status,i.CheckInType,i.Reference,ii.DirectSellerID from iParty_Invitation i " +
                        "inner join IParty_Customer c (nolock)  on i.CustomerKey=c.CustomerKey " +
                        "left join ContactsLite.dbo.InternationalConsultants ii on i.ReferenceKey=ii.ContactID " +
                        "where i.PhoneNumber=@phone and partykey=@partykey and status !='03'" ;

            DataSet ds = SqlHelper.ExecuteDataset(AppSetting.Community, CommandType.Text, sql, new SqlParameter("@phone", PhoneNumber), new SqlParameter("@partykey", partykey));
            string json = JsonConvert.SerializeObject(ds.Tables[0]);
            return JsonConvert.DeserializeObject<List<Customer>>(json);
        }

        public static List<partyUnitee> getPartyUnite(Guid partykey)
        {
            string sql = "select p.PartyKey,c.UnitID as ParUnitId  ,u.UnitId " +
                        "from iParty_PartyApplication p  " +
                        "left join iParty_Unitee u on p.PartyKey=u.PartyKey " +
                        "left join ContactsLite.dbo.Consultants c on p.AppliedContactID=c.ContactID " +
                        "where p.PartyKey=@partykey";
            DataSet ds = SqlHelper.ExecuteDataset(AppSetting.Community, CommandType.Text, sql, new SqlParameter("@partykey", partykey));
            string json = JsonConvert.SerializeObject(ds.Tables[0]);
            return JsonConvert.DeserializeObject<List<partyUnitee>>(json);
        }

        public static List<Consultant> getConsultant(string PhoneNumber)
        {
            string sql = "select  c.LastName+c.FirstName Name,p.PhoneNumber,c.ConsultantLevelID Level,c.UnitID , "+
                        "(case when Recruiter is not null then re.DirectSellerID when Recruiter is null then di.DirectSellerID else null end ) RecruiterOrDirId " +
                        "from ContactsLite.dbo.PhoneNumbers p  "+
                        "join ContactsLite.dbo.Consultants c on p.ContactID=c.ContactID  "+
                        "left join (select cr.Consultant, cr.Recruiter,c.DirectSellerID from ContactsLite.dbo.Consultant_To_Recruiter cr (nolock) "+
			            "            inner join  ContactsLite.dbo.InternationalConsultants c (nolock) on cr.Recruiter=c.ContactID) re on p.ContactID=re.Consultant "+
                        "left join (select cd.Consultant, cd.Director,c.DirectSellerID from ContactsLite.dbo.Consultant_To_Director cd  (nolock) "+
			            "            inner join  ContactsLite.dbo.InternationalConsultants c (nolock) on cd.Director=c.ContactID ) di on p.ContactID=di.Consultant "+
                        "where PhoneNumber=@phone and PhoneNumberTypeID='5' and c.ConsultantStatus not like '%x%' ";


            DataSet ds = SqlHelper.ExecuteDataset(AppSetting.Community, CommandType.Text, sql, new SqlParameter("@phone", PhoneNumber));
            string json = JsonConvert.SerializeObject(ds.Tables[0]);
            return JsonConvert.DeserializeObject<List<Consultant>>(json);
        }

        public static List<Consultant> getinviter(string id)
        {
            string sql = "select c.LastName+c.FirstName Name ,c.ConsultantLevelID Level,c.UnitID " +
                        "from  ContactsLite.dbo.Consultants c " +
                        "join ContactsLite.dbo.InternationalConsultants i on c.ContactID=i.ContactID " +
                        "where i.DirectSellerID=@id and  c.ConsultantStatus not like '%x%'";

            DataSet ds = SqlHelper.ExecuteDataset(AppSetting.Community, CommandType.Text, sql, new SqlParameter("@id", id));
            string json = JsonConvert.SerializeObject(ds.Tables[0]);
            return JsonConvert.DeserializeObject<List<Consultant>>(json);
        }


        public static int addCustomer(Guid PartyKey, string Name, string PhoneNumber, string InviterId, string CreateBy)
        {
            try
            {
                string sql = "iparty_paperAddCustomer";
                SqlParameter[] param ={
                              new SqlParameter("@PartyKey",PartyKey),
                              new SqlParameter("@Name",Name),
                              new SqlParameter("@PhoneNumber",PhoneNumber),
                              new SqlParameter("@ReferenceId",InviterId),
                              new SqlParameter("@CheckInDate",DateTime.Now),
                              new SqlParameter("@CreateBy",CreateBy)
  
                                };
                object sn = SqlHelper.ExecuteScalar(AppSetting.Community, CommandType.StoredProcedure, sql, param);
                int outtext = 0;
                int.TryParse(sn.ToString(), out outtext);

                return outtext;
            }
            catch (Exception ex)
            {
                LogHelper.Debug("iparty_paperAddCustomer"+PartyKey + "/" + Name + "/" + PhoneNumber + "/" + InviterId + "/" + CreateBy + "----" + ex.ToString());
                return -1;
            }

        }


        public static int updateCustomer(Guid PartyKey , string PhoneNumber)
        {
            try
            {
                string sql = "iparty_paperUpdateCustomer";
                SqlParameter[] param ={
                              new SqlParameter("@PartyKey",PartyKey),
                              new SqlParameter("@PhoneNumber",PhoneNumber),
                              new SqlParameter("@CheckInDate",DateTime.Now)
  
                                };
                object sn = SqlHelper.ExecuteScalar(AppSetting.Community, CommandType.StoredProcedure, sql, param);
                int outtext = 0;
                int.TryParse(sn.ToString(), out outtext);

                return outtext;
            }
            catch (Exception ex)
            {
                LogHelper.Debug("iparty_paperUpdateCustomer"+PartyKey  + "/" + PhoneNumber  + "----" + ex.ToString());
                return -1;
            }

        }


        public static List<Customer> getInputCustomers(Guid PartyKey)
        {
            string sql = "select ic.Name,i.PhoneNumber,i.Status,i.CheckInType,i.Reference,c.DirectSellerID  from iparty_invitation i "+
                         "join IParty_Customer ic on i.CustomerKey=ic.CustomerKey "+
                         "left join ContactsLite.dbo.InternationalConsultants c on i.ReferenceKey=c.ContactID "+
                         "where partykey=@partykey and status='04' and checkintype='06' order by i.CheckinDate";

            DataSet ds = SqlHelper.ExecuteDataset(AppSetting.Community, CommandType.Text, sql, new SqlParameter("@partykey", PartyKey));
            string json = JsonConvert.SerializeObject(ds.Tables[0]);
            return JsonConvert.DeserializeObject<List<Customer>>(json);
        }

    }
}