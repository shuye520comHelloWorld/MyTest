using IParty.RegisterCustomer.Mobile.Common;
using IParty.RegisterCustomer.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IParty.RegisterCustomer.Mobile.Controllers
{
    public class CustomerController : Controller
    {
        //
        // GET: /Customer/

        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult Register(string Token)
        {
            if (string.IsNullOrEmpty(Token))
                return RedirectToAction("error", "home", new { msg = "没有找到的页面" });

            if (WechatHelper.WachatJSDKCanUse())
                return RedirectToAction("Index", "Home", new { Token = Token });


            return View();
        }




    }
}
