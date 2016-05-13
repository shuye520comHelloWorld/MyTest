using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.Services.Common
{
    public static class JavaCsDateTimeHelper
    {
        /// <summary>
        /// convert from java to c# dateTime
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime JavaConverTOCsDateTime(long date)
        {
            return new DateTime(new DateTime(1970, 1, 1, 0, 0, 0).Ticks + date * 10000).AddHours(8);
        }

        public static long CsConverToJavaDateTime(DateTime dt)
        {
            return (dt.Ticks - new DateTime(1970, 1, 1, 0, 0, 0).Ticks) / 10000 - 8 * 60 * 60 * 1000;
        }
    }
}
