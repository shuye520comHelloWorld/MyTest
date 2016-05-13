using IParty.RegisterCustomer.Mobile.Common;
using IParty.RegisterCustomer.Mobile.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IParty.RegisterCustomer.Mobile.Controllers
{
    public class WechatController : Controller
    {
        
        public ActionResult Callback()
        {
            var Token = Request.QueryString["Token"];

            if (string.IsNullOrEmpty(Token))
                return RedirectToAction("error", "home", new { msg = "没有找到的页面" });

            if (!WechatHelper.WachatJSDKCanUse())
                return RedirectToAction("Index", "Home", new { Token = Token });


            var code = Request.QueryString["code"];
            var state = Request.QueryString["state"];
            var ReferenceKey = Request.QueryString["ReferenceKey"];

            return View();


        }

        [HttpPost]
        public JsonResult reCallback()
        {
            string code = Request.Form["code"];
            string state = Request.Form["state"];
            var Token = Request.Form["Token"];
            var ReferenceKey = Request.Form["ReferenceKey"];
            LogHelper.Debug("Request code :" + code);
            LogHelper.Debug("Request ReferenceKey :" + ReferenceKey);
            LogHelper.Debug("Request Token :" + Token);
            var result = WechatHelper.getUserUnionIdByOauthApi(code);

            if (result == null || result.errcode != null)
            {
                if (result != null)
                    LogHelper.DebugErr("login error " + result.errcode + ":" + result.errmsg);
                else
                    LogHelper.DebugErr("login WechatUserInfo is null ");

                return Json(new { url = "/Customer/Register?Token=" + HttpUtility.UrlEncode(Token) + "&ReferenceKey=" + ReferenceKey }, JsonRequestBehavior.AllowGet);
            }

            LogHelper.Debug(JsonConvert.SerializeObject(result));

            return Json(new { url = "/wechat/Register?Token=" + HttpUtility.UrlEncode(Token) + "&ReferenceKey=" + ReferenceKey + "&openId=" + result.openid + "&unionId=" + result.unionid }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult wechatViewErr(string code, string status)
        {
            LogHelper.Debug("wechat code:" + code + "; status:" + status + "; UserAgent :  " + Request.UserAgent);
            return Json(JsonRequestBehavior.AllowGet);
        }

        public ActionResult Register(string Token)
        {
            //if (string.IsNullOrEmpty(Token))
            //    return RedirectToAction("error", "home", new { msg = "没有找到的页面" });

            //if (!WechatHelper.WachatJSDKCanUse(Request.UserAgent))
            //    return RedirectToAction("Index", "Home", new { Token = Token });

            return View();
        }


        public JsonResult GetWechatConfig(string url)
        {
            var ticket = WechatHelper.getWechatJsSignature(HttpUtility.UrlEncode(url));
            return Json(new { noncestr = ticket.noncestr, timestamp = ticket.timestamp, signature = ticket.signature }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult addCardParms(string cardCode, string Token, string cardId)
        {
            var location = string.Empty;
            try
            {
               
                LogHelper.Debug(cardId);
                var cardPush = new CardPushModel();

                cardPush.cardId = cardId;

                var sign = WechatHelper.getWechatCardSignature(cardId, cardCode);

                var objExtra = new CardPushExtra
                {
                    code = cardCode,
                    // openid = string.Empty,
                    signature = sign.signature,
                    timestamp = sign.timestamp

                };
                cardPush.cardExt = JsonConvert.SerializeObject(objExtra);
                var listCards = new List<CardPushModel>();
                listCards.Add(cardPush);
                return Json(new { result = true, lst = listCards, cardCode = cardCode, cardId = cardId }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogHelper.Debug(ex.ToString());
                return Json(new { result = false, msg = ex.Message + " || error location:" + location }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult getCardSuccess(Guid cardCode,string CardId,Guid partyKey)
        {
            string cardAdd = GlobalSettings.PartyAPIUrl + "wechart/cards";
            Dictionary<string, string> parms = new Dictionary<string, string>();
            parms.Add("CardCode", cardCode.ToString());
            parms.Add("CardID", CardId);
            parms.Add("PartyKey", partyKey.ToString());
            var res = ServiceHttpRequest.Post<WechatCard>(cardAdd, parms);
            if (res.ResponseStatus != null)
            {
                return Json(new { result = false, msg = res.ResponseStatus.Message }, JsonRequestBehavior.AllowGet);
            }
           
            return Json(new { result = true, res = res}, JsonRequestBehavior.AllowGet);
        }
    }



    class WXOauthResponse
    {
        public string OAuthData { get; set; }
    }

    class WXATResoponse
    {
        public string access_token { get; set; }
        public string openid { get; set; }
    }

    enum WXOAuthType
    {
        AccessToken,
        GlobalTicket,
        CardApiTicket,
        SiteAccessToken,
        MobileAccessToken
    }


}
