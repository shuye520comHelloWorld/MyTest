using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace PinkBus.SendActivitySMS.WindowsService
{
    public class ActivitySMS
    {
        public ActivitySMS()
        {
            string CurrentTime = DateTime.UtcNow.AddHours(8).ToString();
            QueryActivitySql = string.Format(QueryActivitySql, CurrentTime, SendStartTime, SendEndTime);
        }
        public Logger _logger = LogManager.GetCurrentClassLogger();
        public readonly string QueryActivitySql = "select EventKey,EventTitle,EventLocation,CheckinStartDate from [dbo].[PB_Event] " +
                                                  "where DATEDIFF(HH,'{0}',CheckinStartDate)<={1} and DATEDIFF(HH,'{0}',CheckinStartDate)>{2}";
        public readonly string sqlconn = ConfigurationManager.AppSettings["Community"];
        public readonly string SendStartTime = ConfigurationManager.AppSettings["SendStartTime"];
        public readonly string SendEndTime = ConfigurationManager.AppSettings["SendEndTime"];
        public readonly string SMSContent = ConfigurationManager.AppSettings["SMSContent"];
        public readonly string getActivityCustomer_SP = "[dbo].[PB_ActivityCustomerSelect]";
        public readonly string updateSendSMSCustomerStatus_SP = "[dbo].[PB_SendSMSCustomerStatusUpdate]";
        public readonly string SMSTypeId = ConfigurationManager.AppSettings["SMSTypeId"];
        public readonly string SendSMSWCFUrl = ConfigurationManager.AppSettings["SendSMSWCFUrl"];


        public void Send()
        {
            try
            {
                var activityList = QueryActivity();
                //var activityList = new List<PB_Activity>();
                //activityList.Add(new PB_Activity()
                //{
                //    EventKey = new Guid("9B797C7F-4677-4D3A-A1DB-4FEC825B1841"),
                //    CheckinStartDate = "2016-1-14 12:12:12.000",
                //    EventLocation = "上海上海上海上海上海上海上海上海上海上海上海上海",
                //    EventTitle = "上海上海上海"
                //});
                _logger.Debug("activity count=" + activityList.Count);
                if (activityList.Count == 0) {                   
                    return;
                } 
                var smsClient = new DEVMKSMSWS.MKSMSWSSoapClient();
                smsClient.Endpoint.Address = new EndpointAddress(SendSMSWCFUrl);              
                foreach (PB_Activity activity in activityList)
                {
                    bool tag = true;
                    while (tag)
                    {
                        var customerDS = SqlHelper.ExecuteDataset(sqlconn, CommandType.StoredProcedure, getActivityCustomer_SP, new SqlParameter[] { new SqlParameter("@eventKey", activity.EventKey) });
                        if (customerDS.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < customerDS.Tables[0].Rows.Count; i++)
                            {
                                bool sendResult = false;
                                try
                                {
                                    sendResult = smsClient.CreateNewSMSSender(
                                       customerDS.Tables[0].Rows[i]["CustomerPhone"].ToString()
                                       , string.Format(SMSContent
                                                   , customerDS.Tables[0].Rows[i]["CustomerName"].ToString()
                                                   , customerDS.Tables[0].Rows[i]["TicketType"].ToString()
                                                   , customerDS.Tables[0].Rows[i]["SMSToken"].ToString()
                                                   , Convert.ToDateTime(customerDS.Tables[0].Rows[i]["SessionStartDate"].ToString()).ToString("yyyy.MM.dd HH:mm", CultureInfo.InvariantCulture)
                                                   , activity.EventLocation
                                                   )
                                       , int.Parse(SMSTypeId)
                                       , null);

                                }
                                catch (Exception ex)
                                {
                                    sendResult = false;
                                    string parameters = "phoneNumber=" + customerDS.Tables[0].Rows[i]["CustomerPhone"].ToString()
                                                        + " smsmessage=" + string.Format(SMSContent
                                                   , customerDS.Tables[0].Rows[i]["CustomerName"].ToString()
                                                   , customerDS.Tables[0].Rows[i]["TicketType"].ToString()
                                                   , customerDS.Tables[0].Rows[i]["SMSToken"].ToString()
                                                   , customerDS.Tables[0].Rows[i]["SessionStartDate"].ToString()
                                                   , activity.EventLocation
                                                   )
                                                        + " typeid=" + SMSTypeId
                                                        + " scheduletime=null";
                                    _logger.Error("CreateNewSMSSender Error parameters:" + parameters + " Exception=" + ex.StackTrace + ex.Message);
                                    _logger.Debug("CreateNewSMSSender debug parameters:" + parameters + "Exception=" + ex.StackTrace + ex.Message);
                                }
                                if (sendResult)
                                {
                                    UpdateCustomerSMS(customerDS.Tables[0].Rows[i]["CustomerKey"].ToString(), "success", 1);

                                }
                            }

                        }
                        else
                        {
                            tag = false;
                            UpdateCustomerSMS(activity.EventKey, "failed", 2);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                _logger.Error("Send Error Exception=" + ex.StackTrace + ex.Message);
                _logger.Debug("Send debug Exception=" + ex.StackTrace + ex.Message);
            }

        }
        private void UpdateCustomerSMS(object CustomerKey, object status, object whereType)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(sqlconn, CommandType.StoredProcedure, updateSendSMSCustomerStatus_SP, new SqlParameter[]{
                                  new SqlParameter("@key",CustomerKey==null?DBNull.Value:CustomerKey)
                                 ,new SqlParameter("@smsStatus",status)
                                 ,new SqlParameter("@whereType",whereType)
                            });
            }
            catch (Exception ex)
            {
                string parameters = string.Format("key={0},smsStatus={1},whereType={2}", CustomerKey, status, whereType);
                _logger.Error("UpdateCustomerSMS Error spName=" + updateSendSMSCustomerStatus_SP
                    + "parameters=" + parameters
                    + " Exception=" + ex.StackTrace + ex.Message);
                _logger.Debug("UpdateCustomerSMS debug spName=" + updateSendSMSCustomerStatus_SP
                     + "parameters=" + parameters
                    + "Exception=" + ex.StackTrace + ex.Message);
            }

        }
        private List<PB_Activity> QueryActivity()
        {
            List<PB_Activity> activityList = new List<PB_Activity>();
            try
            {
                DataSet activityDS = SqlHelper.ExecuteDataset(sqlconn, CommandType.Text, QueryActivitySql);
                if (activityDS.Tables[0].Rows.Count != 0)
                {
                    for (int i = 0; i < activityDS.Tables[0].Rows.Count; i++)
                    {
                        activityList.Add(new PB_Activity()
                        {
                            EventKey = new Guid(activityDS.Tables[0].Rows[i]["EventKey"].ToString()),
                            EventTitle = activityDS.Tables[0].Rows[i]["EventTitle"].ToString(),
                            EventLocation = activityDS.Tables[0].Rows[i]["EventLocation"].ToString(),
                            CheckinStartDate = activityDS.Tables[0].Rows[i]["CheckinStartDate"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("UpdateCustomerSMS Error sqlText=" + QueryActivitySql
                    + " Exception=" + ex.StackTrace + ex.Message);
                _logger.Debug("UpdateCustomerSMS debug sqlText=" + QueryActivitySql
                    + "Exception=" + ex.StackTrace + ex.Message);
            }


            return activityList;
        }
    }
}
