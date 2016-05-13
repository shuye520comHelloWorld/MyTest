using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.Services.Entity
{
    /// <summary>
    /// Record consultant apply ticker ticker in an event
    /// </summary>
    [Alias("PB_ApplyTicketTracker")]
    public class ApplyTicketTracker
    {
        /// <summary>
        /// The unique  identifier of ApplyTicketTracker
        /// </summary>
        [PrimaryKey]
        [Alias("TrackerKey")]
        public Guid ApplyTicketTrackerId { get; set; }
        /// <summary>
        /// The unique  identifier of consultant,who send the apply ticket request
        /// </summary>       
        public Guid MappingKey { get; set; }
        /// <summary>
        /// The The unique  identifier of session key, bc applied session's ticket
        /// </summary>
        public Guid SessionKey { get; set; }
        /// <summary> 
        /// Apply ticket result status, variable value are Unknown,Success,Fail
        /// </summary>   
        [Description("refer the enum type the processing stage, variable value are 0:pending;1:processing;2:complete")]
        public int Status { get; set; }
        /// <summary> 
        /// 
        /// 
        /// 
        /// Apply ticket result status, variable value are Unknown,Success,Fail
        /// </summary>
      
        [Description("refer the enum type the ApplyTicketResult, variable value are  Unknown,Success,Fail")]
        [Alias("ApplyResult")]
        public int ApplyTicketResult { get; set; }
        /// <summary>
        /// The date when created the event
        /// </summary>
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// The person who created the event
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// The date when updated the event 
        /// </summary>
        public DateTime? UpdatedDate { get; set; }
        /// <summary>
        /// The person who updated the event 
        /// </summary>
        public string UpdatedBy { get; set; }
    }
}
