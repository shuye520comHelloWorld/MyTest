using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace IParty.SyncDirShops.WindowsService
{
    public class ConnectionStrings
    {
        public static string Community = ConfigurationManager.AppSettings["Community"];
        public static string DirShops = ConfigurationManager.AppSettings["DirShops"];
        public static string ContactsLite = ConfigurationManager.AppSettings["ContactsLite"];
        public static string PageCount = ConfigurationManager.AppSettings["PageCount"];
       
    }
}
