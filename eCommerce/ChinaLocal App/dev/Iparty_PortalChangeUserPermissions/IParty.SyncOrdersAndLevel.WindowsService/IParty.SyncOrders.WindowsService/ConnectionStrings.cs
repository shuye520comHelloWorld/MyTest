
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IParty.SyncOrdersAndLevel.WindowsService
{
    public static class ConnectionStrings
    {
        public static string Community = ConfigurationManager.AppSettings["Community"];
        public static string ContactsLite = ConfigurationManager.AppSettings["ContactsLite"];
    }

    public static class AppConfigs
    {
        public static string StartDate = ConfigurationManager.AppSettings["StartDate"];
        public static string EndDate = ConfigurationManager.AppSettings["EndDate"];
        public static int PageCount = int.Parse(ConfigurationManager.AppSettings["PageCount"]);
    }
}
