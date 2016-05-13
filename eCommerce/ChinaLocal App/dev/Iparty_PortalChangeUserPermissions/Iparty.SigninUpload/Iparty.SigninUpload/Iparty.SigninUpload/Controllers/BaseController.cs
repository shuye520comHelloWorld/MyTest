using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;


namespace Iparty.SigninUpload.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/

        public BaseController()
        {
          
        }

        public ActionResult Error(string value)
        {
            if (value == "autherr")
            {
                ViewBag.msg = "没有权限打开此页面";
            }
            return View();
        }


    }


    public class UserAuth : System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //string admin = System.Web.HttpContext.Current.User.Identity.Name;
            //string admins = ConfigurationManager.AppSettings["adminUser"];
            //if (!admins.ToLower().Contains(admin.ToLower()))
            //{
            //    var Url = new UrlHelper(filterContext.RequestContext);
            //    var url = Url.Action("Error", "Base", new { value = "autherr" });
            //    filterContext.Result = new RedirectResult(url);
            //}
        }

    }
}
