using iParty.Services.ORM;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Services.Infrastructure
{
    public static class MessageSender
    {
        private const string SPMESSAGESEND = "exec uspNewSMS @ContactId,@Message,@Typeid";
        public static void Send(long contactId, string message, int typeid)
        {
            if (ConfigurationManager.AppSettings["SendSMS"] == "true")
            {
                var operation = new BaseEntityOperation<long>(ConfigurationManager.AppSettings["MKCEBIZAPP"]);

                var result = operation.ExecEntityProcedure(SPMESSAGESEND, new { ContactId = contactId, Message = message, Typeid = typeid });
            }


        }
    }
}
