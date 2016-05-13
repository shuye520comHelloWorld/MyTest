using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.ApplyBCTicket.WindowsService
{
    public class AppSetting
    {
        public static string Community = ConfigurationManager.AppSettings["Community"];
        public static int PendingSize = int.Parse(ConfigurationManager.AppSettings["PendingSize"]);

    }
}
