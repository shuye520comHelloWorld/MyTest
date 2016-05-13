using PinkBus.Services.Common;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.Services.Contract
{
    [Api("Cancel Invitation while BC cancellation her customer's ticket")]
    [Route("/customers/{TicketKey}/invitations", "DELETE, OPTIONS")]
    [Route("/customers/{TicketKey}/invitations/delete", "POST, OPTIONS")]
    public sealed class CancelInvitation : CommandRequestDto, IReturn<CancelInvitationResponse>
    {
        [ApiMember(Name = "TicketKey", ParameterType = "path", Description = "the unique identifier of the ticket", DataType = "guid", IsRequired = true)]
        public Guid TicketKey { get; set; }
    }

    public class CancelInvitationResponse : CommandResponseDto
    {
        [ApiMember(Name = "Result", ParameterType = "body", DataType = "bool", Description = "indicate the result of this operation", IsRequired = true)]
        public bool Result { get; set; }

        [ApiMember(Name = "ErrorMessage", ParameterType = "body", DataType = "string", Description = "ErrorMessage", IsRequired = false)]
        public string ErrorMessage { get; set; }
    }
}
