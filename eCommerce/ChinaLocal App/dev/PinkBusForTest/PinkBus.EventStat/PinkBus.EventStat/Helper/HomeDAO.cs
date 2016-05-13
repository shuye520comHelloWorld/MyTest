
using Newtonsoft.Json;
using PinkBus.EventStat.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace PinkBus.EventStat.Helper
{
    public class HomeDAO
    {
        public static  List<T> GetPageItems<T>(SearchModel SM)
        {
            try
            {
                string sql = "[dbo].[PB_Pagination]";

                SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@Page",SM.Page)
                ,new SqlParameter("@PageSize",SM.pageSize)
                ,new SqlParameter("@Table",SM.TableName)
                ,new SqlParameter("@Field","*")
                ,new SqlParameter("@OrderBy",SM.OrderBy)
                ,new SqlParameter("@Filter",SM.Filter)
                ,new SqlParameter("@MaxPage",SqlDbType.Int)
                ,new SqlParameter("@TotalRow",SqlDbType.Int)
                ,new SqlParameter("@Descript",SqlDbType.NVarChar,2000)
            };

                Params[6].Direction = ParameterDirection.Output;
                Params[7].Direction = ParameterDirection.Output;
                Params[8].Direction = ParameterDirection.Output;

                DataSet ds = SqlHelper.ExecuteDataset(AppSetting.Community, CommandType.StoredProcedure, sql, Params);
                SM.MaxPage = Common.ConvertTo<int>(Params[6].Value, 0);
                SM.Descript = Params[8].Value.ToString();
                SM.TotalRow = Common.ConvertTo<int>(Params[7].Value, 0);
                if (ds.Tables.Count > 0)
                {
                    string json = JsonConvert.SerializeObject(ds.Tables[0]);
                    List<T> list = JsonConvert.DeserializeObject<List<T>>(json);

                    return list;
                }
                SM.TotalRow = 0;
                return new List<T>();
            }
            catch (Exception ex)
            {
                LogHelper.Debug(ex.ToString());
                return new List<T>();
            }
        }


        public static List<PB_OfflineCustomer> GetPageNewCustomer(SearchModel SM,Guid eventKey)
        {
            var customers = GetPageItems<PB_OfflineCustomer>(SM);
            if (customers.Count > 0)
            {
                var ids = from c in customers
                         select c.DirectSellerId;

                string idsStr = string.Join("','", ids.ToArray());
                idsStr = "'" + idsStr + "'";

                string sql = @"select DirectSellerID,c.LastName,c.FirstName from  ContactsLite.dbo.InternationalConsultants i (NOLOCK)
                            join ContactsLite.dbo.Consultants c (NOLOCK) on i.ContactID=c.ContactID
                            where  i.DirectSellerID in(" + idsStr + ")";
                DataSet ds = SqlHelper.ExecuteDataset(AppSetting.Community, CommandType.Text, sql);
                if (ds.Tables.Count > 0)
                {
                    string json = JsonConvert.SerializeObject(ds.Tables[0]);
                    List<PB_OfflineCustomerSeller> c = JsonConvert.DeserializeObject<List<PB_OfflineCustomerSeller>>(json);
                    foreach (PB_OfflineCustomer oc in customers)
                    {
                        var cs = c.FirstOrDefault(e => e.DirectSellerId == oc.DirectSellerId);
                        oc.FirstName = cs.FirstName;
                        oc.LastName = cs.LastName;
                    }
                    return customers;
                }
                return null;

            }
            return new List<PB_OfflineCustomer>();
        }

        public static List<CheckinUser> GetCheckinVolunteer(Guid eventkey)
        {
            string sql = @"select distinct ec.MappingKey,ec.DirectSellerId,ec.FirstName,ec.LastName, TicketType=2,
                         CustomerName=ec.LastName+ec.FirstName,ec.PhoneNumber as CustomerPhone,ec.CreatedDate,c.CheckinDate from PB_VolunteerCheckin c (NOLOCK)
                        right join [PB_Event-Consultant] ec (NOLOCK) on c.MappingKey=ec.MappingKey
                         where ec.EventKey=@eventkey 
                         and c.CheckinDate in 
                         (select MAX(b.CheckinDate) from PB_VolunteerCheckin b (NOLOCK) where ec.MappingKey=b.MappingKey) or c.CheckinDate is null ";
            DataSet ds = SqlHelper.ExecuteDataset(AppSetting.Community, CommandType.Text, sql, new SqlParameter("@eventkey", eventkey));

            if (ds.Tables.Count > 0)
            {
                string json = JsonConvert.SerializeObject(ds.Tables[0]);
                List<CheckinUser> c = JsonConvert.DeserializeObject<List<CheckinUser>>(json);
                return c;
            }
            return new List<CheckinUser>();


        }
        //得到导出签到者名单Exl表的数据
        public static List<CheckinUser> GetCheckinExl(Guid eventkey)
        {
            string sql = @"(select  c.MappingKey,c.DirectSellerId,c.FirstName,cu.CustomerKey,c.LastName, 
                            cu.CustomerName,cu.CustomerPhone,t.TicketType, t.TicketStatus,t.UpdatedBy,
                             cu.CreatedDate,t.CheckinDate 
                            from [PB_Event-Consultant] c (NOLOCK) join PB_Ticket t (NOLOCK) on c.MappingKey=t.MappingKey
                            join PB_Customer cu (NOLOCK) on cu.CustomerKey=t.CustomerKey
                            where t.EventKey=@EventKey
                            and t.CustomerKey is not null and (TicketStatus in (2,5,6)))
                            union 
                            (select  distinct ec.MappingKey,ec.DirectSellerId,ec.FirstName,CustomerKey=null,
                            ec.LastName, CustomerName=ec.LastName+ec.FirstName,ec.PhoneNumber as CustomerPhone, 
                            TicketType=2,(case when c.CheckinDate is null then 2 else 5 end) TicketStatus,c.UpdatedBy, ec.CreatedDate,c.CheckinDate 
                            from  [PB_Event-Consultant] ec (NOLOCK) left join
                            (select MAX(CheckinDate) CheckinDate,mappingkey,UpdatedBy from  PB_VolunteerCheckin v where EventKey=@EventKey group by MappingKey,UpdatedBy) c
                             on c.MappingKey=ec.MappingKey
                            where ec.EventKey=@EventKey and ec.UserType=3 and ec.Status in (0,1,3))   order  BY TicketType";
            DataSet ds = SqlHelper.ExecuteDataset(AppSetting.Community, CommandType.Text, sql, new SqlParameter("@eventkey", eventkey));
            if (ds.Tables.Count > 0)
            {
                string json = JsonConvert.SerializeObject(ds.Tables[0]);
                List<CheckinUser> c = JsonConvert.DeserializeObject<List<CheckinUser>>(json);
                return c;
            }
            return new List<CheckinUser>();
        }

        public static CheckinSummary GetCheckSummary(Guid eventKey)
        {
            string sql = @"select * from PB_Ticket (NOLOCK) where  CustomerKey is not null and EventKey=@eventKey and (TicketStatus in (2,5,6))
                            (select ec.MappingKey, (case when c.CheckinDate is null then 2 else 5 end) TicketStatus,
                            c.CheckinDate 
                            from  [PB_Event-Consultant] ec (NOLOCK) left join
                            (select MAX(CheckinDate) CheckinDate,mappingkey from  PB_VolunteerCheckin v (NOLOCK) where EventKey=@eventKey group by MappingKey) c 
                             on c.MappingKey=ec.MappingKey
                            where ec.EventKey=@eventKey and ec.Status in (0,1,3) and ec.UserType=3)";

            DataSet ds = SqlHelper.ExecuteDataset(AppSetting.Community, CommandType.Text, sql, new SqlParameter("@eventkey", eventKey));
            CheckinSummary summary = new CheckinSummary();
            if (ds.Tables.Count > 0)
            {
                string json = JsonConvert.SerializeObject(ds.Tables[0]);
                List<PB_Ticket>  Tickets= JsonConvert.DeserializeObject<List<PB_Ticket>>(json);

                summary.NormalCheckinCount = Tickets.FindAll(e => e.TicketType==TicketType.Normal && e.TicketStatus == TicketStatus.Checkin).Count;
                summary.NormalCount = Tickets.FindAll(e => e.TicketType==TicketType.Normal&&(e.TicketStatus == TicketStatus.Invited || e.TicketStatus == TicketStatus.UnCheckin)).Count + summary.NormalCheckinCount;
                summary.VIPCheckinCount = Tickets.FindAll(e => e.TicketType == TicketType.VIP && e.TicketStatus == TicketStatus.Checkin).Count;
                summary.VIPCount = Tickets.FindAll(e => e.TicketType == TicketType.VIP && (e.TicketStatus == TicketStatus.Invited || e.TicketStatus == TicketStatus.UnCheckin)).Count + summary.VIPCheckinCount;

                string volunteerJson = JsonConvert.SerializeObject(ds.Tables[1]);
                List<PB_Consultant> volunteer = JsonConvert.DeserializeObject<List<PB_Consultant>>(volunteerJson);
                summary.VolunteerCount = volunteer.Count;
                summary.VolunteerCheckinCount = volunteer.FindAll(e => e.TicketStatus.Value == 5).Count;

            }
            return summary;
        }

        public static void GetEventInfo(Guid eventKey, ref int NormalTicketCount, ref int VIPTicketCount, ref int inviterCount,ref int checkinCount,ref int needCheckinCount,ref int newCustomerCount)
        {
            string sql = @"select * from PB_Ticket (NOLOCK) where EventKey=@eventKey  and TicketStatus in (0,1,2,5,6,7)
                           select * from [PB_Event-Consultant] (NOLOCK) where EventKey=@eventKey and UserType=3 and Status in (0,1,3)";

            DataSet ds = SqlHelper.ExecuteDataset(AppSetting.Community, CommandType.Text, sql, new SqlParameter("@eventKey", eventKey));
            if (ds.Tables.Count > 0)
            {
                string json = JsonConvert.SerializeObject(ds.Tables[0]);
                List<PB_Ticket> Tickets = JsonConvert.DeserializeObject<List<PB_Ticket>>(json);

                string json2 = JsonConvert.SerializeObject(ds.Tables[1]);
                List<PB_Consultant> Consultants = JsonConvert.DeserializeObject<List<PB_Consultant>>(json2);

                NormalTicketCount = Tickets.FindAll(e => e.TicketType == TicketType.Normal).Count;
                VIPTicketCount = Tickets.FindAll(e => e.TicketType == TicketType.VIP).Count;
                checkinCount = Tickets.FindAll(e => e.TicketStatus == TicketStatus.Checkin).Count+Consultants.FindAll(e=>e.Status==3).Count;
                needCheckinCount = Tickets.FindAll(e => e.TicketStatus ==TicketStatus.Checkin||e.TicketStatus==TicketStatus.Invited).Count + Consultants.Count;


            }

            string sqlInviter = @"select count(distinct t.MappingKey) from [PB_Event-Consultant] p (NOLOCK) join
                                    PB_Ticket t (NOLOCK) on p.MappingKey=t.MappingKey 
                                    where t.EventKey=@eventKey and  t.TicketStatus in (0,1,2,5,6,7)";

            object es = SqlHelper.ExecuteScalar(AppSetting.Community, CommandType.Text, sqlInviter, new SqlParameter("@eventKey", eventKey));
            
            if (es!=null)
            {
                int.TryParse(es.ToString(), out inviterCount);
            }

            string newCustomer = " select count(1) from PB_OfflineCustomer (NOLOCK) where EventKey=@eventKey";
            object customerCount = SqlHelper.ExecuteScalar(AppSetting.Community, CommandType.Text, newCustomer, new SqlParameter("@eventKey", eventKey));

            if (customerCount != null)
            {
                int.TryParse(customerCount.ToString(), out newCustomerCount);
            }

        }

        public static List<pb_Inviter> GetInviter(SearchModel SM, Guid eventKey)
        {


            return new List<pb_Inviter>();
        }

        public static List<PB_EventSession> GetEventSessions(Guid eventKey)
        {
            string sql = @"
                            select es.SessionKey,es.displayorder,
                            SUM(case when TicketType=1 then 1 else 0 end) NormalTicketUsedCount,
                            SUM(case when TicketType=0 then 1 else 0 end) VipTicketUsedCount,
                            es.SessionStartDate,
                            es.SessionEndDate 
                            from PB_EventSession es (nolock)
                            left join PB_Ticket tn (nolock) on es.SessionKey=tn.SessionKey and tn.TicketStatus in( 0,1,2, 5, 6, 7 )
                            where es.EventKey=@eventkey
                            group by  es.SessionKey,es.SessionStartDate,es.SessionEndDate ,es.displayorder
                            order by es.displayorder";
            DataSet ds = SqlHelper.ExecuteDataset(AppSetting.Community, CommandType.Text, sql, new SqlParameter("@eventkey", eventKey));

            if (ds.Tables.Count > 0)
            {
                string json = JsonConvert.SerializeObject(ds.Tables[0]);
                List<PB_EventSession> c = JsonConvert.DeserializeObject<List<PB_EventSession>>(json);
                return c;
            }
            return new List<PB_EventSession>();
        }
        //得到导出新顾客Exl表的数据
        public static List<PB_OfflineCustomer> GetNewCustomerExl(Guid eventkey)
        {
           string sql = @"select CustomerName,ContactInfo,ContactType, AgeRange,
                        CreatedDate,c.LastName,c.FirstName,oc.Career, oc.customertype,oc.DirectSellerId,oc.IsHearMaryKay,oc.IsJoinEvent,oc.UsedProduct,oc.CustomerResponse,oc.BestContactDate,oc.AdviceContactDate,oc.InterestingTopic,oc.Province,oc.City,oc.County
                         from pb_offlinecustomer oc  (NOLOCK)
                         left join ContactsLite.dbo.InternationalConsultants i (NOLOCK) on oc.DirectSellerId collate SQL_Latin1_General_CP1_CI_AS=i.DirectSellerID  
                         left join ContactsLite.dbo.Consultants c (NOLOCK) on c.ContactID=i.ContactID 
                         where oc.EventKey=@eventkey order BY CreatedDate desc";
            DataSet ds = SqlHelper.ExecuteDataset(AppSetting.Community, CommandType.Text, sql, new SqlParameter("@eventkey", eventkey));

            if (ds.Tables.Count > 0)
            {
                string json = JsonConvert.SerializeObject(ds.Tables[0]);
                List<PB_OfflineCustomer> c = JsonConvert.DeserializeObject<List<PB_OfflineCustomer>>(json);
                return c;
            }
            return new List<PB_OfflineCustomer>();
        }
        //得到导出邀约者Exl表的数据
        public static List<pb_Inviter> GetInviterExl(Guid eventkey)
        {
            string sql = @"select UserType,NormalTicketQuantity,VIPTicketQuantity,DirectSellerId,ConsultantLevelID as Level,c.FirstName,c.LastName,c.PhoneNumber,c.CreatedBy
  from [PB_Event-Consultant] c (NOLOCK) left join ContactsLite.dbo.Consultants co (NOLOCK) on c.ContactId=co.ContactID 
   where c.EventKey=@eventkey and  ConsultantLevelID is not null and (NormalTicketQuantity !=0 or VIPTicketQuantity!=0)  order BY  DirectSellerId";
            DataSet ds = SqlHelper.ExecuteDataset(AppSetting.Community, CommandType.Text, sql, new SqlParameter("@eventkey", eventkey));

            if (ds.Tables.Count > 0)
            {
                string json = JsonConvert.SerializeObject(ds.Tables[0]);
                List<pb_Inviter> c = JsonConvert.DeserializeObject<List<pb_Inviter>>(json);
                return c;
            }
            return new List<pb_Inviter>();
        }

        //得到产品使用数据数据
        public static EventTracking GetTractingInfo(Guid eventKey)
        {
            string sql = @" 
                        select 
                        sum(case when EventId='D265BB3B-B5EC-4DA1-A629-FF8980F774A7' then 1 else 0 end) SendTicketByWechat,
                        sum(case when EventId='D67EC716-3D34-4EDF-BCE5-6C07EBC93553' then 1 else 0 end) SendTicketBySMS,
                        sum(case when EventId='5599B335-36B2-47F5-B8C6-3088B27FA0A2' then 1 else 0 end) InvitBySMS,
                        sum(case when EventId='DD707FE4-350D-43B6-8C60-88EC7B398367' then 1 else 0 end) InvitByWechat
                        from Track_Event (nolock) where Description='{0}'

                        select
                         SUM(case when c.Source =0 then 1 else 0 end) GetTicketByWechat,
                         SUM(case when c.Source =1 then 1 else 0 end) GetTicketBySMS,
                         SUM(case when c.Source =2 then 1 else 0 end) GetTicketByBCInput   
                          from PB_Customer c (nolock)
                        join PB_Ticket t (nolock) on t.CustomerKey=c.CustomerKey
                        where EventKey=@eventkey

                        select 
                         SUM(case when t.UpdatedBy ='ClientCreateNew' then 1 else 0 end) CheckinByLiveInput,
                         SUM(case when t.UpdatedBy ='QRCode' then 1 else 0 end) CheckinByQRcode,
                         SUM(case when t.UpdatedBy ='SMS' then 1 else 0 end) CheckinByToken
                         from PB_Customer c (nolock)
                        join PB_Ticket t (nolock) on c.CustomerKey=t.CustomerKey
                        where EventKey=@eventkey and TicketStatus=5 ";
            sql = string.Format(sql, eventKey.ToString().Replace("-",""));
            DataSet ds = SqlHelper.ExecuteDataset(AppSetting.Community, CommandType.Text, sql, new SqlParameter("@eventkey", eventKey));

            if (ds.Tables.Count > 0)
            {
                EventTracking c = new EventTracking();
                EventTracking c0 = new EventTracking();
                EventTracking c1 = new EventTracking();
                EventTracking c2 = new EventTracking();

                string json = JsonConvert.SerializeObject(ds.Tables[0]);
                c0 = JsonConvert.DeserializeObject<List<EventTracking>>(json)[0];

                string json1 = JsonConvert.SerializeObject(ds.Tables[1]);
                c1 = JsonConvert.DeserializeObject<List<EventTracking>>(json1)[0];

                string json2 = JsonConvert.SerializeObject(ds.Tables[2]);
                c2 = JsonConvert.DeserializeObject<List<EventTracking>>(json2)[0];

                c.SendTicketByWechat = c0.SendTicketByWechat;
                c.SendTicketBySMS = c0.SendTicketBySMS;
                c.InvitBySMS = c0.InvitBySMS;
                c.InvitByWechat = c0.InvitByWechat;
                c.GetTicketByWechat = c1.GetTicketByWechat;
                c.GetTicketBySMS = c1.GetTicketBySMS;
                c.GetTicketByBCInput = c1.GetTicketByBCInput;
                c.CheckinByLiveInput = c2.CheckinByLiveInput;
                c.CheckinByQRcode = c2.CheckinByQRcode;
                c.CheckinByToken = c2.CheckinByToken;
                

                return c;
            }
            return new EventTracking();

        }

        //得到签到者名单来宾信息数据
        public static CheckinUser GetCheckGuest(Guid CustomerKey)
        {
            string sql = "select * from PB_Customer where CustomerKey = @CustomerKey";
             DataSet ds = SqlHelper.ExecuteDataset(AppSetting.Community, CommandType.Text, sql, new SqlParameter("@CustomerKey", CustomerKey));

             if (ds.Tables.Count > 0)
             {
                 CheckinUser CU = new CheckinUser();
                 string json = JsonConvert.SerializeObject(ds.Tables[0]);
                 CU = JsonConvert.DeserializeObject<List<CheckinUser>>(json)[0];
                 return CU;
             }
             return new CheckinUser();
        }

        //得到签到者名单志愿者信息数据
        public static PB_EventConsultant GetCheckinVolunteerInfo(Guid MappingKey)
        {
            string sql = "select * from [PB_Event-Consultant] where MappingKey=@MappingKey";
            DataSet ds = SqlHelper.ExecuteDataset(AppSetting.Community,CommandType.Text,sql,new SqlParameter("@MappingKey",MappingKey));
            if (ds.Tables.Count > 0)
            {
                PB_EventConsultant PE = new PB_EventConsultant();
                string json = JsonConvert.SerializeObject(ds.Tables[0]);
                PE = JsonConvert.DeserializeObject<List<PB_EventConsultant>>(json)[0];
                return PE;
            }
            return new PB_EventConsultant();
        }

        //得到邀约者邀请顾客的名单数据
        public static List<PB_Customer> GetInviterCustomer(string DirectSellerId, Guid eventKey)
        {
            string sql = @"select  t.TicketType,C.CustomerName,C.CustomerPhone from PB_Customer C left join  PB_Ticket T on C.CustomerKey=T.CustomerKey
left join [PB_Event-Consultant]  EC on EC.MappingKey = T.MappingKey
where   t.eventKey = @eventKey and EC.DirectSellerId = @sellerid ";
            DataSet ds = SqlHelper.ExecuteDataset(AppSetting.Community, CommandType.Text, sql, new SqlParameter("@eventKey", eventKey), new SqlParameter("@sellerid",  DirectSellerId));

            if (ds.Tables.Count > 0)
            {
                string json = JsonConvert.SerializeObject(ds.Tables[0]);
                List<PB_Customer> c = JsonConvert.DeserializeObject<List<PB_Customer>>(json);
                return c;
            }
            return new List<PB_Customer>();
        }
    }
}