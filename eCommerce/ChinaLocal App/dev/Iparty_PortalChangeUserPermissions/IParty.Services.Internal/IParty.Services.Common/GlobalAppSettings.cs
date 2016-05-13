using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IParty.Services.Common
{

    public class GlobalAppSettings
    {
        //public static string ContactsLite = ConfigurationManager.AppSettings["ContactsLite"];
        public static string Community = ConfigurationManager.AppSettings["Community"];
        ///public static string Contacts = ConfigurationManager.AppSettings["Contacts"];
        public static string EnableOAuthATValidation = ConfigurationManager.AppSettings["EnableOAuthATValidation"];
        public static string OAuthServiceURL = ConfigurationManager.AppSettings["OAuthServiceURL"];
        public static string ValidateCommandServiceURL = ConfigurationManager.AppSettings["ValidateCommandServiceURL"];
        public static string EnableCommandContextValidation = ConfigurationManager.AppSettings["EnableCommandContextValidation"];

      
      
      
       
    }
}
