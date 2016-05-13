
using PinkBus.Services.Common;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.Services.Contract
{

    [Api(@"BC forwarded tickets to another BC")]
    [Route("/tickets/bestowal", "PUT, OPTIONS")]
    [Route("/tickets/bestowal/put", "POST, OPTIONS")]
    public class BestowalTicket : CommandRequestDto, IReturn<BestowalTicketResponse>
    {
        [ApiMember(Name = "TicketKey", ParameterType = "body", Description = "the unique  identifier of ticket ", DataType = "guid", IsRequired = true)]
        public Guid TicketKey { get; set; }

        [ApiMember(Name = "EventKey", ParameterType = "body", DataType = "guid", Description = "the unqiue identifier of the Event ", IsRequired = true)]
        public Guid EventKey { get; set; }

        [ApiMember(Name = "ConsultantName", ParameterType = "body", Description = "the name of the other consultant", DataType = "string", IsRequired = true)]
        public string ConsultantName { get; set; }

        [ApiMember(Name = "ConsultantPhone", ParameterType = "body", Description = "the phone number of other consultant", DataType = "string", IsRequired = false)]
        public string ConsultantPhone { get; set; }

        [ApiMember(Name = "ContactId", ParameterType = "body", Description = "the consultants ContactId in system", DataType = "long", IsRequired = false)]
        public long ContactId { get; set; }

        [ApiMember(Name = "DirectSellerId", ParameterType = "body", Description = "the consultants DirectSellerId in system", DataType = "string", IsRequired = true)]
        public string DirectSellerId { get; set; } 
    }

    public class BestowalTicketResponse : CommandResponseDto
    {
        [ApiMember(Name = "TicketKey", ParameterType = "body", Description = "the unique  identifier of new ticket ", DataType = "guid", IsRequired = true)]
        public Guid TicketKey { get; set; }

        [ApiMember(Name = "TicketStatus", ParameterType = "body", Description = "ticket status", DataType = "enum", IsRequired = true)]
        public TicketStatus TicketStatus { get; set; }

        [ApiMember(Name = "Result", ParameterType = "body", DataType = "bool", Description = "indicate the result of this operation", IsRequired = true)]
        public bool Result { get; set; }

        [ApiMember(Name = "ErrorMessage", ParameterType = "body", DataType = "string", Description = "ErrorMessage", IsRequired = false)]
        public string ErrorMessage { get; set; }
    }

}
