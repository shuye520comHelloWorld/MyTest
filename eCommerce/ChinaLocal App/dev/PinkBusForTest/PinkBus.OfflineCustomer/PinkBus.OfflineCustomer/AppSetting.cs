using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.OfflineCustomer
{
    public class AppSetting
    {
        public static int FormHeight = int.Parse(ConfigurationManager.AppSettings["FormHeight"]);
        public static int FormWidth = int.Parse(ConfigurationManager.AppSettings["FormWidth"]);
        public static string DBPath = AppDomain.CurrentDomain.BaseDirectory + @"Data\";
        public static string DBName = ConfigurationManager.AppSettings["DBName"];
        public static string BaseAPI = ConfigurationManager.AppSettings["BaseAPI"];
        public static string SyncDataAPI = ConfigurationManager.AppSettings["SyncDataAPI"];


        public static string DataSource
        {
            get
            {
                return string.Format("data source={0}", DBPath + DBName);
            }
        }
    }
}
