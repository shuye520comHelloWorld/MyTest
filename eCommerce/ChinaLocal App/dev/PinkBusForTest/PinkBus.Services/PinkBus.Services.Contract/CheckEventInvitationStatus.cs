using PinkBus.Services.Common;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.Services.Contract
{

    [Api(@"check event invitation status before bc is going to consume her tickets,1. it is not the time to start invitation,return NotStart error, 2. it is the time to end invitation, return invitation end error")]
    [Route("/eventinvitation", "GET, OPTIONS")]
    public class CheckEventInvitationStatus : BaseRequestDto, IReturn<CheckEventInvitationStatusResponse>
    {
        [ApiMember(Name = "EventKey", ParameterType = "body", DataType = "guid", Description = "the id of the Event ", IsRequired = true)]
        public Guid EventKey { get; set; }
    }

    public class CheckEventInvitationStatusResponse : BaseResponseDto
    {
        [ApiMember(Name = "Result", ParameterType = "body", DataType = "bool", Description = "the check result indicate event whether start to invitation stage ", IsRequired = true)]
        public bool Result { get; set; }

        [ApiMember(Name = "Message", ParameterType = "body", DataType = "string", Description = "show the message while event did not start to invitation stage ", IsRequired = false)]
        public string Message { get; set; }
    }
}
