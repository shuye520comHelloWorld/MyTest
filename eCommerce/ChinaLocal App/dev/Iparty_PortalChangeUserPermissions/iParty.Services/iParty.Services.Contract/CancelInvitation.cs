using ServiceStack;
using System;

namespace iParty.Services.Contract
{
    [Api("Cancel Invitation for a party event.")]
    [Route("/parties/{InvitationKey}/invitations", "DELETE, OPTIONS")]
    [Route("/parties/{InvitationKey}/invitations/delete", "POST, OPTIONS")]
    public sealed class CancelInvitation : CommandRequestDto, IReturn<InvitationCommandResponse>
    {
        [ApiMember(Name = "InvitationKey", ParameterType = "path", Description = "the unique identifier of the party invitation", DataType = "guid", IsRequired = true)]
        public Guid InvitationKey { get; set; }
    }
    
}
