using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Services.Contract
{
    [Api("generate a invitation token of a party.")]
    [Route("/parties/{partyKey}/token", "GET, OPTIONS")]
    public sealed class InvitationTokenRequest : CommandRequestDto, IReturn<InvitationTokenResponse>
    {
        [ApiMember(Name = "PartyKey", ParameterType = "path", Description = "the unique identifier of the party.", DataType = "guid", IsRequired = true)]
        public Guid PartyKey { get; set; }
    }

    public sealed class InvitationTokenResponse : CommandResponseDto
    {
        public string Token { get; set; }
    }
}
