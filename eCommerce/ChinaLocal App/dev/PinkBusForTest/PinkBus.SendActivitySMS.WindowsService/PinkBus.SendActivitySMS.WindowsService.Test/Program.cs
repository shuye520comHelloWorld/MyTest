using NLog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.SendActivitySMS.WindowsService.Test
{
    class Program
    {
        static void Main(string[] args)
        {


            Console.WriteLine(Convert.ToDateTime("2016-01-13 20:53:53.657").ToString("yyyy.MM.dd HH:mm", CultureInfo.InvariantCulture));
            Logger _logger = LogManager.GetCurrentClassLogger();
            var work = new ActivitySMS();
            work.Send();
        }
    }
}
