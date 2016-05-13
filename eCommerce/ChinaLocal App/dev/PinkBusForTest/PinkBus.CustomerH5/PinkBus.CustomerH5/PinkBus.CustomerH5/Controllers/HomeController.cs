using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WechatH5Helper;
namespace PinkBus.CustomerH5.Controllers
{
    public class HomeController : BaseController
    {
        //[Authorize]
        public ActionResult Index()
        {
          
          //  LogHelper.Debug("1:" + Request.UserAgent);

            if (WechatHelper.IsWechatBrowser() && AuthorizeUser == null)
            {


                var url = "";
                var returnUrl = Request.Url.AbsoluteUri;
                if (returnUrl.IndexOf("://") > 0)
                {
                    string[] newReturnArrUrl = returnUrl.Split(':');
                    newReturnArrUrl[0] =GlobalAppSettings.HttpProtocol;

                    returnUrl = string.Join(":", newReturnArrUrl);
                }
                LogHelper.Debug(returnUrl);
                LogHelper.Debug("port:" + Request.Url.Port + "url=" + returnUrl);
                int retry = string.IsNullOrEmpty(Request.QueryString["retry"]) ? 0 : Convert.ToInt32(Request.QueryString["retry"]);
                returnUrl = HttpUtility.UrlEncode(WechatHelper.StringToBase64(returnUrl));               
                url = WechatHelper.WechatRedirect(2, returnUrl, retry);

                LogHelper.Debug("returnUrl:"+url);
                return Redirect(url);
            }
            ViewData["unionid"] = AuthorizeUser == null ? "" : AuthorizeUser.userInfo.unionid;
            //ViewData["headImgUrl"] = AuthorizeUser == null ? "" : AuthorizeUser.userInfo.headimgurl;
            return View();
        }

        public ActionResult Success()
        {
            return View();
        }


    }
}
