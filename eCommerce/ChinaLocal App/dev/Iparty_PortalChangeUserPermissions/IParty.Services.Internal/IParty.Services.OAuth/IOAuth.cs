using ServiceStack.Web;
using System;
using System.Collections.Generic;

namespace IParty.Services.OAuth
{
    public interface IOAuth
    {
        OAuthToken AcquireToken(IRequest req, IResponse res);
    }
    public class OAuthToken
    {
        public string Token { get; set; }
        public string ResponseCode { get; set; }
        public List<AccountInfo> User { get; set; }
        public string Scope { get; set; }
        public string ExpirationDateUTC { get; set; }
    }

    public class AccountInfo 
    {
        public string AccountType { get; set; }

        public string AccountID { get; set; }
    }
}
