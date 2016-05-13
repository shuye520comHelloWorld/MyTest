using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.SendActivitySMS.WindowsService
{
    public class PB_Activity
    {
        public Guid EventKey { get; set; }
        public string EventTitle { get; set; }
        public string EventLocation { get; set; }
        public string CheckinStartDate { get; set; }
        
    }
 
}
