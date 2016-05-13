using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PinkBus.Services.Common;
namespace PinkBus.Services.Contract
{

    [Api(@"Normal BC can loot ticket while event is OpenForScrambleTicket stage in intouch")]
    [Route("/tickets/apply", "POST, OPTIONS")]
    public class ApplyTicket : CommandRequestDto, IReturn<ApplyTicketResponse>
    {
        [ApiMember(Name = "Sessionkey", ParameterType = "guid", Description = "the unique identifier of session", DataType = "string", IsRequired = true)]
        public Guid SessionKey { get; set; }

        [ApiMember(Name = "MappingKey", ParameterType = "body", Description = "the unique identifier of consultant who attend one specfic event", DataType = "guid", IsRequired = true)]
        public Guid MappingKey { get; set; }
    }

    public class ApplyTicketResponse : CommandResponseDto
    {
        [ApiMember(Name = "ApplyTicketTrackerId", ParameterType = "body", Description = "the unique identifier of event", DataType = "string", IsRequired = true)]
        public Guid ApplyTicketTrackerId { get; set; }
    }
}
