using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PinkBus.EventStat.Models
{
    public class PB_Event
    {
        public System.Guid EventKey { get; set; }
        public string EventLocation { get; set; }
        public string EventTitle { get; set; }
        public DateTime EventStartDate { get; set; }
        public DateTime EventEndDate { get; set; }
        public DateTime ApplyTicketStartDate { get; set; }
        public DateTime ApplyTicketEndDate { get; set; }
        public DateTime? InvitationStartDate { get; set; }
        public DateTime InvitationEndDate { get; set; }
        public DateTime CheckinStartDate { get; set; }
        public DateTime CheckinEndDate { get; set; }
        public string DownloadToken { get; set; }       
        public string UploadToken { get; set; }
        public bool? IsUpload { get; set; }
        public bool BCImport { get; set; }
        public bool VolunteerImport { get; set; }
        public bool VipImport { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        public List<PB_EventSession> EventSessions { get; set; }
    }

    public class PB_EventSession
    {
        public Guid SessionKey { get; set; }
        public Guid EventKey { get; set; }
        public short DisplayOrder { get; set; }
        public DateTime SessionStartDate { get; set; }
        public DateTime  SessionEndDate { get; set; }
        public bool CanApply { get; set; }
        public int VIPTicketQuantity { get; set; }
        public int NormalTicketQuantity { get; set; }
        public DateTime  CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        public int? NormalTicketUsedCount { get; set; }
        public int? VipTicketUsedCount { get; set; }
        public DateTime?  UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

    }


    public partial class PB_EventTicketSetting
    {
        public int Id { get; set; }
        public System.Guid EventKey { get; set; }
        public int TicketQuantityPerSession { get; set; }
        public int ApplyTicketTotal { get; set; }
        public int? VolunteerNormalTicketCountPerPerson { get; set; }
        public int? VolunteerVIPTicketCountPerPerson { get;set;}
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

    }


    public class PB_Customer
    {
        public Guid CustomerKey { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public CustomerType CustomerType { get; set; }
        public AgeRange AgeRange { get; set; }
        public Career Career { get; set; }
        public InterestingTopic InterestingTopic { get; set; }
        public bool? BeautyClass { get; set; }
        public bool? UsedProduct { get; set; }
        public string UsedSet { get; set; }
        public string InterestInCompany { get; set; }
        public string AcceptLevel { get; set; }

        public long? CustomerContactId { get; set; }
        public string UnionID { get; set; }
        public string HeadImgUrl { get; set; }
        public int Source { get; set; }
        public bool? IsImportMyCustomer { get; set; }
        public string SMSStatus { get; set; }

        public TicketType TicketType { get; set; } 
    }


    public  class PB_OfflineCustomer
    {
        public Guid CustomerKey { get; set; }
        public string CustomerName { get; set; }
        public string ContactType { get; set; }
        public string ContactInfo { get; set; }
        public string DirectSellerId { get; set; }
        public Guid EventKey { get; set; }
        public AgeRangeOffline? AgeRange { get; set; }
        public bool? IsHearMaryKay { get; set; }
        public string InterestingTopic { get; set; }
        public OfflineCustomerType? CustomerType { get; set; }
        public string Career { get; set; }
        public int? IsJoinEvent { get; set; }
        public int? CustomerResponse { get; set; }
        public bool? UsedProduct { get; set; }
        public int? BestContactDate { get; set; }
        public int? AdviceContactDate { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public bool? IsImport { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        
    }

    public class PB_OfflineCustomerSeller
    {
        public string DirectSellerId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
    }


    public class PB_Ticket
    {
        public Guid TicketKey { get; set; }
        public Guid EventKey { get; set; }
        public TicketType TicketType { get; set; }
        public TicketStatus TicketStatus { get; set; }
        //public string DirectSellerId { get; set; }
        //public string DirectSellerId { get; set; }
        //public string DirectSellerId { get; set; }
        //public string DirectSellerId { get; set; }
        //public string DirectSellerId { get; set; }
        //public string DirectSellerId { get; set; }
        //public string DirectSellerId { get; set; }
        //public string DirectSellerId { get; set; }
    }

    public class pb_Inviter
    {
        public string DirectSellerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Level { get; set; }
        public string PhoneNumber { get; set; }
        public int NormalTicketQuantity { get; set; }
        public int VIPTicketQuantity { get; set; }
    }

    public class PB_Consultant
    {
        public Guid MappingKey { get; set; }
        public Guid EventKey { get; set; }
        public int UserType { get; set; }
        public string DirectSellerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int Status { get; set; }
        public int? TicketStatus { get; set; }
        public DateTime? CheckinDate { get; set; }
    }



    public  class EventTracking
    {
        public string InvitByWechat { get; set; }
        public string InvitBySMS { get; set; }
        public string SendTicketByWechat { get; set; }
        public string SendTicketBySMS { get; set; }
        public string GetTicketByBCInput { get; set; }
        public string GetTicketByWechat { get; set; }
        public string GetTicketBySMS { get; set; }
        public string CheckinByQRcode { get; set; }
        public string CheckinByToken { get; set; }
        public string CheckinByLiveInput { get; set; }
    }

    public class  PB_EventConsultant
    {
        public string MappingKey { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string PhoneNumber { get; set; }
        public string ResidenceID { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string CountyName { get; set; }
        public string Level { get; set; }


    }


}