using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.Services.Entity
{
    /// <summary>
    /// Event information
    /// </summary>
    [Alias("PB_Event")]
    public class Event
    {
        /// <summary>
        /// The identifier of event
        /// </summary>
        [PrimaryKey]
        public System.Guid EventKey { get; set; }
        /// <summary>
        /// The location of event happened
        /// </summary>
        public string EventLocation { get; set; }
        /// <summary>
        /// The title of event
        /// </summary>
        public string EventTitle { get; set; }            
        /// <summary>
        /// The start date of event
        /// </summary>
        public System.DateTime EventStartDate { get; set; }
        /// <summary>
        /// The end date of event
        /// </summary>
        public System.DateTime EventEndDate { get; set; }       
        /// <summary>
        /// The start date of apply ticket stage
        /// </summary>
        public System.DateTime ApplyTicketStartDate { get; set; }
        /// <summary>
        /// The end date of apply ticket stage
        /// </summary>
        public System.DateTime ApplyTicketEndDate { get; set; }
        /// <summary>
        /// The start date of check in
        /// </summary>
        public System.DateTime CheckInStartDate { get; set; }
        /// <summary>
        /// The end date of check in
        /// </summary>
        public System.DateTime CheckInEndDate { get; set; }
        /// <summary>
        /// The start date of invitation
        /// </summary>
        public System.DateTime? InvitationStartDate { get; set; }
        /// <summary>
        /// The end date of invitation
        /// </summary>
        public System.DateTime InvitationEndDate { get; set; }
        /// <summary>
        /// The download token while downloaded data to client application
        /// </summary>
        public string DownloadToken { get; set; }
        /// <summary>
        /// The upload token while upload data to client application
        /// </summary>
        public string UploadToken { get; set; }
        /// <summary>
        /// Indicate whether client applicaiton upload data already.
        /// </summary>
        public bool IsUpload { get; set; }
        /// <summary>
        /// The flag indicate whether imported event-consultant BC already
        /// </summary>
        public bool BCImport { get; set; }
        /// <summary>
        /// The flag indicate whether imported event-consultant Volunteer already
        /// </summary>
        public bool VolunteerImport { get; set; }
        /// <summary>
        /// The flag indicate whether imported event-consultant VIP already
        /// </summary>
        public bool VipImport { get; set; }
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
