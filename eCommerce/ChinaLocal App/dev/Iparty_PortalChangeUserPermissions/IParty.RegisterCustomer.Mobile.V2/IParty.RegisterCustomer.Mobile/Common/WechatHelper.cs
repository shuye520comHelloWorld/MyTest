using IParty.RegisterCustomer.Mobile.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IParty.RegisterCustomer.Mobile.Common
{
    public class WechatHelper
    {


        public static bool WachatJSDKCanUse()
        {
            string userAgent = System.Web.HttpContext.Current.Request.UserAgent.ToLower();
            if (userAgent.Contains("micromessenger"))
            {
                var uas = userAgent.Split(' ');
                foreach (var item in uas)
                {
                    if (item.ToLower().Contains("micromessenger"))
                    {
                        var i = item.Split('/');
                        var version = i[1].Split('.')[0];
                        int ver = 0;
                        int.TryParse(version, out ver);
                        if (ver >= 6)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static bool IsWechatBrowser()
        {
            string userAgent = System.Web.HttpContext.Current.Request.UserAgent.ToLower();
            if (userAgent.Contains("micromessenger"))
                return true;
            else
                return false;
        }

        public static string EncodeByBase64(string stringParam)
        {
            byte[] abytes = Encoding.Default.GetBytes(stringParam);
            return Convert.ToBase64String(abytes);

        }

        /// <summary>
        /// 通过oauthAPI获取用户信息 userinfo scope
        /// </summary>
        /// <param name="code">wechat_code</param>
        /// <returns></returns>
        public static OauthAuthorizeCodeLogin getUserInfoByOauthAPI(string code)
        {
            Dictionary<string, string> httpParms = new Dictionary<string, string>();
            httpParms.Add("wechat_code", code);
            httpParms.Add("wechat_app_id", ConfigurationManager.AppSettings["AppID"]);
            httpParms.Add("client_id", ConfigurationManager.AppSettings["ClientID"]);
            httpParms.Add("client_secret", ConfigurationManager.AppSettings["ClientSecret"]);

            string Url = ConfigurationManager.AppSettings["OauthWechatUrl"] + "wechat/authorization-code/login";
            var info = ServiceHttpRequest.Post<OauthAuthorizeCodeLogin>(Url, httpParms);
            return info;
        }


        /// <summary>
        /// 通过oauthAPI获取unionid base scope
        /// </summary>
        /// <param name="code">wechat_code</param>
        /// <returns></returns>
        public static WechatLoginToken getUserUnionIdByOauthApi(string code)
        {
            var OauthToken = getOauthToken();
            if (OauthToken != null && OauthToken.ErrorCode == null)
            {
                string OauthUrl = ConfigurationManager.AppSettings["OauthWechatUrl"] +
                   "wechat/user-tokens?app_id=" + ConfigurationManager.AppSettings["AppID"] + "&code=" + code;
                var httpHeaders = new Dictionary<string, string>();
                httpHeaders.Add("mk_access_token", OauthToken.access_token);

                WechatLoginToken res = ServiceHttpRequest.Get<WechatLoginToken>(OauthUrl, null, httpHeaders);
                return res;
            }
            LogHelper.DebugErr("JsSignature err :" + OauthToken == null ? "OauthToken is null" : OauthToken.ErrorCode);
            return new WechatLoginToken { errcode = "-1", errmsg = "JsSignature err :" + OauthToken == null ? "OauthToken is null" : OauthToken.ErrorCode };

        }

        //oauth 校验
        private static GetTokenByClientId getOauthToken()
        {
            var httpParams = new Dictionary<string, string>();
            httpParams.Add("grant_type", "client_credentials");

            var httpHeaders = new Dictionary<string, string>();
            var authorization = "Basic " + WechatHelper.EncodeByBase64(ConfigurationManager.AppSettings["ClientID"] + ":" + ConfigurationManager.AppSettings["ClientSecret"]);
            Dictionary<string, string> headerParams = new Dictionary<string, string>();
            headerParams.Add("Authorization", authorization);
            var OauthToken = ServiceHttpRequest.Post<GetTokenByClientId>(ConfigurationManager.AppSettings["OauthTokenUrl"], httpParams, headerParams);
            return OauthToken;
        }

        /// <summary>
        /// 获取注册微信jsdk signature
        /// </summary>
        /// <param name="url">当前页面URL</param>
        /// <returns></returns>
        public static WechatSignature getWechatJsSignature(string url)
        {
            var OauthToken = getOauthToken();
            if (OauthToken != null && OauthToken.ErrorCode == null)
            {
                string OauthUrl = ConfigurationManager.AppSettings["OauthWechatUrl"] +
                    "/wechat/jssdk/signature";
                var httpHeaders = new Dictionary<string, string>();
                httpHeaders.Add("mk_access_token", OauthToken.access_token);

                var httpParams = new Dictionary<string, string>();
                httpParams.Add("wechat_app_id", ConfigurationManager.AppSettings["AppID"]);
                httpParams.Add("url", url);
                WechatSignature res = ServiceHttpRequest.Post<WechatSignature>(OauthUrl, httpParams, httpHeaders);
                return res;
            }

            LogHelper.DebugErr("JsSignature err :" + OauthToken == null ? "OauthToken is null" : OauthToken.ErrorCode);
            return new WechatSignature { errcode = "-1", errmsg = "JsSignature err :" + OauthToken == null ? "OauthToken is null" : OauthToken.ErrorCode };
        }

        /// <summary>
        /// 获取卡券signature
        /// </summary>
        /// <param name="CardId">cardid</param>
        /// <param name="CardCode">cardcode</param>
        /// <returns></returns>
        public static WechatSignature getWechatCardSignature(string CardId, string CardCode)
        {
            var OauthToken = getOauthToken();
            if (OauthToken != null && OauthToken.ErrorCode == null)
            {
                string url = ConfigurationManager.AppSettings["OauthWechatUrl"] +
                    "/wechat/card/signature?wechat_app_id=" + ConfigurationManager.AppSettings["AppID"] + "&card_id=" + CardId + "&card_code=" + CardCode;
                var httpHeaders = new Dictionary<string, string>();
                httpHeaders.Add("mk_access_token", OauthToken.access_token);

                var res = ServiceHttpRequest.Get<WechatSignature>(url, null, httpHeaders);
                return res;
            }
            LogHelper.DebugErr("CardSignature err :" + OauthToken == null ? "OauthToken is null" : OauthToken.ErrorCode);
            return new WechatSignature { errcode = "-1", errmsg = "CardSignature err :" + OauthToken == null ? "OauthToken is null" : OauthToken.ErrorCode };
        }

    }


}
