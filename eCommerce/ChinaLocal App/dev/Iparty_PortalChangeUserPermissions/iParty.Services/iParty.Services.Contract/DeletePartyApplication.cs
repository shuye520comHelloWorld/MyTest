using ServiceStack;
using System;

namespace iParty.Services.Contract
{
    [Api("Remove application for a party event.")]
    [Route("/parties/{PartyKey}/applications", "DELETE, OPTIONS")]
    [Route("/parties/{PartyKey}/applications/delete", "POST, OPTIONS")]
    public sealed class DeletePartyApplication : CommandRequestDto, IReturn<PartyApplicationCommandResponse>
    {
        [ApiMember(Name = "PartyKey", ParameterType = "path", Description = "the unique identifier of the party event", DataType = "guid", IsRequired = true)]
        public Guid PartyKey { get; set; }
    }
}
