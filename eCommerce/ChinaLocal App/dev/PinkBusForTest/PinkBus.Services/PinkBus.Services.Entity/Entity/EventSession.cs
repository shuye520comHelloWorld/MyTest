using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.Services.Entity
{
    /// <summary>
    ///Event session infomation, an event including serveral session
    /// </summary>
    [Alias("PB_EventSession")]
    public class EventSession
    {
        /// <summary>
        /// The identifier of Session
        /// </summary>
        [PrimaryKey]
        public System.Guid SessionKey { get; set; }
        /// <summary>
        /// The identifier of event, an event inlcuding several session 
        /// </summary>
        public System.Guid Eventkey { get; set; }        
        /// <summary>
        /// Display Order of session
        /// </summary>
        public int DisplayOrder { get; set; }
        /// <summary>
        /// Start date of session
        /// </summary>
        public System.DateTime SessionStartDate { get; set; }
        /// <summary>
        /// End date of session
        /// </summary>
        public System.DateTime SessionEndDate { get; set; }
        /// <summary>
        /// Indicate the session whether can be applied
        /// </summary>
        public bool CanApply { get; set; }

        /// <summary>
        /// Indicate ticket is out of stock
        /// </summary>
        public bool TicketOut { get; set; }
        /// <summary>
        /// Total amount of vip ticket in current session
        /// </summary>
        public int VIPTicketQuantity { get; set; }
        /// <summary>
        /// Total amount of vip ticket in current session
        /// </summary>
        public int NormalTicketQuantity { get; set; }
        /// <summary>
        /// The date when created the event
        /// </summary>
        public System.DateTime CreatedDate { get; set; }
        /// <summary>
        /// The person who created the event
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// The date when updated the event 
        /// </summary>
        public System.DateTime UpdatedDate { get; set; }
        /// <summary>
        /// The person who updated the event 
        /// </summary>
        public string UpdatedBy { get; set; }

    }
}
