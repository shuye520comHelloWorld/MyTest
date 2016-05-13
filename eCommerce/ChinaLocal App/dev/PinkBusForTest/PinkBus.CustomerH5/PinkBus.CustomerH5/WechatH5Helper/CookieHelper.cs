using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CustomerAcquisitionGame.Instrument.Common
{
    public static class CookieHelper
    {
        public static string Get(string cookieName)
        {
          var cookie= HttpContext.Current.Request.Cookies[cookieName];
          if (cookie!=null)
          {
              return cookie.Value;
          }
          return null;
        }
       /// <summary>
        /// add a Http Cookie to the intrinsic cookie collection
       /// </summary>
       /// <param name="cookieName">Name</param>
       /// <param name="cookieValue">Value</param>
       /// <param name="expires">Expires</param>
       public static void Add(string cookieName, string cookieValue, TimeSpan expires)
       {
           
           HttpCookie cookie = new HttpCookie(cookieName, cookieValue);
           cookie.Expires = DateTime.UtcNow.Add(expires);
           HttpContext.Current.Response.AppendCookie(cookie);              
       }
       /// <summary>
       /// update a individual cookie
       /// </summary>
       /// <param name="cookieName">Name</param>
       /// <param name="cookieValue">Value</param>
       public static void Update(string cookieName, string cookieValue) 
       {
           var cookie = HttpContext.Current.Request.Cookies[cookieName];
           if (cookie!=null)
           {
               cookie.Value = cookieValue;
           }
           HttpContext.Current.Response.AppendCookie(cookie);
       }

       /// <summary>
       /// remove a individual cookie
       /// </summary>
       /// <param name="cookieName">Name</param>
       public static void Remove(string cookieName)
       {
           var cookie=HttpContext.Current.Request.Cookies["cookieName"];
           if (cookie!=null)
           {
               cookie.Expires = DateTime.Now.AddMinutes(-1);
               HttpContext.Current.Response.AppendCookie(cookie);       
           }
       }
    }
}
