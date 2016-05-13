
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using System.Web.Security;
using Newtonsoft.Json;

namespace WechatH5Helper
{
    public class SessionManager
    {
        public static UserModel User {
            get {
                if (!HttpContext.Current.User.Identity.IsAuthenticated) return null;
                var UserData = (HttpContext.Current.User.Identity as FormsIdentity).Ticket.UserData;
                return string.IsNullOrWhiteSpace(UserData) ? null : JsonConvert.DeserializeObject<UserModel>(UserData);
                                 
            }
            set {
                FormsAuthentication.SignOut();
                SetCookie(value);
            }
        }
        private static void SetCookie(UserModel user)
        {
            var userDataStr = JsonConvert.SerializeObject(user);
            //create new ticket
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                user.Userkey, DateTime.Now, DateTime.Now.AddDays(1),
                false, userDataStr);
            string encryptTicket = FormsAuthentication.Encrypt(ticket);
            HttpCookie coo = new HttpCookie(FormsAuthentication.FormsCookieName, encryptTicket);         
            HttpContext.Current.Response.Cookies.Add(coo);

        }
    }
}
