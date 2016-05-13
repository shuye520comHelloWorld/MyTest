
using PinkBus.Services.Common;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.Services.Contract
{
    [Api(@"Retrive ticket list(including status) of current BC in intouch")]
    [Route("/tickets/query", "GET, OPTIONS")]
    public class QueryTickets : QueryRequestDto, IReturn<QueryTicketsResponse>
    {
        [ApiMember(Name = "EventKey", ParameterType = "body", DataType = "guid", Description = "the id of the Event ", IsRequired = true)]
        public Guid EventKey { get; set; }
    }

    public class QueryTicketsResponse : QueryResponseDto
    {
        [ApiMember(Name = "EventBaseInfo", ParameterType = "body", DataType = "object", Description = "including current event concrete information", IsRequired = true)]
        public EventBaseInfo EventBaseInfo { get; set; }

        [ApiMember(Name = "Tickets", ParameterType = "body", DataType = "list", Description = "tickets of current bc", IsRequired = true)]
        public List<Ticket> Tickets { get; set; }
    }

    public class Ticket
    {
        [ApiMember(Name = "TicketKey", ParameterType = "body", DataType = "guid", Description = "the unique identifier of ticket", IsRequired = true)]
        public Guid TicketKey { get; set; }

        //[ApiMember(Name = "TicketQRKey", ParameterType = "body", DataType = "guid", Description = "the unique identifier of ticketQR", IsRequired = true)]
        //public Guid TicketQRKey { get; set; }

        [ApiMember(Name = "TicketType", ParameterType = "body", DataType = "enum", Description = "ticket type,variable are normal,VIP", IsRequired = true)]
        public TicketType TicketType { get; set; }

        [ApiMember(Name = "TicketStatus", ParameterType = "body", DataType = "enum", Description = "ticket status,variable are Created,Inviting,Invited,Bestowed,Canceled, Checkin,UnCheckin,Expired", IsRequired = true)]
        public TicketStatus TicketStatus { get; set; }

        [ApiMember(Name = "CustomerName", ParameterType = "body", DataType = "string", Description = "customer name", IsRequired = false)]
        public string CustomerName { get; set; }

        [ApiMember(Name = "CustomerPhone", ParameterType = "body", DataType = "string", Description = "customer phone ", IsRequired = false)]
        public string CustomerPhone { get; set; }

        [ApiMember(Name = "CustomerKey", ParameterType = "path", Description = "the unique identifier of customer", DataType = "Guid", IsRequired = false)]
        public Guid CustomerKey { get; set; }
    }
}
