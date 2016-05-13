using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IParty.RegisterCustomer.Mobile.Common
{
    public static class TokenCodes
    {
        public static string TokenDecode(string Token)
        {
            if (string.IsNullOrEmpty(Token)) return "";
            return Token.Replace("[PLUS]", "+").Replace("[LINE]", "/").Replace("[EQUAL]","=");
        }

        public static string TokenEncode(string Token)
        {
            if (string.IsNullOrEmpty(Token)) return "";

            return Token.Replace("+", "[PLUS]").Replace("/", "[LINE]").Replace("=", "[EQUAL]");
           
        }
    }
}