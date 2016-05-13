using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace PinkBus.EventManagement.Helper
{
    public class AppSetting
    {
        public static string Community = ConfigurationManager.AppSettings["Community"];
        public static string JsMini = ConfigurationManager.AppSettings["JsMini"];
        public static int EventPageSize = int.Parse(ConfigurationManager.AppSettings["EventPageSize"]);
        public static string Application = ConfigurationManager.AppSettings["Application"];
        public static string Ver = ConfigurationManager.AppSettings["Ver"];
    }
}