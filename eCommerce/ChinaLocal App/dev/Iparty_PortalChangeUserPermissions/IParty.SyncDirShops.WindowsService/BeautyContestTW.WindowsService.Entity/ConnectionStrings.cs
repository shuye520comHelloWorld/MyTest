using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace BeautyContestTW.WindowsService.Entity
{
    public class ConnectionStrings
    {
        public static string Contacts = ConfigurationManager.AppSettings["Contacts"]; 
        public static string ContactsLite = ConfigurationManager.AppSettings["ContactsLite"];
        public static string MyCustomer = ConfigurationManager.AppSettings["MyCustomer"];
        public static string Basket = ConfigurationManager.AppSettings["Basket"];
        public static string MKCEBIZAPP = ConfigurationManager.AppSettings["MKCEBIZAPP"];
        public static string Community = ConfigurationManager.AppSettings["Community"];
        public static string Intelligence_FO = ConfigurationManager.AppSettings["Intelligence_FO"];
        public static string VoteFactor = ConfigurationManager.AppSettings["voteFactor"];
        public static string AwardServiceURL = ConfigurationManager.AppSettings["AwardServiceURL"];

        public static string APBeautyContest = ConfigurationManager.AppSettings["APBeautyContest"];
    }
}
