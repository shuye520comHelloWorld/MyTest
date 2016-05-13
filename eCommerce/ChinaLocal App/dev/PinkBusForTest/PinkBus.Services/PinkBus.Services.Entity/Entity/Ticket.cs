using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.Services.Entity
{
    /// <summary>
    ///Ticket, the certificate of consultant attend an event 
    /// </summary>
    [Alias("PB_Ticket")]
    public class Ticket
    {
        /// <summary>
        /// The unique  identifier of event apply ticket  
        /// </summary>
        [PrimaryKey]
        public Guid TicketKey { get; set; }
        /// <summary>
        /// The unique  identifier of consultant
        /// </summary>
        //[Alias("MappingKey")]
        public Guid MappingKey { get; set; }
        /// <summary>
        /// The unique  identifier of event
        /// </summary>
        public Guid EventKey { get; set; }
        /// <summary>
        /// The unique identifier of Session
        /// </summary>
        public Guid SessionKey { get; set; }
        /// <summary>
        /// The unique  identifier of customer
        /// </summary>
        public Guid? CustomerKey { get; set; }
        /// <summary>
        /// Ticket type, variable value are Vip/Normal
        /// </summary>
        [Description("refer the enum type the TicketType, variable value are VIP,Normal")]
        public int TicketType { get; set; }
        /// <summary>
        /// Record the ticket pathway , variable value are Import,Apply,Bestowal,Rebuild
        /// </summary> 
        [Description("refer the enum type the TicketType, variable value are Import,Apply,Bestowal,Rebuild")]
        public int TicketFrom { get; set; }
        /// <summary>
        /// Ticket stauts,variable value are Created,Inviting,Invited, Bestowed,Canceled,Checkin, UnCheckin,Expired 
        /// </summary>
        [Description("refer the enum type the TicketType, variable are Created,Inviting,Invited,Bestowed,Canceled, Checkin,UnCheckin,Expired")]
        public int TicketStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>        
        public string SMSToken { get; set; }
        /// <summary>
        /// The unique identifier of Session
        /// </summary>
        public DateTime SessionStartDate { get; set; }
        /// <summary>
        /// The unique identifier of Session
        /// </summary>
        public DateTime SessionEndDate { get; set; }

        public DateTime? CheckinDate { get; set; }
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

    //volunteer checkin times
    [Alias("PB_VolunteerCheckin")]
    public class VolunteerCheckin
    {
        /// <summary>
        /// The unique  identifier of VolunteerCheckin
        /// </summary>
        [PrimaryKey]
        public Guid Key { get; set; }
        /// <summary>
        /// The unique  identifier of event
        /// </summary>
        public Guid EventKey { get; set; }
        /// <summary>
        /// The unique  identifier EventKey of consultant
        /// </summary>
        public Guid MappingKey { get; set; }
        /// <summary>
        /// The date when Volunteer check in
        /// </summary>
        public DateTime CheckinDate { get; set; }

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

    ////apply bc setting
    //[Alias("PinkBus_ApplyTicket")]
    //public class ApplyTicket
    //{
    //    public int ID { get; set; }
    //    public Guid EventKey { get; set; }
    //    [Description("a BC can get tickets in one session")]
    //    public int ApplyTicketEverySession { get; set; }
    //    [Description("the ticket amount in all session")]
    //    public int CanApplyTicketCount { get; set; }

    //    public DateTime CreatedTime { get; set; }
    //    public string CreatedBy { get; set; }
    //    public DateTime UpdatedTime { get; set; }
    //    public string UpdatedBy { get; set; }
    //}

    ////apply sessions 
    //public class ApplyTicketSession
    //{
    //    public int ID { get; set; }
    //    public int ApplyTicketId { get; set; }
    //    public Guid SessionKey { get; set; }

    //    public DateTime CreatedTime { get; set; }
    //    public string CreatedBy { get; set; }
    //    public DateTime UpdatedTime { get; set; }
    //    public string UpdatedBy { get; set; }
    //}




    //[Alias("PinkBus_Ticket")]
    //public class Ticket
    //{

    //    public Guid TicketKey { get; set; }
    //    public Guid EventKey { get; set; }
    //    public TicketType TicketType { get; set; }
    //    public DateTime CreatedTime { get; set; }
    //    public string CreatedBy { get; set; }
    //    public DateTime UpdatedTime { get; set; }
    //    public string UpdatedBy { get; set; }
    //}

    ////volunteer checkin times
    //[Alias("PinkBus_VolunteerCheckin")]
    //public class VolunteerCheckin
    //{
    //    public Guid EventKey { get; set; }
    //    public long ContactId { get; set; }
    //    public DateTime CheckinTime { get; set; }
    //    public DateTime CreatedTime { get; set; }
    //}


    //[Alias("PinkBus_BCTicketMapping")]
    //public class BCTicketMapping
    //{
    //    public int ID { get; set; }
    //    public long ContactId { get; set; }
    //    public Guid TicketKey { get; set; }
    //    [Description("this ticket can be used by the BC")]
    //    public bool Status { get; set; }
    //    public string CreatedBy { get; set; }
    //    public DateTime CreatedTime { get; set; }
    //    public string UpdatedBy { get; set; }
    //    public DateTime UpdatedTime { get; set; }
    //}


    //[Alias("PinkBus_CustomerTicketMapping")]
    //public class CustomerTicketMapping
    //{
    //    [Description("the key is QRCode")]
    //    public Guid TicketQRKey { get; set; }
    //    public Guid TicketKey { get; set; }
    //    public string SMSToken { get; set; }
    //    public Guid CustomerKey { get; set; }
    //    public TicketType TicketType { get; set; }
    //    public TicketStatus TicketStatus { get; set; }
    //    public DateTime CheckinTime { get; set; }

    //    public string CreatedBy { get; set; }
    //    public DateTime CreatedTime { get; set; }
    //    public string UpdatedBy { get; set; }
    //    public DateTime UpdatedTime { get; set; }

    //}
}
