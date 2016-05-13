using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PinkBus_QRcode.Controllers
{
    public class HomeController : Controller
    {
       
        public ActionResult CheckIn(Guid id)
        {
            ViewBag.NewId = id.ToString();
            return View();
        }

       
    }
}
