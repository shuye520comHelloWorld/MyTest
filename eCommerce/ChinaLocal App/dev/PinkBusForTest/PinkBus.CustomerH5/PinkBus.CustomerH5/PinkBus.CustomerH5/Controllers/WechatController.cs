using PinkBus.CustomerH5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WechatH5Helper;
namespace PinkBus.CustomerH5.Controllers
{
    public class WechatController : BaseController
    {
        public ActionResult Callback()
        {
            
            var code = Request.QueryString["code"];
            var returnUrl = Request.QueryString["returnurl"];
            var scope = Request.QueryString["scope"];
            int retry = string.IsNullOrEmpty(Request.QueryString["retry"]) ? 0 : Convert.ToInt32(Request.QueryString["retry"]);
            returnUrl = WechatHelper.Base64ToString(returnUrl);
            //测试用数据：
            //code = "xxxx";
            //returnUrl = "/Home/Index";
            //scope = "2";
            ViewData["returnUrl"] = returnUrl;
            LogHelper.Debug("callback returnUrl:" + returnUrl);
            LogHelper.Debug("callback code:" + code);
            OauthAuthorizeCodeLogin UserInfo = new OauthAuthorizeCodeLogin();           
            if (scope == "1")
            {
                UserInfo = WechatHelper.getUserInfoByOauthAPI(code);
            }
            else
            {
                var baseInfo = WechatHelper.getBaseInfoByOauthAPI(code);
                UserInfo.access_token = baseInfo.access_token;
                UserInfo.userInfo = new userInfo { openid = baseInfo.openid, unionid = baseInfo.unionid };
                UserInfo.errcode = baseInfo.errcode;
                UserInfo.errmsg = baseInfo.errmsg;
            }
            if (!string.IsNullOrEmpty(UserInfo.errcode))
            {
                LogHelper.Debug("UserInfo.errcode:" + UserInfo.errcode + "returnUrl=" + returnUrl);
                if (UserInfo.errcode == "101006")
                {
                    if (retry == 2)
                    {
                        LogHelper.Debug("errcode retry == 2" + UserInfo.errcode);
                        return Redirect("~/Views/Shared/WeChatError.cshtml");
                    }
                    else
                    {
                        ++retry;
                        if (returnUrl.IndexOf("?retry=") > -1)
                        {
                            returnUrl = returnUrl.Substring(0, returnUrl.IndexOf("?retry="));
                            LogHelper.Debug("new returnUrl" + returnUrl);
                        }
                        return Redirect(returnUrl + "?retry=" + retry.ToString());
                    }
                }              
                return Redirect(returnUrl);
            }
            ViewData["nickname"] = UserInfo.userInfo.nickname??" ";
            ViewData["headimgurl"] = UserInfo.userInfo.headimgurl ?? " ";
            UserInfo.isLoginUserInfo = true;          
            ViewData["unionID"] = UserInfo.userInfo.unionid;

            if (UserInfo.errcode == null)
            {
                UserInfo.access_token = "";
                AuthorizeUser = UserInfo;
            }
            return View();
        }

        public ActionResult WechatAuthorize(int type)
        {
            if (WechatHelper.IsWechatBrowser())
            {
                var url = "";
                int retry = string.IsNullOrEmpty(Request.QueryString["retry"]) ? 0 : Convert.ToInt32(Request.QueryString["retry"]);
                LogHelper.Debug("HomeController,retry:" + retry.ToString());
                var returnUrl = Request.UrlReferrer == null ? Request.QueryString["ReturnUrl"] : Request.UrlReferrer.AbsoluteUri;
                LogHelper.Debug("returnUrl:" + returnUrl);
                returnUrl = HttpUtility.UrlEncode(WechatHelper.StringToBase64(returnUrl));
                //手动授权
                if (type == 1)
                {
                    url = WechatHelper.WechatRedirect(1, returnUrl, retry);
                }
                else  //静默授权
                {
                    url = WechatHelper.WechatRedirect(2, returnUrl, retry);
                }

                LogHelper.Debug("open url: "+url);
                return Redirect(url);
            }
            else
            {

                return View("~/Views/Shared/WeChatError.cshtml");
            }
        }

        public JsonResult GetAccessToken()
        {
           
            GetAccessTokenModel accessToken = GetAT();
           // LogHelper.Warn("accessToken:" + accessToken == null ? "null" : accessToken.ErrorCode + accessToken.access_token);
            if (accessToken.ErrorCode == "100035")
            {
                string url =WechatH5Helper.GlobalAppSettings.SaveAccount;
                var httpParams = new Dictionary<string, string>();
                httpParams.Add("client_id", GlobalAppSettings.ClientID);
                httpParams.Add("client_secret", GlobalAppSettings.ClientSecret);
                httpParams.Add("account_id", AuthorizeUser.userInfo.unionid);//
                httpParams.Add("account_type", "wechat");
                var SaveAccountModel = ServiceHttpRequest.Post<SaveAccountModel>(url, httpParams);
                accessToken = GetAT();
            }
            else
            {
                LogHelper.Warn("GetAccessToken ErrorCode:" + accessToken.ErrorCode + "\r\n AccessToken:" + accessToken.access_token);
            }
            return Json(new { accessToken = accessToken.access_token }, JsonRequestBehavior.AllowGet);
        }

        private GetAccessTokenModel GetAT()
        {
          
            string url = GlobalAppSettings.GetAccessTokenAPIServer;
            var httpParams = new Dictionary<string, string>();
            httpParams.Add("grant_type", "password");
            httpParams.Add("client_id", GlobalAppSettings.ClientID);
            httpParams.Add("client_secret", GlobalAppSettings.ClientSecret);
            httpParams.Add("username", "[wechat]:" + AuthorizeUser.userInfo.unionid);// 
            httpParams.Add("password", "wechat");
            
            return ServiceHttpRequest.Post<GetAccessTokenModel>(url, httpParams);
        }

        public JsonResult GetWechatConfig(string url)
        {
            var jsSign = WechatHelper.getWechatJsSignature(HttpUtility.UrlEncode(url));

            if (jsSign.errcode == null)
            {
                return Json(new
                {
                    result = true,
                    appId = GlobalAppSettings.Wechat_AppID,
                    noncestr = jsSign.noncestr,
                    timestamp = jsSign.timestamp,
                    signature = jsSign.signature,
                    //url = url
                }, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { result = false, msg = jsSign.errmsg }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUnionId()
        {
            var unionid = AuthorizeUser == null ? "" : AuthorizeUser.userInfo.unionid;

            return Json(new { unionId = unionid }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CheckWechatRemind()
        {
            bool isCheck = false;
            if (Request.Url.Host.ToLower().Contains("dev") ||
                Request.Url.Host.ToLower().Contains("qa") ||
                Request.Url.Host.ToLower().Contains("uat"))
            {
                isCheck = false;
            }
            else {
                isCheck = CheckUserWeChatRemind();
            }
          
           
            return Json(new { isCheck = isCheck }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// check this user is attention marykay service
        /// </summary>
        /// <returns></returns>
        private bool CheckUserWeChatRemind()
        {
            // return false;
            var resu = false;
            string unionid = AuthorizeUser.userInfo.unionid;
            string url = GlobalAppSettings.getVoteRemindInfo_Url;
            Dictionary<string, string> httpPramary = new Dictionary<string, string>();
            httpPramary.Add("ClientID", GlobalAppSettings.getVoteRemindInfo_ClientId);
            httpPramary.Add("BusinessCode", GlobalAppSettings.getVoteRemindInfo_BusinessCode);
            httpPramary.Add("AccountType", GlobalAppSettings.getVoteRemindInfo_AccountType);
            httpPramary.Add("AccountIds", unionid);
            var response = ServiceHttpRequest.Post<VoteRemindModel>(url, httpPramary);
            if (response != null && response.UserProfiles != null && response.UserProfiles.Count > 0 && response.UserProfiles[0].IsSubscribe)
            {
                resu = true;
            }
            return resu;
        }
    }
}
