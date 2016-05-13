
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.Services.Contract
{

    /// <summary>
    /// Event query response base entity
    /// </summary>
    public class EventBaseInfo
    {
        [ApiMember(Name = "EventKey", ParameterType = "body", DataType = "guid", Description = "identifier of event", IsRequired = true)]
        public Guid? EventKey { get; set; }

        [ApiMember(Name = "EventTitle", ParameterType = "body", DataType = "string", Description = "title of event", IsRequired = true)]
        public string EventTitle { get; set; }

        [ApiMember(Name = "Location", ParameterType = "body", DataType = "string", Description = "location of event", IsRequired = true)]
        public string Location { get; set; }

        [ApiMember(Name = "EventStartDate", ParameterType = "body", DataType = "datetime", Description = "starttime of event", IsRequired = true)]
        public DateTime EventStartDate { get; set; }

        [ApiMember(Name = "EventEndDate", ParameterType = "body", DataType = "datetime", Description = "endtime of event", IsRequired = true)]
        public DateTime EventEndDate { get; set; }
        
        //[ApiMember(Name = "InvitationEndDate", ParameterType = "body", DataType = "datetime", Description = "the end time of invitation", IsRequired = true)]
        //public DateTime InvitationEndDate { get; set; }

    }

    public class EventQueryBaseResponse : EventBaseInfo
    {
        [ApiMember(Name = "EventStage", ParameterType = "body", DataType = "Enum", Description = "the stage of event {OpenForCountDown,OpenForScrambleTicket,TicketOutOfStock,OpenForInvitation,OpenForDownloadData,OpenForOffline,OpenForHistory}", IsRequired = true)]
        public EventStage EventStage { get; set; }

        [ApiMember(Name = "BCType", ParameterType = "body", DataType = "enum", Description = "bc type,the variable value are NormalBC,BestowalBC, VIPBC,VolunteerBC", IsRequired = true)]
        public EventUserType BCType { get; set; }

        [ApiMember(Name = "NormalTicketLeftCount", ParameterType = "body", DataType = "int", Description = "left the normal ticket count of BC in current event", IsRequired = false)]
        public int NormalTicketLeftCount { get; set; }
        //贵宾券
        [ApiMember(Name = "VIPTicketLeftCount", ParameterType = "body", DataType = "int", Description = " left the vip ticket count of Volunteer/Vip in current event", IsRequired = false)]
        public int VIPTicketLeftCount { get; set; }

        [ApiMember(Name = "InvitedCustomerCount", ParameterType = "body", DataType = "int", Description = "the customer invited count of BC in current event", IsRequired = false)]
        public int InvitedCustomerCount { get; set; }

        [ApiMember(Name = "CheckedInCustomerCount", ParameterType = "body", DataType = "int", Description = "the checkedin quantity of customer whom invitated by current bc ", IsRequired = false)]
        public int CheckedInCustomerCount { get; set; }     

        [ApiMember(Name = "MappingKey", ParameterType = "body", DataType = "guid", Description = "the identifier of consultant in an event", IsRequired = true)]
        public Guid MappingKey { get; set; }


    }

    public class EventQueryForBCResponse : EventQueryBaseResponse
    {
        [ApiMember(Name = "TimeRemaining", ParameterType = "body", DataType = "int", Description = "it is the remain time to event start ", IsRequired = false)]
        public int TimeRemaining { get; set; }

    }


    /// <summary>
    /// query for Volunteer
    /// </summary>
    public class EventQueryForVolunteerResponse : EventQueryBaseResponse
    {      
        [ApiMember(Name = "CountyConfirmed", ParameterType = "body", DataType = "bool", Description = "indicate volunteer confirmed her information in intouch", IsRequired = false)]
        public bool CountyConfirmed { get; set; }

    }
    /// <summary>
    /// query for vip
    /// </summary>
    public class EventQueryForVipResponse : EventQueryBaseResponse
    {     
      
    }
    /// <summary>
    /// query for historical event
    /// </summary>
    public class EventHistoryResponse : EventQueryBaseResponse
    {
        [ApiMember(Name = "CheckInCount", ParameterType = "body", DataType = "int", Description = "the checked in count of BC in current event", IsRequired = false)]
        public int CheckInCount { get; set; }     
 
    }

    //活动场次信息
    public class EventSessionResponse : EventBaseInfo
    {       
        [ApiMember(Name = "EventSession", ParameterType = "body", DataType = "list", Description = "the checked in count of BC in current event", IsRequired = true)]
        public List<EventSession> EventSession { get; set; }
    }

    public class EventSession
    {
        [ApiMember(Name = "SessionKey", ParameterType = "body", DataType = "string", Description = "the unique  identifier session", IsRequired = true)]
        public Guid SessionKey { get; set; }

        [ApiMember(Name = "SessionStartDate", ParameterType = "body", DataType = "datetime", Description = "starttime of session", IsRequired = true)]
        public DateTime SessionStartDate { get; set; }

        [ApiMember(Name = "SessionEndDate", ParameterType = "body", DataType = "datetime", Description = "endtime of session", IsRequired = true)]
        public DateTime SessionEndDate { get; set; }

        [ApiMember(Name = "ApplyQuantityPerSession", ParameterType = "body", DataType = "int", Description = "applyTicket quantity of current session", IsRequired = true)]
        public int ApplyQuantityPerSession { get; set; }

        [ApiMember(Name = "TicketOut", ParameterType = "body", DataType = "bool", Description = "the ticket of current session whether is out stock", IsRequired = true)]
        public bool TicketOut { get; set; }

        [ApiMember(Name = "CanApply", ParameterType = "body", DataType = "bool", Description = "indicate current session whether can be applied ticket for bc", IsRequired = true)]
        public bool CanApply { get; set; }
    }






}
