using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.CheckInClient.Helper
{
    class AppSetting
    {
        public static string DBPath = AppDomain.CurrentDomain.BaseDirectory + @"Data\";
        public static string DBName = ConfigurationManager.AppSettings["DBName"];
        public static string SyncDataAPI = ConfigurationManager.AppSettings["SyncDataAPI"];
        public static string ClientKey = ConfigurationManager.AppSettings["ClientKey"];
        public static string ClientSecret = ConfigurationManager.AppSettings["ClientSecret"];
        public static string ConsultantImageAPI = ConfigurationManager.AppSettings["ConsultantImageAPI"];
        public static string OauthTokenUrl = ConfigurationManager.AppSettings["OauthTokenUrl"];
        public static int Offset =int.Parse(ConfigurationManager.AppSettings["Offset"]);
        public static int Pivot = int.Parse(ConfigurationManager.AppSettings["Pivot"]);
        public static string DataSource
        {
            get
            {
                return string.Format("data source={0}", DBPath + DBName);
            }
        }
    }
}

