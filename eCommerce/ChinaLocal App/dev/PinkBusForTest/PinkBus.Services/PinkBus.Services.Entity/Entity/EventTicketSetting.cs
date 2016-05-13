using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.Services.Entity
{
    /// <summary>
    /// Ticket setting in an event
    /// </summary>
    [Alias("PB_EventTicketSetting")]
    public partial class EventTicketSetting
    {
        /// <summary>
        /// The unique  identifier of event apply ticket setting 
        /// </summary>
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        /// <summary>
        /// The unique identifier of event
        /// </summary>
        public System.Guid EventKey { get; set; }
        /// <summary>
        /// The normal ticket quantity which normal bc can be applied for every session
        /// </summary>
        public int TicketQuantityPerSession { get; set; }
        /// <summary>
        /// The total ticket amount for normal bc applying
        /// </summary>
        public int ApplyTicketTotal { get; set; }
         /// <summary>
        /// The normal ticket quantity which Volunteer can be applied for every session
        /// </summary>
        public int VolunteerNormalTicketCountPerPerson { get; set; }
        /// <summary>
        /// The vip ticket quantity which Volunteer can be applied for every session
        /// </summary>
        public int VolunteerVIPTicketCountPerPerson { get; set; }
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
        public System.DateTime? UpdatedDate { get; set; }
        /// <summary>
        /// The person who updated the event 
        /// </summary>
        public string UpdatedBy { get; set; }

    }
}
