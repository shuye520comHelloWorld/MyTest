
using Newtonsoft.Json;
using PinkBus.EventManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace PinkBus.EventManagement.Helper
{
    public class HomeDAO
    {
       

        public static int CreateOrUpdateEvent(EventPost Event)
        {
            try
            {
                string sessionXml = Common.SetSessionXml(Event);

                string sql = "dbo.PB_SaveEvent";
                SqlParameter[] Params = { 
                                    new SqlParameter("@EventKey",Event.EventKey),
                                    new SqlParameter("@EventTiTle",Event.EventTitle),
                                    new SqlParameter("@EventLocation",Event.EventLocation),
                                    new SqlParameter("@EventStartDate",Event.EventStartDate),
                                    new SqlParameter("@EventEndDate",Event.EventEndDate),
                                    new SqlParameter("@InvitationEndDate",Event.InvitationEndDate),
                                    new SqlParameter("@InvitationStartDate",Event.InvitationStartDate.HasValue?(object)Event.InvitationStartDate.Value:DBNull.Value),
                                    new SqlParameter("@ApplyTicketStartDate",Event.ApplyTicketStartDate),
                                    new SqlParameter("@ApplyTicketEndDate",Event.ApplyTicketEndDate),
                                    new SqlParameter("@CheckinStartDate",Event.CheckinStartDate),
                                    new SqlParameter("@CheckinEndDate",Event.CheckinEndDate),
                                    new SqlParameter("@DownloadToken",Event.DownloadToken),
                                    new SqlParameter("@UploadToken",Event.UploadToken),
                                    new SqlParameter("@CreatedDate",Event.CreatedDate),
                                    new SqlParameter("@CreatedBy",Event.CreatedBy),
                                    new SqlParameter("@UpdatedDate",Event.UpdatedDate),
                                    new SqlParameter("@UpdatedBy",Event.UpdatedBy),
                                    new SqlParameter("@SessionXML",sessionXml)
                                    };
                int result = SqlHelper.ExecuteNonQuery(AppSetting.Community, CommandType.StoredProcedure, sql, Params);
                return result;
            }
            catch (Exception ex)
            {

                return -1;
            }
        }

        public static int UpdateEventTitleAndLocation(EventPost Event)
        {
            string sql = "dbo.PB_UpdateEventTitleAndLocation";
            SqlParameter[] Params = { 
                                    new SqlParameter("@EventKey",Event.EventKey),
                                    new SqlParameter("@EventTiTle",Event.EventTitle),
                                    new SqlParameter("@EventLocation",Event.EventLocation)
                                    };
            int result = SqlHelper.ExecuteNonQuery(AppSetting.Community, CommandType.StoredProcedure, sql, Params);
            return result;
        }

        public static bool CheckPassToken(string token)
        {
            string sql = "select 1 from PB_Event (nolock) where DownloadToken=@token or UploadToken = @token";
            SqlDataReader result = SqlHelper.ExecuteReader(AppSetting.Community, CommandType.Text, sql, new SqlParameter("@token", token));
            if (result.HasRows)
            {
                result.Close();
                result.Dispose();
                return true;
            }
            return false;
        }

        public static List<PB_Event> getEvents(SearchModel SM)
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
                ,new SqlParameter("@Descript",SqlDbType.NVarChar,100)
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
                    List<PB_Event> Events = JsonConvert.DeserializeObject<List<PB_Event>>(json);
                    return Events;
                }
                SM.TotalRow = 0;
                return null;
            }catch(Exception ex){
                LogHelper.Debug(ex.ToString());
                return null;
            }
        }

        public static List<PB_EventSession> getSessions(string EventKeys)
        {
            string sql = "select * from PB_EventSession (nolock) where eventkey in ( '" + EventKeys + "' )";
            DataSet ds = SqlHelper.ExecuteDataset(AppSetting.Community, CommandType.Text, sql);
            if (ds.Tables.Count > 0)
            {
                string json = JsonConvert.SerializeObject(ds.Tables[0]);
                List<PB_EventSession> Sessions = JsonConvert.DeserializeObject<List<PB_EventSession>>(json);
                return Sessions;
            }

            return null;
        }

        public static List<EventConsultant> getConsultantInfo(string ids,Guid EventKey)
        {
            string sql = "PB_ConsultantInfo";
            DataSet ds = SqlHelper.ExecuteDataset(AppSetting.Community, CommandType.StoredProcedure, sql, new SqlParameter("@IdsXml", ids), new SqlParameter("@EventKey", EventKey));
            if (ds.Tables.Count > 0)
            {
                string json = JsonConvert.SerializeObject(ds.Tables[0]);
                List<EventConsultant> c = JsonConvert.DeserializeObject<List<EventConsultant>>(json);
                return c;
            }
            return new List<EventConsultant>();
        }

        public static bool saveConsultants(ConsultantPost CP)
        {
            try
            {
                string sql = "dbo.PB_Consultant_TicketSetting_save";
                SqlParameter[] Params = {
                new SqlParameter("@EventKey",CP.EventKey),
                new SqlParameter("@ApplyTicketTotal",CP.ApplyTicketTotal),
                new SqlParameter("@TicketQuantityPerSession",CP.TicketQuantityPerSession),
                new SqlParameter("@NormalTicketQuantity",CP.NormalTicketQuantityPerPerson),
                new SqlParameter("@VIPTicketQuantity",CP.VIPTicketQuantityPerPerson),
                new SqlParameter("@VolunteerNormalTicketCountPerPerson",CP.NormalTicketQuantityPerPerson),
                new SqlParameter("@VolunteerVIPTicketCountPerPerson",CP.VIPTicketQuantityPerPerson),
                new SqlParameter("@NormalTicketSettingQuantity",CP.NormalTicketQuantityPerPerson),
                new SqlParameter("@VIPTicketSettingQuantity",CP.VIPTicketQuantityPerPerson),
      
                new SqlParameter("@CreatedDate",CP.CreatedDate),
                new SqlParameter("@CreatedBy",CP.CreatedBy),
                new SqlParameter("@EventSessionsXML",CP.sessionkeysXml),
                new SqlParameter("@EventConsultantsXML",CP.consultantsXml),
                new SqlParameter("@EventTicketsXML",CP.ticketsXml),
                new SqlParameter("@ImportType",CP.Type)

            };
                DataSet ds = SqlHelper.ExecuteDataset(AppSetting.Community, CommandType.StoredProcedure, sql, Params);
                if (ds.Tables.Count > 0)
                {
                    return int.Parse(ds.Tables[0].Rows[0][0].ToString())>0;
                }
                return false;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static DataSet queryEventConsultant(Guid EventKey, EventUserType usertype)
        {
            string sql = "select * from [Community].[dbo].[PB_ConsultantSnapshot] (nolock) where UserType=@usertype and EventKey=@eventkey   order by IsLiveAdd, DirectSellerID ";
            sql += " select * from PB_EventTicketSetting where  EventKey=@eventkey";
            DataSet ds = SqlHelper.ExecuteDataset(AppSetting.Community, CommandType.Text, sql, new SqlParameter("@usertype", (short)usertype), new SqlParameter("@eventkey", EventKey));
            return ds;
           
        }

        public static List<Ticket> queryApplyTickets(Guid EventKey)
        {
            string sql = "select * from [Community].[dbo].PB_Ticket (nolock) where EventKey=@eventkey and (TicketStatus =0 or TicketStatus=1 or TicketStatus=2)";
            DataSet ds = SqlHelper.ExecuteDataset(AppSetting.Community, CommandType.Text, sql, new SqlParameter("@eventkey", EventKey));
            if (ds.Tables.Count > 0)
            {
                string json = JsonConvert.SerializeObject(ds.Tables[0]);
                List<Ticket> c = JsonConvert.DeserializeObject<List<Ticket>>(json);
                return c;
            }
            return null;
        }

        
    }
}