
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PinkBus.Services.Common;
namespace PinkBus.Services.Contract
{


    [Api("Get the ticket detail information in intouch ")]
    [Route("/ticket/{TicketKey}", "GET, OPTIONS")]
    public class GetTicketDetail : QueryRequestDto, IReturn<GetTicketDetailResponse>
    {
        [ApiMember(Name = "TicketKey", ParameterType = "path", Description = "the unique identifier of ticket", DataType = "guid", IsRequired = true)]
        public Guid TicketKey { get; set; }

        [ApiMember(Name = "EventKey", ParameterType = "body", DataType = "guid", Description = "the unique identifier of the Event ", IsRequired = true)]
        public Guid EventKey { get; set; }
    }

    public class GetTicketDetailResponse : QueryResponseDto
    {
        [ApiMember(Name = "EventBaseInfo", ParameterType = "body", DataType = "object", Description = "including current event concrete information", IsRequired = true)]
        public EventBaseInfo EventBaseInfo { get; set; }

        [ApiMember(Name = "SessionStartDate", ParameterType = "body", DataType = "datetime", Description = "the start date of event session", IsRequired = true)]
        public DateTime SessionStartDate { get; set; }

        [ApiMember(Name = "SessionEndDate", ParameterType = "body", DataType = "datetime", Description = "the end date of event session", IsRequired = true)]
        public DateTime SessionEndDate { get; set; }

        [ApiMember(Name = "FirstName", ParameterType = "body", Description = "the first name of the consultant", DataType = "string", IsRequired = true)]
        public string FirstName { get; set; }

        [ApiMember(Name = "LastName", ParameterType = "body", Description = "the last name of the consultant", DataType = "string", IsRequired = true)]
        public string LastName { get; set; }

        [ApiMember(Name = "ConsultantPhone", ParameterType = "body", Description = "the phone number of the unit consultant", DataType = "string", IsRequired = true)]
        public string ConsultantPhone { get; set; }

        [ApiMember(Name = "TicketKey", ParameterType = "body", DataType = "guid", Description = "the unique indentifier of ticket", IsRequired = true)]
        public Guid TicketKey { get; set; }

        [ApiMember(Name = "TicketType", ParameterType = "body", DataType = "enum", Description = "ticket type,variable are normal,honner", IsRequired = true)]
        public TicketType TicketType { get; set; }

        [ApiMember(Name = "TicketStatus", ParameterType = "body", DataType = "enum", Description = "ticket status,variable are Created,Inviting,Invited,Bestowed,Canceled, Checkin,UnCheckin,Expired", IsRequired = true)]
        public TicketStatus TicketStatus { get; set; }
        //move this property to event base info
        [ApiMember(Name = "InvitationEndDate", ParameterType = "body", DataType = "datetime", Description = "the end time of invitation", IsRequired = true)]
        public DateTime InvitationEndDate { get; set; }

        [ApiMember(Name = "CustomerKey", ParameterType = "body", Description = "the unique identifier of customer", DataType = "Guid", IsRequired = false)]
        public Guid CustomerKey { get; set; }

        [ApiMember(Name = "CustomerName", ParameterType = "body", DataType = "string", Description = "tcustomer name", IsRequired = false)]
        public string CustomerName { get; set; }

        [ApiMember(Name = "CustomerPhone", ParameterType = "body", DataType = "string", Description = "customer phone ", IsRequired = false)]
        public string CustomerPhone { get; set; }

        [ApiMember(Name = "SMSToken", ParameterType = "body", DataType = "string", Description = "SMS token ", IsRequired = true)]
        public string SMSToken { get; set; }

        [ApiMember(Name = "SharedUrl", ParameterType = "body", DataType = "string", Description = "the url shared to custoemr via wechat/sms while send one intivation ", IsRequired = true)]
        public string SharedUrl { get; set; }
        
    }
}
