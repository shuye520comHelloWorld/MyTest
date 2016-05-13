using PinkBus.Services.Common;
using PinkBus.Services.Contract;
using ServiceStack.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace PinkBus.Services.OAuth.Filters
{
    public static class GlobalRequestFilters
    {
        public static void ValidateCommandId(IRequest req, IResponse res, object dto) 
        {
            if (GlobalAppSettings.EnableCommandContextValidation != "true") return;
            if (!(dto is CommandRequestDto)) return;
            CommandRequestDto reqdto = dto as CommandRequestDto;
            if (reqdto.CommandId == Guid.Empty || reqdto.CommandId == null)
            {
                throw new CommandException("Missing mandatory Commandid! ");
            }
            else
            {
                try
                {
                    var url = GlobalAppSettings.ValidateCommandServiceURL;
                    var httpParams = new Dictionary<string, string>();
                    httpParams.Add("CommandId", reqdto.CommandId.ToString());
                    var respose = ServiceHttpRequest.Post<CommandValidateResponse>(url, httpParams);
                    if (!respose.Result) throw new CommandException(respose.Message);
                }
                catch (Exception ex)
                {
                    throw new CommandException(ex.Message, ex.InnerException);
                }
            }
        }

        public static void ValidateContext(IRequest req, IResponse res, object dto) 
        {
            if (GlobalAppSettings.EnableCommandContextValidation != "true") return;
            if (!(dto is BaseRequestDto)) throw new RequestErrorException();
            var context = dto as BaseRequestDto;
            if (string.IsNullOrWhiteSpace(context.Device)) 
            {
                throw new ArgumentNullException("Device");
            }
            if (string.IsNullOrWhiteSpace(context.DeviceOS))
            {
                throw new ArgumentNullException("DeviceOS");
            }
            if (string.IsNullOrWhiteSpace(context.DeviceVersion))
            {
                throw new ArgumentNullException("DeviceVersion");
            }
        }
    }
}
