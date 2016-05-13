using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IParty.RegisterCustomer.Mobile.Common
{
    public class GlobalSettings
    {
        public static string WechatSiteCallbackUrl = ConfigurationManager.AppSettings["WechatSiteCallbackUrl"];
        public static string PartyAPIUrl = ConfigurationManager.AppSettings["PartyAPIUrl"];
        public static string ShareTitle = ConfigurationManager.AppSettings["ShareTitle"];
        public static string ShareDesc = ConfigurationManager.AppSettings["ShareDesc"];
        public static string ShareImg = ConfigurationManager.AppSettings["ShareImg"];
        public static string ShareHost = ConfigurationManager.AppSettings["ShareHost"];
        public static string apiHost = ConfigurationManager.AppSettings["apiHost"];
        public static string apiUrl = ConfigurationManager.AppSettings["apiUrl"];
        public static string AppID = ConfigurationManager.AppSettings["AppID"];

        public static string OpenWechatUrl
        {
            get
            {
                var redirectUrl = WechatSiteCallbackUrl;
                var openUrl = ConfigurationManager.AppSettings["OpenWechatUrl"] + "?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_base&state=mobile#wechat_redirect";
                return string.Format(openUrl, AppID, redirectUrl);

            }
        }

    }
}
