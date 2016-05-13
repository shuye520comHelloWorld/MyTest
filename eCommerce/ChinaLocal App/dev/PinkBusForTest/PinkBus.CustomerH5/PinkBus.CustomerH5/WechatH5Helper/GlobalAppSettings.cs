using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WechatH5Helper
{
    public static class GlobalAppSettings
    {

        #region wechat config

        public static string Wechat_AppID = ConfigurationManager.AppSettings["Wechat_AppID"];
        public static string Wechat_Appsecret = ConfigurationManager.AppSettings["Wechat_Appsecret"];

        public static string ClientID = ConfigurationManager.AppSettings["ClientID"];
        public static string ClientSecret = ConfigurationManager.AppSettings["ClientSecret"];

        public static string OauthTokenUrl = ConfigurationManager.AppSettings["OauthTokenUrl"];

        public static string WechatSiteCallbackUrl = ConfigurationManager.AppSettings["WechatSiteCallbackUrl"];

        public static bool Snsapi_UserinfoForHome = Convert.ToBoolean(ConfigurationManager.AppSettings["Snsapi_UserinfoForHome"]);
        public static bool Snsapi_UserinfoForShare = Convert.ToBoolean(ConfigurationManager.AppSettings["Snsapi_UserinfoForShare"]);


        public static string GetAccessTokenAPIServer = ConfigurationManager.AppSettings["GetAccessTokenAPIServer"];
        public static string SaveAccount = ConfigurationManager.AppSettings["SaveAccount"];
        public static string HttpProtocol = ConfigurationManager.AppSettings["HttpProtocol"];

        public static string WechatBaseScopeUrl
        {
            get
            {
                var openUrl = ConfigurationManager.AppSettings["WechatAPIAuthorize"] + "?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_base&state=mobile#wechat_redirect";
                return string.Format(openUrl, Wechat_AppID, WechatSiteCallbackUrl);
            }
        }

        public static string WechatUserInfoScopeUrl
        {
            get
            {
                var openUrl = ConfigurationManager.AppSettings["WechatAPIAuthorize"] + "?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_userinfo&state=mobile#wechat_redirect";
                return string.Format(openUrl, Wechat_AppID, WechatSiteCallbackUrl);
            }
        }

        public static string getVoteRemindInfo_Url = ConfigurationManager.AppSettings["WX_GetVoteRemindInfo"];
        public static string getVoteRemindInfo_ClientId = ConfigurationManager.AppSettings["WX_GetVoteRemindInfo_ClientId"];
        public static string getVoteRemindInfo_BusinessCode = ConfigurationManager.AppSettings["WX_GetVoteRemindInfo_BusinessCode"];
        public static string getVoteRemindInfo_AccountType = ConfigurationManager.AppSettings["WX_GetVoteRemindInfo_AccountType"];
      
        public static string MsgUrlByConsultant = ConfigurationManager.AppSettings["MsgUrlByConsultant"];
        public static string MsgUrlByCustomer = ConfigurationManager.AppSettings["MsgUrlByCustomer"];
        #endregion

    }
}
