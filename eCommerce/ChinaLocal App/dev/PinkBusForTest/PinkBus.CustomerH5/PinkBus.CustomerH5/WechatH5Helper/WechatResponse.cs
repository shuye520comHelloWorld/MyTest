using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WechatH5Helper
{
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
        public bool isLoginBaseInfo { get; set; }
        public bool isLoginUserInfo { get; set; }
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
        public string openid { get; set; }
    }

    public class GetAccessTokenModel
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string ErrorCode { get; set; }
        public string expires_in { get; set; }
        public string refresh_token { get; set; }
        public string scope { get; set; }
    }

    public class SaveAccountModel
    {
        public string ErrorCode { get; set; }
        public string result { get; set; }
        public string account_id { get; set; }
        public string account_type { get; set; }
        public string contactid { get; set; }
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
}
