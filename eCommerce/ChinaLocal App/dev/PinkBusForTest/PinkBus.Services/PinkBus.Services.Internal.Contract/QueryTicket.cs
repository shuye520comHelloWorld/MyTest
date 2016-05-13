using PinkBus.Services.Common;
using PinkBus.Services.Entity;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.Services.Internal.Contract
{
    [Api("return tickets list while pink bus customer view her tickets list in wechat service h5 page")]
    [Route("/tickets/query", "GET,Options")]
    public class QueryTicket : QueryRequestDto, IReturn<QueryTicketResponse>
    {
        [ApiMember(Name = "UnionID", ParameterType = "body", Description = "the union id of a customer", DataType = "string", IsRequired = true)]
        public new string UnionID { get; set; }

    }

    public class QueryTicketResponse : QueryResponseDto
    {
        [ApiMember(Name = "Tickets", ParameterType = "body", Description = "tickets list ", DataType = "array", IsRequired = true)]
        public List<Ticket> Tickets { get; set; }
    }

    public class Ticket
    {
        [ApiMember(Name = "TicketKey", ParameterType = "body", Description = "the identifier of ticket", DataType = "guid", IsRequired = true)]
        public Guid TicketKey { get; set; }

        [ApiMember(Name = "EventTitle", ParameterType = "body", Description = "the event title", DataType = "string", IsRequired = true)]
        public string EventTitle { get; set; }

        [ApiMember(Name = "EventLocation", ParameterType = "body", Description = "the event content", DataType = "string", IsRequired = true)]
        public string EventLocation { get; set; }

        [ApiMember(Name = "SessionStartDate", ParameterType = "body", Description = "the start validate date of ticket  ", DataType = "datetime", IsRequired = false)]
        public DateTime SessionStartDate { get; set; }

        [ApiMember(Name = "SessionEndDate", ParameterType = "body", Description = "the end validate date of ticket  ", DataType = "datetime", IsRequired = false)]
        public DateTime SessionEndDate { get; set; }
        
        [ApiMember(Name = "ContactId", ParameterType = "body", Description = "the contact id of consultant whom send the ticket ", DataType = "long", IsRequired = true)]
        public long ContactId { get; set; }

        [ApiMember(Name = "ConsultantName", ParameterType = "body", Description = "the name of consultant whom send the ticket ", DataType = "string", IsRequired = true)]
        public string ConsultantName { get; set; }

        [ApiMember(Name = "ConsultantPhoneNumber", ParameterType = "body", Description = "the phonenumber of consultant whom send the ticket ", DataType = "string", IsRequired = true)]
        public string ConsultantPhoneNumber { get; set; }

        [ApiMember(Name = "TicketType", ParameterType = "body", Description = "the ticket type of the ticket,variable value are vip/normal", DataType = "enum", IsRequired = true)]
        public TicketType TicketType { get; set; }

        [ApiMember(Name = "TicketStatus", ParameterType = "body", Description = "the status of ticket ,variable value are Unused,Used,Invalid", DataType = "enum", IsRequired = true)]
        public TicketStatus TicketStatus { get; set; }

        [ApiMember(Name = "CheckinDate", ParameterType = "body", Description = "the checkin date of the ticket", DataType = "datetime", IsRequired = false)]
        public DateTime CheckinDate { get; set; }

        [ApiMember(Name = "SMSToken", ParameterType = "body", Description = "the SMSToken of the ticket", DataType = "string", IsRequired = false)]
        public string SMSToken { get; set; }

        [ApiMember(Name = "CreatedDate", ParameterType = "body", Description = "the created dare of the ticket", DataType = "datetime", IsRequired = true)]
        public DateTime CreatedDate { get; set; } 
    }
}
