using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WechatH5Helper
{
    public class UserModel
    {
        public string Userkey { get; set; }
        public string ActivityKey { get; set; }
        public string UnionId { get; set; }

        public int LevelCode { get; set; }
        public string access_token { get; set; }
        public userInfo userInfo { get; set; }
    }
}
