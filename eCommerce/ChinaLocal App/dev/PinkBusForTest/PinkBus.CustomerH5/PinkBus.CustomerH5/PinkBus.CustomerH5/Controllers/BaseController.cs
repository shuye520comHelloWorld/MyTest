using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WechatH5Helper;

namespace PinkBus.CustomerH5.Controllers
{
    public class BaseController : Controller
        {

            public BaseController()
            {

            }

            public OauthAuthorizeCodeLogin AuthorizeUser
            {
                get
                {
                    try
                    {
                        if (System.Web.HttpContext.Current.User == null) return null;
                        if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated == false) return null;
                        var userData = ((System.Web.Security.FormsIdentity)System.Web.HttpContext.Current.User.Identity).Ticket.UserData;
                        return string.IsNullOrWhiteSpace(userData) ? null : JsonConvert.DeserializeObject<OauthAuthorizeCodeLogin>(userData);
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Debug(ex.ToString());
                        return null;
                    }
                }

                set
                {
                    SetData(value);
                }
            }

            private void SetData(OauthAuthorizeCodeLogin user)
            {
                var userDataStr = JsonConvert.SerializeObject(user);
                //create new ticket
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                1, user.userInfo.unionid, DateTime.Now, DateTime.Now.AddMinutes(30),
                false, userDataStr);
                //encrypt ticket
                string authTicket = FormsAuthentication.Encrypt(ticket);
                //save the encrpted ticket to the cookie
                HttpCookie coo = new HttpCookie(FormsAuthentication.FormsCookieName, authTicket);
                System.Web.HttpContext.Current.Response.Cookies.Add(coo);
            }


            protected override void OnActionExecuting(ActionExecutingContext filterContext)
            {

                //if (filterContext.RouteData.Values["action"].ToString().ToLower() != "notwechat")
                //{
                //    if (!WechatHelper.IsWechatBrowser())
                //    {
                //        LogHelper.Debug("Start");
                //        var Url = new UrlHelper(filterContext.RequestContext);
                //        var tt = filterContext.HttpContext.Request.RawUrl;
                //        var url = Url.Action("notwechat", "wechat");
                //        filterContext.Result = new RedirectResult(url);
                //    }
                //}
            }

            public ActionResult Error() 
            {
                return View("~/Views/Shared/Error.cshtml");
            }

            public ActionResult WeChatError()
            {
                return View("~/Views/Shared/WeChatError.cshtml");
            }
        }
}
