using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PinkBus.CustomerH5.Models
{
    public class VoteRemindModel
    {
        public List<UserProfiles> UserProfiles { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }

    }
    public class UserProfiles
    {
        public bool IsSubscribe { get; set; }
        public string OpenId { get; set; }
        public string NickName { get; set; }
        public string Sex { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string SubscribeTime { get; set; }
        public string ContactID { get; set; }
        public string UnionID { get; set; }
        public bool IsBinding { get; set; }

    }
}