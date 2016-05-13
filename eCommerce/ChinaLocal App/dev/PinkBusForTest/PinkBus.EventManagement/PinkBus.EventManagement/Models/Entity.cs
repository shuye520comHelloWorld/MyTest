using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PinkBus.EventManagement.Models
{
    public class EventPost
    {
        public Guid? EventKey { get; set; }
        public string EventTitle { get; set; }
        public string EventLocation { get; set; }
        public DateTime EventStartDate { get; set; }
        public DateTime  EventEndDate { get; set; }
        public DateTime? InvitationStartDate { get; set; }
        public DateTime  InvitationEndDate { get; set; }
        public DateTime ApplyTicketStartDate { get; set; }
        public DateTime ApplyTicketEndDate { get; set; }
        public DateTime CheckinStartDate { get; set; }
        public DateTime CheckinEndDate { get; set; }
        public string DownloadToken { get; set; }
        public string UploadToken { get; set; }


        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }    
        public string UpdatedBy { get; set; }
        public List<EventSession> EventSessions { get; set; }

    }

    public class EventSession
    {
        public int TimesNo { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int VIPTotal { get; set; }
        public int NormalTotal { get; set; }
    }

    public class SearchModel
    {
        public int Page { get; set; }
        public int pageSize { get; set; }
        public string TableName { get; set; }
        public string Field { get; set; }
        public string OrderBy { get; set; }
        public string Filter { get; set; }
        public int MaxPage { get; set; }
        public int TotalRow { get; set; }
        public string Descript { get; set; }
    }

    public class EventConsultant
    {
        public Guid? MappingKey { get; set; }
        public string DirectSellerID { get; set; }
        public string ContactID { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Level { get; set; }
        public string ResidenceID { get; set; }
        public string Province { get; set; }
        public string City { get;set;}
        public string CountyName { get; set; }
        public int NormalTicketQuantity { get; set; }
        public int VIPTicketQuantity { get; set; }
        public int NormalTicketSettingQuantity { get; set; }
        public int VIPTicketSettingQuantity { get; set; }
        public bool IsUpdateName { get; set; }
        public bool IsDir { get; set; }
        public bool IsLiveAdd { get; set; }
        public string ConsultantStatus { get; set; }
        //public Guid? MappingKey { get; set; }
    }

    public class ConsultantPost
    {
        public int Type { get; set; }
        public EventUserType userType { get; set; }
        public Guid EventKey { get; set; }
        public int BtnType { get; set; }
        //ApplyBC use
        public int ApplyTicketTotal { get; set; }
        public int TicketQuantityPerSession { get; set; }
        public List<string> EventSessionkeys { get; set; }

        //VolunterBC use
        public int NormalTicketQuantityPerPerson { get; set; }
        public int VIPTicketQuantityPerPerson { get; set; }

        //BC use
        public int NormalTicketSettingQuantity { get; set; }
        public int VIPTicketSettingQuantity { get; set; }

        //xml
        public string sessionkeysXml { get; set; }
        public string consultantsXml { get; set; }
        public string ticketsXml { get; set; }
        public List<EventConsultant> EventConsultants { get; set; }
        public List<PB_EventSession> EventSessions { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }


    public class Ticket
    {
        public Guid TicketKey { get; set; }
        public Guid MappingKey { get; set; }
        public Guid SessionKey { get; set; }
        public Guid EventKey { get; set; }
        public string SMSToken { get; set; }
        //public Guid CustomerKey { get; set; }
        public DateTime SessionStartDate { get; set; }
        public DateTime SessionEndDate { get; set; }
        public TicketType TicketType { get; set; }
        //public short TicketFrom { get; set; }
       // public short TicketStatus { get; set; }
        //public DateTime? CheckinDate { get; set; }
        //public System.DateTime? CreatedDate { get; set; }
        //public string CreatedBy { get; set; }
        //public System.DateTime UpdatedDate { get; set; }
        //public string UpdatedBy { get; set; }

    }

    public class TicketOutConsultant
    {
        public string DirectSellerID { get; set; }
        public string Name { get; set; }
        public int NormalSettingCount { get; set; }
        public int VIPSettingCount { get; set; }
        public int NormalRealCount { get; set; }
        public int VIPRealCount { get; set; }
    }

}