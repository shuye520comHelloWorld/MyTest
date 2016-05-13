using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PinkBus.Services.Common;

namespace PinkBus.Services.Contract
{

    [Api(@"Create invitation while BC send the invitation link to customer ")]
    [Route("/invitations/put", "POST, OPTIONS")]
    [Route("/invitations", "PUT, OPTIONS")]
    public class CreateInvitation : CommandRequestDto, IReturn<CreateInvitationResponse>
    {
        [ApiMember(Name = "TicketKey", ParameterType = "body", Description = "the unique identifier of the ticket", DataType = "guid", IsRequired = true)]
        public Guid TicketKey { get; set; }
    }

    public class CreateInvitationResponse : CommandResponseDto
    {
        [ApiMember(Name = "Result", ParameterType = "body", DataType = "bool", Description = "indicate the result of this operation", IsRequired = true)]
        public bool Result { get; set; }

        [ApiMember(Name = "ErrorMessage", ParameterType = "body", DataType = "string", Description = "ErrorMessage", IsRequired = false)]
        public string ErrorMessage { get; set; }
    }
}
