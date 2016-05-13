using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Iparty.SigninUpload.Common
{
    public  class AppSetting
    {
        public static string Community = ConfigurationManager.AppSettings["Community"];
        //public static string ContactsLite = ConfigurationManager.AppSettings["ContactsLite"];
    }
}