using IParty.RegisterCustomer.Mobile.Common;
using IParty.RegisterCustomer.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IParty.RegisterCustomer.Mobile.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index(string Token, string ReferenceKey="")
        {
            LogHelper.Debug(Request.UserAgent);
            if (WechatHelper.WachatJSDKCanUse())
            {
                LogHelper.Debug("ReferenceKey--"+ReferenceKey);
                string returnurl = GlobalSettings.OpenWechatUrl.Replace("Tokenval", TokenCodes.TokenEncode(Token)).Replace("ReferenceKeyval", ReferenceKey);
                return Redirect(returnurl);
            }
            else
            {
                return RedirectToAction("Register", "Customer", new {Token=TokenCodes.TokenEncode(Token), ReferenceKey = ReferenceKey });
            }

        }

        public JsonResult GetPartyInfo(string Token)
        {
            try
            {
                LogHelper.Debug("Token");
                LogHelper.Debug(Token);
                Dictionary<string, string> parms = new Dictionary<string, string>();
                parms.Add("InvitationToken", Token);

                var partyInfo = ServiceHttpRequest.Post<PartyInfo>(GlobalSettings.PartyAPIUrl + "parties/invitations/party", parms);
                if (partyInfo.ResponseStatus != null)
                {
                    return Json(new { result = false, msg = partyInfo.ResponseStatus.Message }, JsonRequestBehavior.AllowGet);
                }
                if (string.IsNullOrEmpty(partyInfo.EventTitle))
                {
                    return Json(new { result = false, msg = "要参加的活动不存在！" }, JsonRequestBehavior.AllowGet);

                }
                partyInfo.meetingtime = partyInfo.PartyStartDate.ToString("yyyy年MM月dd日 HH:mm") + " - " + partyInfo.PartyEndDate.ToString("HH:mm");
                LogHelper.Debug("success");
                return Json(new { result = true, party = partyInfo }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogHelper.Debug(ex.ToString());
                return Json(new { result = false,msg=ex.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public JsonResult SaveRegister()
        {
          
            string name = Request.Form["name"];
            string phoneNumber = Request.Form["mobile"];
            
            string referenceKey = Request.Form["referenceKey"];
            string Token = Request.Form["Token"];
            string UnionId = Request.Form["unionId"];
            LogHelper.Debug("SaveRegisterToken");
            LogHelper.Debug(Token);

            Dictionary<string, string> parms = new Dictionary<string, string>();
            parms.Add("InvitationToken", Token);
            parms.Add("Name", name);
            parms.Add("PhoneNumber", phoneNumber);
            parms.Add("ReferenceBy", referenceKey);
            parms.Add("UnionId", UnionId);
            parms.Add("IsOnSite", "false");
            var partyInfo = ServiceHttpRequest.Post<InvitationResponse>(GlobalSettings.PartyAPIUrl + "parties/invitations", parms);
            if (partyInfo.ResponseStatus!=null)
                return Json(new { result = false, msg = partyInfo.ResponseStatus.Message }, JsonRequestBehavior.AllowGet);
           
            LogHelper.Debug(name);
            LogHelper.Debug(phoneNumber);
            LogHelper.Debug(referenceKey);
            LogHelper.Debug(Token);
            LogHelper.Debug(UnionId);
            //InvitationResponse res = partyInfo;
            partyInfo.CustomerKey = partyInfo.CustomerKey.Replace("-", "");
            partyInfo.InvitationKey = partyInfo.InvitationKey.Replace("-", "");


            return Json(new { result = true, Invitation = partyInfo }, JsonRequestBehavior.AllowGet);
        }



        [AllowAnonymous]
        public ActionResult Error(string msg)
        {
            return View();
        }


    }
}
