using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IParty.RegisterCustomer.Mobile.Models
{
    public class RegisterModel
    {
        public string referenceKey { get; set; }
        public string unionId { get; set; }
        public long partyKey { get; set; }
        public string partyname { get; set; }
        public string location { get; set; }
        public string meetingTime { get; set; }
        public string inviter { get; set; }
        public string name { get; set; }
        public string mobile { get; set; }
        public string work { get; set; }
        public string age { get; set; }
        public string marriage { get; set; }
        public bool isWX { get; set; }

    }


    public class CustomerModel
    {
        public string  CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNo { get; set; }
        public string IdentityId { get; set; }
        public int WorkId { get; set; }
        public int AgePhaseId { get; set; }
        public int SalaryId { get; set; }
        public int MarriageId { get; set; }
        public int TestResultId { get; set; }
        public int UsedMKProduct { get; set; }
    }


    public class CardPushModel
    {
        public string cardId { get; set; }
        public string cardExt { get; set; }
    }
    public class CardPushExtra
    {
        public string code { get; set; }
        //public string openid { get; set; }
        public string timestamp { get; set; }
        public string signature { get; set; }

    }



    /// <summary>
    /// oauthTokenByClientId
    /// </summary>
    public class GetTokenByClientId
    {
        public string access_token;
        public string expires_in;
        public string token_type;
        public string scope;
        public string ErrorCode;
    }

    //signature
    public class WechatSignature
    {
        public string errcode { get; set; }
        public string errmsg { get; set; }
        public string signature { get; set; }
        public string timestamp { get; set; }
        public string noncestr { get; set; }
    }


    /// <summary>
    /// oauthCodeLogin
    /// </summary>
    public class OauthAuthorizeCodeLogin
    {
        public string access_token { get; set; }
        //public string refresh_token { get; set; }
        public string binding_code { get; set; }
        public userInfo userInfo { get; set; }
        public string errcode { get; set; }
        public string errmsg { get; set; }
    }

    public class userInfo
    {
        public string _nickname { get; set; }
        public string nickname
        {
            get { return _nickname; }
            set
            { _nickname = HttpUtility.UrlEncode(value, System.Text.Encoding.UTF8); }
        }

        public string sex { get; set; }
        public string province { get; set; }
        public string City { get; set; }
        public string country { get; set; }
        public string[] privilege { get; set; }
        public string headimgurl { get; set; }
        public string unionid { get; set; }
    }



    /// <summary>
    /// WechatLoginToken
    /// </summary>
    public class WechatLoginToken
    {
        public string access_token { get; set; }
        public string expires_in { get; set; }
        public string refresh_token { get; set; }
        public string openid { get; set; }
        public string scope { get; set; }
        public string unionid { get; set; }
        public string errcode { get; set; }
        public string errmsg { get; set; }

    }
}