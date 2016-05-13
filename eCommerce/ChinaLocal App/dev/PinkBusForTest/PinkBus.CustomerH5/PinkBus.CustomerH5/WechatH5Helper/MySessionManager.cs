using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace WechatH5Helper
{
    public class MySessionManager<TModel>
       where TModel : class
    {
        public static TModel User
        {
            get
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated == false) return null;
                var userData = ((System.Web.Security.FormsIdentity)HttpContext.Current.User.Identity).Ticket.UserData;
                return string.IsNullOrWhiteSpace(userData) ? null : JsonConvert.DeserializeObject<TModel>(userData);
            }
            set
            {
                System.Web.Security.FormsAuthentication.SignOut();
                SetCookie(value);
            }
        }
        /// <summary>
        /// set cookie for user
        /// </summary>
        /// <param name="user"></param>
        private static void SetCookie(TModel user)
        {

            var userDataStr = JsonConvert.SerializeObject(user);
            //create new ticket
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
            1, "sss", DateTime.Now, DateTime.Now.AddMinutes(5),
            true, userDataStr);
            var ispar = ticket.IsPersistent;
            //encrypt ticket
            string authTicket = FormsAuthentication.Encrypt(ticket);
            //save the encrpted ticket to the cookie
            HttpCookie coo = new HttpCookie(FormsAuthentication.FormsCookieName, authTicket);
            coo.Expires = DateTime.Now.AddMinutes(15);
            HttpContext.Current.Response.Cookies.Add(coo);
        }
    }
}
