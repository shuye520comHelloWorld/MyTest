using PinkBus.Services.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.Services.Infrastructure.SMS
{
    public static class SMSHelper
    {
        /// <summary>
        /// SMS
        /// </summary>
        public static string SMSUrl = ConfigurationManager.AppSettings["SMSUrl"];
        public static int SMSTypeId = int.Parse(ConfigurationManager.AppSettings["SMSTypeId"]);
      
        public static bool SendSMS(string phonenumber, string smsMessage, string scheduledTime)
        {
            SmsWs.MKSMSWSSoapClient client = new SmsWs.MKSMSWSSoapClient();
            client.Endpoint.Address = new EndpointAddress(SMSUrl);
            int smsTypeId = SMSTypeId;
            return client.CreateNewSMSSender(phonenumber, smsMessage, smsTypeId, scheduledTime);
        }


    }
}
