using iParty.Services.Entity;
using ServiceStack;
using System;
using System.Collections.Generic;

namespace iParty.Services.Contract
{
    [Api("Update application for a party event.")]
    [Route("/parties/{PartyKey}/applications", "PUT, OPTIONS")]
    [Route("/parties/{PartyKey}/applications/put", "POST, OPTIONS")]
    public sealed class UpdatePartyApplication : CommandRequestDto, IReturn<PartyApplicationCommandResponse>
    {
        [ApiMember(Name = "PartyKey", ParameterType = "path", Description = "the unique identifier of the party event", DataType = "guid", IsRequired = true)]
        public Guid PartyKey { get; set; }

        [ApiMember(Name = "UnionConsultants", ParameterType = "body", Description = "the union consultant id list .", DataType = "IList<long>", IsRequired = false)]
        public IList<long> UnionConsultants { get; set; }

        [ApiMember(Name = "StartDateUtc", ParameterType = "body", Description = "the utc time of the application start", DataType = "datetime", IsRequired = true)]
        public DateTime StartDateUtc { get; set; }

        [ApiMember(Name = "EndDateUtc", ParameterType = "body", Description = "the utc time of the application end", DataType = "datetime", IsRequired = true)]
        public DateTime EndDateUtc { get; set; } 
       
    }
}
