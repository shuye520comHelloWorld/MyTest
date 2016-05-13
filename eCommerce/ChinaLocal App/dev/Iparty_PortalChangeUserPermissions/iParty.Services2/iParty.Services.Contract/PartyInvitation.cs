using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Services.Contract
{
    [Api("get the party's infomation when invite a customer in wepage.")]
    [Route("/parties/invitations/party", "POST, OPTIONS")]
    public class PartyInvitation : CommandRequestDto, IReturn<PartyInvitationResponse>, IPublicRequestDto
    {
        [ApiMember(Name = "InvitationToken", ParameterType = "body", Description = "the InvitationToken", DataType = "string", IsRequired = true)]
        public string InvitationToken { get; set; }
    }

    public class PartyInvitationResponse : CommandResponseDto
    {
        public Guid PartyKey { get; set; }
        public string EventTitle { get; set; }
        public string WorkshopLocation { get; set; }
        public DateTime PartyStartDate { get; set; }
        public DateTime PartyEndDate { get; set; }
        public DateTime PartyDisplayStartDate { get; set; }
        public DateTime PartyDisplayEndDate { get; set; }
        public DateTime ApplicationStartDate { get; set; }
        public DateTime ApplicationEndDate { get; set; }
        public long ForwordContactID { get; set; }
        public string ForwordName { get; set; }
    }
}
