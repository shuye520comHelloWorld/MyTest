﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.Services.Infrastructure.Common
{
    public class EventConsultantInfo
    {
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
        /// The start date of event check in
        /// </summary>
        public System.DateTime CheckInStartDate { get; set; }
        /// <summary>
        /// The end date of event check in
        /// </summary>
        public System.DateTime CheckInEndDate { get; set; }
        /// <summary>
        /// The start date of apply ticket stage
        /// </summary>
        public System.DateTime ApplyTicketStartDate { get; set; }
        /// <summary>
        /// The end date of apply ticket stage
        /// </summary>
        public System.DateTime ApplyTicketEndDate { get; set; }
        /// <summary>
        /// The end date of invitation
        /// </summary>
        public System.DateTime InvitationEndDate { get; set; }
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
        /// The unique  identifier of consultant who attend the event
        /// </summary>    
        public Guid MappingKey { get; set; }
        /// <summary>
        /// The type of consultant who attend the event,arivable value  NormalBC,BestowalBC, VIPBC,VolunteerBC
        /// </summary>
        public short UserType { get; set; }
        /// <summary>
        /// Consultant has the normal ticket quantity 
        /// </summary>
        public int NormalTicketQuantity { get; set; }
        /// <summary>
        /// Consultant has the vip ticket quantity 
        /// </summary>
        public int VIPTicketQuantity { get; set; }
        /// <summary>
        /// The unique identifier of beauty consultant in our business system
        /// </summary>
        public long ContactId { get; set; }
        /// <summary>
        /// The county name  of beauty consultant in our business system
        /// </summary>
        public bool IsConfirmed { get; set; }

        /// <summary>
        /// status:0 or null,normal have't apply Ticket
        /// status:1, normal have applied ticket
        /// status:2, volunteer was canceled
        /// </summary>
        public short Status { get; set; }

    }

    public class UnitBCInfo
    {
        public long ContactId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string DirectSellerId { get; set; }

        public string UnitName { get; set; }
    }

    public class TicketInfo
    {
        public Guid TicketKey { get; set; }

        public short TicketType { get; set; }

        public short TicketStatus { get; set; }

        public string CustomerName { get; set; }

        public string CustomerPhone { get; set; }

        public Guid CustomerKey { get; set; }

        public Guid MappingKey { get; set; }

        public short TicketFrom { get; set; }

    }

    public class TicketDetailInfo
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public Guid TicketKey { get; set; }

        public string SMSToken { get; set; }

        public short TicketType { get; set; }

        public short TicketStatus { get; set; }

        public Guid CustomerKey { get; set; }

        public string CustomerName { get; set; }

        public string CustomerPhone { get; set; }

        public DateTime SessionStartDate { get; set; }

        public DateTime SessionEndDate { get; set; }

    }

    public class InvitationInfo
    {
        public Guid TicketKey { get; set; }

        public int TicketType { get; set; }

        public int TicketStatus { get; set; }

        public Guid CustomerKey { get; set; }

        public string CustomerName { get; set; }

        public string CustomerPhone { get; set; }

        public string UnionID { get; set; }

        public string HeadImgUrl { get; set; }
    }

    public class ConsultantInfo
    {
        public string DirectSellerID { get; set; }

        //public string ConsultantLevelID { get; set; }

        public long ContactID { get; set; }
      
        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Level { get; set; }

        public string ResidenceID { get; set; }

        public string Province { get; set; }

        public string City { get; set; }

        public string County { get; set; }

        public string PhoneNumber { get; set; }

       
    }

    public class PhoneNumberValidationInfo
    {
        public string DirectSellerID { get; set; }
        public long ContactID { get; set; }
        public string ConsultantLevelID { get; set; }
        public string PhoneNumber { get; set; }
    }
}
