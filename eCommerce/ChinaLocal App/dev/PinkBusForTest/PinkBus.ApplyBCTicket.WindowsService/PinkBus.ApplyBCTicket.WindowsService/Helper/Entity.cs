using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.ApplyBCTicket.WindowsService
{
    public class PB_ApplyTicketTracker
    {
        public Guid EventKey { get; set; }
        public Guid TrackerKey { get; set; }
        public Guid MappingKey{get;set;}
        public Guid SessionKey { get; set; }
        public short Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
