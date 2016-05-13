using PinkBus.EventStat.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Web;
using System.Xml;

namespace PinkBus.EventStat.Helper
{
    public class Common
    {

        public static T ConvertTo<T>(object val, T defaultVal)
        {
            if (Convert.IsDBNull(val) || val == null)
            {
                return defaultVal;
            }
            else
            {
                try
                {
                    return (T)Convert.ChangeType(val, typeof(T));
                }
                catch
                {
                    return defaultVal;
                }
            }
        }






    }
    
}