using Newtonsoft.Json;
using PinkBus.CheckInClient.Entitys;
using PinkBus.CheckInClient.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading;

namespace PinkBus.CheckInClient.DAL
{
    public class EventDAL
    {
        public static void OperationSqlite(Action<SQLiteHelper> action)
        {
            using (SQLiteConnection conn = new SQLiteConnection(AppSetting.DataSource))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();
                    SQLiteHelper sh = new SQLiteHelper(cmd);
                    sh.BeginTransaction();
                    try
                    {
                        action(sh);
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
            string timeNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            using (SQLiteConnection conn = new SQLiteConnection(AppSetting.DataSource))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();
                    Event e = res.Event;
                    SQLiteHelper sh = new SQLiteHelper(cmd);
                    sh.BeginTransaction();
                    try
                    {
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
                        dic["DownloadDate"] = timeNow;

                        sh.Insert("PB_Event", dic);
                        #endregion

                        sh.Execute("insert into SyncStatusLog values ('" + e.EventKey.ToString().ToLower() + "','D',1,0,0,0,0,0,0 ,'" + DateTime.Now + "')");
                        //updateSyncStatusLog(sql);
                        worker.ReportProgress(8);
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

        public static void InsertSessions(BackgroundWorker worker, CheckTokenResponse res)
        {
            string timeNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            using (SQLiteConnection conn = new SQLiteConnection(AppSetting.DataSource))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();
                    Event e = res.Event;
                    SQLiteHelper sh = new SQLiteHelper(cmd);
                    sh.BeginTransaction();
                    try
                    {
                        #region Insert Sessions
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
                            dics["DownloadDate"] = timeNow;

                            sh.Insert("PB_EventSession", dics);

                        }

                        #endregion
                        // updateSyncStatusLog(sql);
                        worker.ReportProgress(10);
                        string sql = "update SyncStatusLog set Session=1 where EventKey='" + e.EventKey.ToString().ToLower() + "' and SyncType='D'";
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

        public static void InsertConsultants(BackgroundWorker worker, List<Consultant> consultants, Guid eventKey)
        {
            string timeNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            using (SQLiteConnection conn = new SQLiteConnection(AppSetting.DataSource))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();
                    SQLiteHelper sh = new SQLiteHelper(cmd);
                    sh.BeginTransaction();
                    try
                    {

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
                            dic["DownloadDate"] = timeNow;
                            dic["ChangeStatus"] = 0;
                            dic["SyncTimes"] = 0;
                            dic["SyncMsg"] = "";
                            sh.Insert("PB_Event_Consultant", dic);
                            #endregion
                            //Thread.Sleep(100);
                            //updateSyncStatusLog(sql);
                            int per = int.Parse(Math.Round((i * 1.00 / consultants.Count * 1.00) * 25.00 + 15, 0).ToString());
                            worker.ReportProgress(per);
                        }
                        string sql = "update SyncStatusLog set Consultant=1 where EventKey='" + eventKey.ToString().ToLower() + "' and SyncType='D'";
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

        public static void InsertTickets(BackgroundWorker worker, List<Ticket> tickets, Guid eventKey)
        {
            string timeNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            using (SQLiteConnection conn = new SQLiteConnection(AppSetting.DataSource))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();

                    SQLiteHelper sh = new SQLiteHelper(cmd);
                    sh.BeginTransaction();
                    try
                    {

                        for (int i = 0; i < tickets.Count; i++)
                        {
                            var e = tickets[i];
                            #region  insert event
                            var dic = new Dictionary<string, object>();
                            dic["TicketKey"] = e.TicketKey.ToString();
                            dic["MappingKey"] = e.MappingKey.ToString();
                            dic["EventKey"] = e.EventKey.ToString().ToLower();
                            dic["SessionKey"] = e.SessionKey.ToString();
                            dic["CustomerKey"] = e.CustomerKey.ToString();
                            dic["TicketType"] = e.TicketType;
                            dic["TicketFrom"] = e.TicketFrom;
                            dic["TicketStatus"] = e.TicketStatus;
                            dic["SMSToken"] = e.SMSToken;
                            dic["SessionStartDate"] = e.SessionStartDate;
                            dic["SessionEndDate"] = e.SessionEndDate;
                            dic["CheckinDate"] = e.CheckinDate;
                            dic["CreatedDate"] = e.CreatedDate;
                            dic["CreatedBy"] = e.CreatedBy;
                            dic["UpdatedDate"] = e.UpdatedDate;
                            dic["UpdatedBy"] = e.UpdatedBy;
                            dic["DownloadDate"] = timeNow;
                            dic["ChangeStatus"] = 0;
                            dic["SyncTimes"] = 0;
                            dic["SyncMsg"] = "";
                            sh.Insert("PB_Ticket", dic);
                            #endregion

                            //updateSyncStatusLog(sql);
                            int per = int.Parse(Math.Round((i * 1.00 / tickets.Count * 1.00) * 25.00 + 45, 0).ToString());
                            worker.ReportProgress(per);
                        }
                        string sql = "update SyncStatusLog set Ticket=1 where EventKey='" + eventKey.ToString().ToLower() + "' and SyncType='D'";
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

        public static void InsertCustomers(BackgroundWorker worker, List<Customer> customers, Guid eventKey)
        {
            string timeNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            using (SQLiteConnection conn = new SQLiteConnection(AppSetting.DataSource))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();

                    SQLiteHelper sh = new SQLiteHelper(cmd);
                    sh.BeginTransaction();
                    try
                    {

                        for (int i = 0; i < customers.Count; i++)
                        {
                            var e = customers[i];
                            #region  insert event
                            var dic = new Dictionary<string, object>();
                            dic["CustomerKey"] = e.CustomerKey.ToString();
                            dic["CustomerName"] = e.CustomerName;
                            dic["CustomerPhone"] = e.CustomerPhone;
                            dic["CustomerType"] = e.CustomerType;
                            dic["AgeRange"] = e.AgeRange;
                            dic["Career"] = e.Career;
                            dic["InterestingTopic"] = e.InterestingTopic;
                            dic["BeautyClass"] = e.BeautyClass;
                            dic["UsedProduct"] = e.UsedProduct;
                            dic["UsedSet"] = e.UsedSet;
                            dic["InterestInCompany"] = e.InterestInCompany;
                            dic["AcceptLevel"] = e.AcceptLevel;

                            dic["CustomerContactId"] = e.CustomerContactId;
                            dic["UnionID"] = e.UnionID;
                            dic["HeadImgUrl"] = e.HeadImgUrl;
                            dic["Source"] = e.Source;
                            dic["IsImportMyCustomer"] = e.IsImportMyCustomer;

                            dic["CreatedDate"] = e.CreatedDate;
                            dic["CreatedBy"] = e.CreatedBy;
                            dic["UpdatedDate"] = e.UpdatedDate;
                            dic["UpdatedBy"] = e.UpdatedBy;
                            dic["DownloadDate"] = timeNow;
                            dic["ChangeStatus"] = 0;
                            dic["SyncTimes"] = 0;
                            dic["SyncMsg"] = "";
                            sh.Insert("PB_Customer", dic);
                            #endregion

                            int per = int.Parse(Math.Round((i * 1.00 / customers.Count * 1.00) * 20.00 + 75, 0).ToString());
                            worker.ReportProgress(per);
                        }
                        sh.Commit();
                        string sql = "update SyncStatusLog set Customer=1 where EventKey='" + eventKey.ToString().ToLower() + "' and SyncType='D'";

                        OperationSqlite(e => e.Execute(sql));

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


        public static List<Event> QueryEvents(string where = null)
        {
            string sql = "select * from PB_Event ";
            if (!string.IsNullOrEmpty(where)) sql += where;

            sql += " order by DownloadDate desc";
            List<Event> events = new List<Event>();
            OperationSqlite(e => events = e.Selects<Event>(sql));
            return events;

        }

        public static List<Provinces> QueryProvinces()
        {
            string sql = "select * from PB_aProvince";
            List<Provinces> provinces = new List<Entitys.Provinces>();
            OperationSqlite(e => provinces = e.Selects<Provinces>(sql));
            return provinces;
        }

        public static List<Cities> QueryCitys(string id)
        {
            string sql = "select * from PB_aCity where father ='" + id + "' or father=0";
            List<Cities> cities = new List<Entitys.Cities>();
            OperationSqlite(e => cities = e.Selects<Cities>(sql));

            return cities;
        }

        public static List<Counties> QueryCountys(string id, string custom)
        {
            string sql = "select * from PB_aCounty where father='" + id + "' or father =0 " + custom + "";
            List<Counties> counties = new List<Entitys.Counties>();
            OperationSqlite(e => counties = e.Selects<Counties>(sql));

            return counties;
        }

        public static List<ConsultantsWhichHasUnUseTicket> QueryConsultantsWhichHasUnUseTickets(Guid eventKey)
        {
            string sql = @"select t.MappingKey,c.EventKey,c.ContactId,c.DirectSellerId,c.UserType,c.LastName,c.FirstName,t.TicketType, count(*) as Count from PB_Ticket t 
                           join PB_Event_Consultant c on t.MappingKey=c.MappingKey
                           where  t.EventKey='{0}' and (t.TicketStatus=0 or t.TicketStatus=1)
                           group by t.MappingKey ,TicketType
                           having count>0 ";

            sql = string.Format(sql, eventKey.ToString().ToLower());

            List<ConsultantsWhichHasUnUseTicket> consultants = new List<ConsultantsWhichHasUnUseTicket>();
            OperationSqlite(e => consultants = e.Selects<ConsultantsWhichHasUnUseTicket>(sql));

            return consultants;
        }




        public static SyncStatusLog GetSyncStatusLog(Guid eventKey, SyncType syncType)
        {
            string sql = "select * from SyncStatusLog where EventKey='" + eventKey.ToString().ToLower() + "' and SyncType='" + syncType + "'";
            SyncStatusLog syncStatusLog = new SyncStatusLog();
            OperationSqlite(e => syncStatusLog = e.Select<SyncStatusLog>(sql));
            return syncStatusLog;
        }

        public static List<SyncStatusLog> QuerySyncStatusLogs(string where = null)
        {
            string sql = "select * from SyncStatusLog ";
            sql += where;
            List<SyncStatusLog> syncStatusLogs = new List<SyncStatusLog>();
            OperationSqlite(e => syncStatusLogs = e.Selects<SyncStatusLog>(sql));
            return syncStatusLogs;
        }

        public static List<CustomerTicket> QueryCustomers(string eventKey)
        {
            string sql = "select * from pb_Ticket t join PB_Customer c  on t.customerkey=c.CustomerKey where t.eventkey='" + eventKey.ToString().ToLower() + "'  ";

            List<CustomerTicket> CustomerTickets = new List<CustomerTicket>();
            OperationSqlite(e => CustomerTickets = e.Selects<CustomerTicket>(sql));
            return CustomerTickets;
        }

        public static List<ConsultantTicket> QueryVolunteers(string eventKey)
        {
            string sql = "select * from PB_Event_Consultant c left join " +
                        "(select MappingKey,count(MappingKey) CheckinCount ,EventKey " +
                        "from PB_VolunteerCheckin  GROUP BY MappingKey) a " +
                        "on c.MappingKey=a.MappingKey " +
                        "where c.UserType=3 and c.EventKey='" + eventKey.ToString().ToLower() + "' ";
            List<ConsultantTicket> ConsultantTickets = new List<ConsultantTicket>();
            OperationSqlite(e => ConsultantTickets = e.Selects<ConsultantTicket>(sql));
            return ConsultantTickets;
        }

        public static List<object> QuerySellersContractId(Guid eventKey)
        {
            string sql = "select ContactId from PB_Event_Consultant where EventKey='" + eventKey.ToString().ToLower() + "'";
            List<object> rows = new List<object>();
            OperationSqlite(e => rows = e.Select(sql).AsEnumerable().Select(c => c.ItemArray[0]).ToList());

            return rows;
        }


        public static Consultant GetConsultant(string mappingKey, Guid eventKey)
        {
            string sql = "select * from PB_Event_Consultant where MappingKey='" + mappingKey + "' and EventKey='" + eventKey + "'";
            Consultant consultant = new Consultant();
            OperationSqlite(e => consultant = e.Select<Consultant>(sql));

            return consultant;
        }




        public static CustomerInfo GetCustomer(string customerKey, Guid eventKey)
        {
            string sql = "select * from PB_Customer c join PB_Ticket t on c.CustomerKey=t.CustomerKey " +
                "join PB_Event_Consultant ec on t.MappingKey=ec.MappingKey " +
                "where c.CustomerKey='" + customerKey + "' and t.EventKey='" + eventKey + "'";
            CustomerInfo customerInfo = new CustomerInfo();
            OperationSqlite(e => customerInfo = e.Select<CustomerInfo>(sql));

            return customerInfo;
        }

        public static int CancelTicket(string ticketKey)
        {
            using (SQLiteConnection conn = new SQLiteConnection(AppSetting.DataSource))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();
                    SQLiteHelper sh = new SQLiteHelper(cmd);
                    sh.BeginTransaction();
                    try
                    {

                        string sqlS = "select * from PB_Ticket where TicketKey='" + ticketKey + "'";
                        Ticket T = sh.Select<Ticket>(sqlS);

                        string sqlU = "update PB_Ticket set TicketStatus=4 , ChangeStatus=1 where TicketKey='" + ticketKey + "'";
                        sh.ExecuteNonQuery(sqlU);

                        Dictionary<string, object> val = new Dictionary<string, object>();
                        val.Add("TicketKey", Guid.NewGuid().ToString());
                        val.Add("MappingKey", T.MappingKey.ToString());
                        val.Add("EventKey", T.EventKey.ToString());
                        val.Add("SessionKey", T.SessionKey.ToString());
                        val.Add("CustomerKey", "");
                        val.Add("TicketType", T.TicketType);
                        val.Add("TicketFrom", 3);
                        val.Add("TicketStatus", 0);
                        val.Add("SMSToken", Common.MakeSMSToken());
                        val.Add("SessionStartDate", T.SessionStartDate);
                        val.Add("SessionEndDate", T.SessionEndDate);
                        val.Add("CheckinDate", "");
                        val.Add("CreatedDate", DateTime.Now);
                        val.Add("CreatedBy", "ClientRebuild");
                        val.Add("UpdatedDate", "");
                        val.Add("UpdatedBy", "");
                        val.Add("DownloadDate", DateTime.Now);
                        val.Add("ChangeStatus", 1);
                        val.Add("SyncTimes", 0);
                        val.Add("SyncMsg", "");
                        sh.Insert("PB_Ticket", val);

                        sh.Commit();
                        return 1;
                    }
                    catch (Exception ex)
                    {
                        sh.Rollback();
                    }
                    conn.Close();
                }
            }


            return 0;
        }

        public static int CancelVolunter(string mappingKey)
        {
            string sql = "update PB_Event_Consultant set Status=" + (int)ConsultantStatus .Canceled+ " ,ChangeStatus=1 where MappingKey='" + mappingKey + "' ";
            int res = 0;
            OperationSqlite(e => res = e.ExecuteNonQuery(sql));
            return res;
        }

        public static List<Ticket> QueryTickets(Guid eventKey)
        {
            string sql = "select * from PB_Ticket where EventKey='" + eventKey.ToString().ToLower() + "'";
            List<Ticket> ticktes = new List<Ticket>();
            OperationSqlite(e => ticktes = e.Selects<Ticket>(sql));
            return ticktes;
        }

        public static List<Ticket> QueryTicketsReserve(Guid eventKey)
        {
            string sql = "select * from PB_Ticket t join PB_Event_Consultant c on t.MappingKey=c.MappingKey where t.EventKey='" + eventKey.ToString().ToLower() + "' and c.DirectSellerId in ('000000000020','000000000021','000000000022','000000000023','000000000024','000000000025') and t.ticketstatus in (0,1)";
            List<Ticket> ticktes = new List<Ticket>();
            OperationSqlite(e => ticktes = e.Selects<Ticket>(sql));
            return ticktes;
        }

        public static bool AddNewCustomer(Customer customer, string ticketType, string mappingKey)
        {
            using (SQLiteConnection conn = new SQLiteConnection(AppSetting.DataSource))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();
                    SQLiteHelper sh = new SQLiteHelper(cmd);
                    sh.BeginTransaction();
                    try
                    {
                        string sqlU = "update PB_Ticket set TicketStatus=5, CustomerKey='" + customer.CustomerKey
                            + "',CheckinDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                            + "',UpdatedBy='ClientCreateNew' ,ChangeStatus=1 where TicketKey=(select TicketKey from PB_Ticket where MappingKey='"
                            + mappingKey + "' and ticketType='" + ticketType + "' and  (TicketStatus=0 or TicketStatus=1))";
                        sh.ExecuteNonQuery(sqlU);

                        Dictionary<string, object> val = new Dictionary<string, object>();
                        val.Add("CustomerKey", customer.CustomerKey.ToString());
                        val.Add("CustomerName", customer.CustomerName);
                        val.Add("CustomerPhone", customer.CustomerPhone);
                        val.Add("CustomerType", customer.CustomerType);
                        val.Add("AgeRange", customer.AgeRange);
                        val.Add("Career", customer.Career);
                        val.Add("InterestingTopic", customer.InterestingTopic);
                        val.Add("BeautyClass", customer.BeautyClass);
                        val.Add("UsedProduct", customer.UsedProduct);
                        val.Add("UsedSet", customer.UsedSet);
                        val.Add("InterestInCompany", customer.InterestInCompany);
                        val.Add("AcceptLevel", "");
                        val.Add("CustomerContactId", 0);
                        val.Add("UnionID", "");
                        val.Add("HeadImgUrl", "");
                        val.Add("Source", 3);
                        val.Add("IsImportMyCustomer", 0);
                        val.Add("CreatedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        val.Add("CreatedBy", "WinClient");
                        val.Add("UpdatedDate", "");
                        val.Add("UpdatedBy", "");
                        val.Add("DownloadDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        val.Add("ChangeStatus", 1);
                        val.Add("SyncTimes", 0);
                        val.Add("SyncMsg", "");
                        sh.Insert("PB_Customer", val);

                        sh.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        sh.Rollback();

                    }
                    conn.Close();

                }
            }
            return false;
        }


        public static bool AddVolunteer(Consultant consultant)
        {
            using (SQLiteConnection conn = new SQLiteConnection(AppSetting.DataSource))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();
                    SQLiteHelper sh = new SQLiteHelper(cmd);
                    sh.BeginTransaction();
                    try
                    {
                        #region addVolunteer
                        Dictionary<string, object> val = new Dictionary<string, object>();
                        val.Add("MappingKey", consultant.MappingKey.ToString());
                        val.Add("EventKey", consultant.EventKey.ToString());
                        val.Add("UserType", (int)EventUserType.VolunteerBC);
                        val.Add("NormalTicketQuantity", consultant.NormalTicketQuantity);
                        val.Add("VIPTicketQuantity", consultant.VIPTicketQuantity);
                        val.Add("NormalTicketSettingQuantity", consultant.NormalTicketSettingQuantity);
                        val.Add("VIPTicketSettingQuantity", consultant.VIPTicketSettingQuantity);
                        val.Add("ContactId", "");
                        val.Add("DirectSellerId", consultant.DirectSellerId);
                        val.Add("FirstName", consultant.FirstName);
                        val.Add("LastName", consultant.LastName);
                        val.Add("PhoneNumber", consultant.PhoneNumber);
                        val.Add("Level", consultant.Level);
                        val.Add("ResidenceID", consultant.ResidenceID);
                        val.Add("Province", consultant.Province);
                        val.Add("City", consultant.City);
                        val.Add("OECountyId", "");
                        val.Add("CountyName", consultant.CountyName);
                        val.Add("CreatedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        val.Add("CreatedBy", "ClientAdd");
                        val.Add("UpdatedDate", "");
                        val.Add("UpdatedBy", "");
                        val.Add("Status", (int)ConsultantStatus.CheckedIn);
                        val.Add("IsConfirmed", 1);
                        val.Add("DownloadDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        val.Add("ChangeStatus", 1);
                        val.Add("SyncTimes", 0);
                        val.Add("SyncMsg", "");
                        sh.Insert("PB_Event_Consultant", val);
                        #endregion

                        #region  getReserveTickets
                        List<Ticket> Tickets = QueryTicketsReserve(consultant.EventKey);
                        List<Ticket> TicketsV = Tickets.FindAll(e => e.TicketType == 0);
                        List<Ticket> TicketsN = Tickets.FindAll(e => e.TicketType == 1);
                        for (int i = 0; i < consultant.VIPTicketSettingQuantity; i++)
                        {
                            if (TicketsV.Count - 1 < i) continue;
                            Dictionary<string, object> VIPT = new Dictionary<string, object>();
                            VIPT.Add("TicketKey", Guid.NewGuid().ToString());
                            VIPT.Add("MappingKey", consultant.MappingKey.ToString());
                            VIPT.Add("EventKey", consultant.EventKey.ToString());
                            VIPT.Add("SessionKey", TicketsV[i].SessionKey.ToString());
                            VIPT.Add("CustomerKey", "");
                            VIPT.Add("TicketType", 0);
                            VIPT.Add("TicketFrom", 2);
                            VIPT.Add("TicketStatus", 0);
                            VIPT.Add("SMSToken", Common.MakeSMSToken());
                            VIPT.Add("SessionStartDate", TicketsV[i].SessionStartDate);
                            VIPT.Add("SessionEndDate", TicketsV[i].SessionEndDate);
                            VIPT.Add("CheckinDate", "");
                            VIPT.Add("CreatedDate", DateTime.Now);
                            VIPT.Add("CreatedBy", "ClientBestowal");
                            VIPT.Add("UpdatedDate", "");
                            VIPT.Add("UpdatedBy", "");
                            VIPT.Add("DownloadDate", DateTime.Now);
                            VIPT.Add("ChangeStatus", 1);
                            VIPT.Add("SyncTimes", 0);
                            VIPT.Add("SyncMsg", "");
                            sh.Insert("PB_Ticket", VIPT);
                            string sql = "update PB_Ticket set TicketStatus=3,ChangeStatus=1 where TicketKey='" + TicketsV[i].TicketKey + "'";
                            sh.ExecuteNonQuery(sql);
                        }

                        for (int i = 0; i < consultant.NormalTicketSettingQuantity; i++)
                        {
                            if (TicketsN.Count - 1 < i) continue;
                            Dictionary<string, object> NormalT = new Dictionary<string, object>();
                            NormalT.Add("TicketKey", Guid.NewGuid().ToString());
                            NormalT.Add("MappingKey", consultant.MappingKey.ToString());
                            NormalT.Add("EventKey", consultant.EventKey.ToString());
                            NormalT.Add("SessionKey", TicketsN[i].SessionKey.ToString());
                            NormalT.Add("CustomerKey", "");
                            NormalT.Add("TicketType", 1);
                            NormalT.Add("TicketFrom", 2);
                            NormalT.Add("TicketStatus", 0);
                            NormalT.Add("SMSToken", Common.MakeSMSToken());
                            NormalT.Add("SessionStartDate", TicketsN[i].SessionStartDate);
                            NormalT.Add("SessionEndDate", TicketsN[i].SessionEndDate);
                            NormalT.Add("CheckinDate", "");
                            NormalT.Add("CreatedDate", DateTime.Now);
                            NormalT.Add("CreatedBy", "ClientBestowal");
                            NormalT.Add("UpdatedDate", "");
                            NormalT.Add("UpdatedBy", "");
                            NormalT.Add("DownloadDate", DateTime.Now);
                            NormalT.Add("ChangeStatus", 1);
                            NormalT.Add("SyncTimes", 0);
                            NormalT.Add("SyncMsg", "");
                            sh.Insert("PB_Ticket", NormalT);
                            string sql = "update PB_Ticket set TicketStatus=3,ChangeStatus=1 where TicketKey='" + TicketsN[i].TicketKey + "'";
                            sh.ExecuteNonQuery(sql);
                        }
                        #endregion

                        string sqlVI = "insert into PB_VolunteerCheckin values('" + Guid.NewGuid() + "','" + consultant.EventKey.ToString() + "','" + consultant.MappingKey.ToString() + "','" + DateTime.Now.ToString() + "','" + DateTime.Now.Date.ToString() + "','" + DateTime.Now.ToString() + "','ClientAdd','','',1,0,'')";
                        sh.ExecuteNonQuery(sqlVI);
                        sh.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        sh.Rollback();
                    }
                    conn.Close();
                }
            }
            return false;
        }

        public static bool PassCodeCheckin(Guid eventKey, string phone, string code, ref Guid customerKey, ref string customerName, ref TicketType ticketType, out string err)
        {
            string sqlS = "select * from PB_Ticket t join PB_Customer c on t.CustomerKey=c.CustomerKey where t.SMSToken='" + code + "' and t.EventKey='" + eventKey.ToString() + "'";
            CustomerTicket cTicket = new CustomerTicket();
            OperationSqlite(e => cTicket = e.Select<CustomerTicket>(sqlS));
            if (cTicket.TicketKey != null)
            {
                if (cTicket.TicketStatus == TicketStatus.Checkin)
                {
                    err = "请勿重复签到！";
                    return false;
                }
                if (cTicket.CustomerPhone.Contains(phone))
                {
                    string sqlU = "update PB_Ticket set TicketStatus='" + (int)TicketStatus.Checkin + "', checkindate='"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+"' ,updatedby='SMS', ChangeStatus=1 where TicketKey='" + cTicket.TicketKey.ToString() + "'";
                    int res = 0;
                    OperationSqlite(e => res = e.ExecuteNonQuery(sqlU));
                    if (res > 0)
                    {
                        customerName = cTicket.CustomerName;
                        ticketType = cTicket.TicketType;
                        customerKey = cTicket.CustomerKey;
                        err = "签到成功！";
                        return true;
                    }
                    else
                    {
                        err = "签到失败！";
                        return false;
                    }
                }
                else
                {
                    err = "手机号码错误！";
                    return false;
                }
            }
            err = "短信口令错误！";
            return false;
        }

        public static bool QRCodeCheckin(Guid eventKey, string code, ref Guid customerKey, ref string msg)
        {
            if (code.Length == 12)
            {
                string sql = "select * from PB_Event_Consultant where DirectSellerId='" + code + "' and usertype=3 and EventKey='" + eventKey + "'";
                Consultant con = new Consultant();
                OperationSqlite(e => con = e.Select<Consultant>(sql));
                if (con.DirectSellerId != null)
                {
                    customerKey = con.MappingKey;

                    string sqlS = "select * from PB_VolunteerCheckin where MappingKey='" + con.MappingKey.ToString() + "' and EventKey='" + eventKey + "'";
                    List<VolunteerCheckin> vol = new List<VolunteerCheckin>();
                    OperationSqlite(e => vol = e.Selects<VolunteerCheckin>(sqlS));
                    if (con.Status == (int)ConsultantStatus.Canceled)
                    {
                        msg = "该志愿者作废,不能签到！";
                        return false;
                    }

                    if (vol.FindAll(e => e.CheckinDate > DateTime.Now.Date && e.CheckinDate < DateTime.Now.AddDays(1).Date).Count > 0)
                    {
                        msg = "该志愿者今日已签到，请勿重复签到！";
                        return false;
                    }
                    string sqlI = "insert into PB_VolunteerCheckin values('" + Guid.NewGuid() + "','" + con.EventKey.ToString() + "','" + con.MappingKey.ToString() + "','" + DateTime.Now.ToString() + "','" + DateTime.Now.Date.ToString() + "','" + DateTime.Now.ToString() + "','ClientAdd','','',1,0,'');";
                    sqlI = sqlI + "update PB_Event_Consultant set status=" + (int)ConsultantStatus.CheckedIn + " , ChangeStatus=1  where MappingKey='" + con.MappingKey + "';";
                    int res = 0;
                    //添加签到状态记得
                    OperationSqlite(e => res = e.ExecuteNonQuery(sqlI));
                    if (res > 1)
                    {
                        msg = "志愿者 " + con.LastName + con.FirstName + "！";
                        return true;
                    }


                }
            }
            else
            {
                string sql = "select * from PB_Ticket t join PB_Customer c on t.CustomerKey=c.CustomerKey where t.TicketKey='" + code + "' and EventKey='" + eventKey + "'";
                CustomerTicket tic = new CustomerTicket();
                OperationSqlite(e => tic = e.Select<CustomerTicket>(sql));
                if (tic.TicketKey != null)
                {
                    if (tic.TicketStatus == TicketStatus.Checkin)
                    {
                        msg = "该顾客已签到,请勿重复签到！";
                        return false;
                    }

                    if (tic.TicketStatus == TicketStatus.Canceled)
                    {
                        msg = "该顾客已作废,不能签到！";
                        return false;
                    }
                    customerKey = tic.CustomerKey;
                    string sqlU = "update PB_Ticket set TicketStatus=5, ChangeStatus=1,updatedby='QRCode', CheckinDate='"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+"' where TicketKey='" + code + "'";
                    int res = 0;
                    OperationSqlite(e => res = e.ExecuteNonQuery(sqlU));
                    if (res > 0)
                    {
                        msg = (tic.TicketType == TicketType.Normal ? "来宾 " : "贵宾 ") + tic.CustomerName;
                        return true;
                    }
                }

            }

            msg = "二维码无效,签到失败！";

            return false;
        }

        public static DataTable QueryVolunteerExcels(Guid eventKey)
        {
            string sql = "select checkinday,count(checkinday) from PB_VolunteerCheckin where eventkey ='" + eventKey + "' group by checkinday";
            DataTable dt = new DataTable();
            OperationSqlite(e => dt = e.Select(sql));
            return dt;
        }

        public static List<VolunteerForExcel> QueryConsultantsForExcel(Guid eventKey, DateTime date)
        {
            string sql = "select ec.EventKey,ec.MappingKey,DirectSellerId, LastName||FirstName as ConsultantName, PhoneNumber as ConsultantPhone, Level as ConsultantLevel,CountyName as County,  * from PB_Event_Consultant ec join PB_VolunteerCheckin vc on ec.mappingkey=vc.mappingkey where ec.eventkey='" + eventKey.ToString() + "' and vc.CheckinDay='" + date + "'";
            List<VolunteerForExcel> cons = new List<VolunteerForExcel>();
            OperationSqlite(e => cons = e.Selects<VolunteerForExcel>(sql));
            return cons;
        }
        public static List<VolunteerCheckin> QueryVolunteerCheckinForExcel(Guid eventKey)
        {
            string sql = "select * from PB_VolunteerCheckin and EventKey='" + eventKey + "'";
            List<VolunteerCheckin> checkins = new List<VolunteerCheckin>();
            OperationSqlite(e => checkins = e.Selects<VolunteerCheckin>(sql));
            return checkins;
        }

        public static Consultant GetUploadConsultantTop1(Guid eventKey)
        {
            string sql = "select * from PB_Event_Consultant where eventKey='" + eventKey + "' and  ChangeStatus=1 and SyncTimes<3 order by SyncTimes LIMIT 1";
            Consultant con = new Consultant();
            OperationSqlite(e => con = e.Select<Consultant>(sql));
            return con;
        }

        public static Customer GetUploadCustomerTop1(Guid eventKey)
        {
            string sql = "select * from PB_Customer c join Pb_Ticket t on c.CustomerKey=t.CustomerKey where t.EventKey='" + eventKey + "' and c.ChangeStatus=1 and c.SyncTimes<3 order by SyncTimes LIMIT 1";
            Customer cus = new Customer();
            OperationSqlite(e => cus = e.Select<Customer>(sql));
            return cus;
        }

        public static Ticket GetUploadTicketTop1(Guid eventKey)
        {
            string sql = "select * from PB_Ticket where eventKey='" + eventKey + "' and  ChangeStatus=1 and SyncTimes<3 order by SyncTimes LIMIT 1";
            Ticket tic = new Ticket();
            OperationSqlite(e => tic = e.Select<Ticket>(sql));
            return tic;
        }

        public static VolunteerCheckin GetUploadVolunteerTop1(Guid eventKey)
        {
            string sql = "select * from PB_VolunteerCheckin where eventKey='" + eventKey + "' and  ChangeStatus=1 and SyncTimes<3 order by SyncTimes LIMIT 1";
            VolunteerCheckin vol = new VolunteerCheckin();
            OperationSqlite(e => vol = e.Select<VolunteerCheckin>(sql));
            return vol;
        }


        public static int ExecuteNonQuerySql(string sql)
        {
            int res = 0;
            OperationSqlite(e => res = e.ExecuteNonQuery(sql));
            return res;
        }


        public static int UploadRowCount(Guid eventKey, out int conCount, out int cusCount, out int ticCount, out int volCount)
        {
            string sqlCon = "select  count(*) from PB_Event_Consultant where eventKey='" + eventKey + "' and  ChangeStatus=1 and SyncTimes<3 ";
            DataTable conDT = new DataTable();
            OperationSqlite(e => conDT = e.Select(sqlCon));
            conCount = int.Parse(conDT.Rows[0][0].ToString());

            string sqlCus = "select  count(*) from PB_Customer c join Pb_Ticket t on c.Customerkey=t.CustomerKey where t.EventKey='" + eventKey + "' and c.ChangeStatus=1 and c.SyncTimes<3";
            DataTable cusDT = new DataTable();
            OperationSqlite(e => cusDT = e.Select(sqlCus));
            cusCount = int.Parse(cusDT.Rows[0][0].ToString());

            string sqlTic = "select  count(*) from PB_Ticket where eventKey='" + eventKey + "' and  ChangeStatus=1 and SyncTimes<3";
            DataTable ticDT = new DataTable();
            OperationSqlite(e => ticDT = e.Select(sqlTic));
            ticCount = int.Parse(ticDT.Rows[0][0].ToString());

            string sqlVol = "select count(*) from PB_VolunteerCheckin where eventKey='" + eventKey + "' and  ChangeStatus=1 and SyncTimes<3";
            DataTable volDT = new DataTable();
            OperationSqlite(e => volDT = e.Select(sqlVol));
            volCount = int.Parse(volDT.Rows[0][0].ToString());
            int count = conCount + cusCount + ticCount + volCount;
            return count;
        }


    }

}
