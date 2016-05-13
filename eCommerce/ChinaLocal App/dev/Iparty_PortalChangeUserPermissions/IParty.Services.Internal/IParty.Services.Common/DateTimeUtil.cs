using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IParty.Services.Common
{
    public static class DateTimeUtil
    {
        public static DateTime GetDayMaxTime(this DateTime dt)
        {
            DateTime dayMaxtime = DateTime.Now.AddHours(1 - dt.Hour).AddDays(1).Date;
            return dayMaxtime;
        }
    }
}
