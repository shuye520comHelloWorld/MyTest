using iParty.Services.Entity;
using ServiceStack;
using System;
using System.Collections.Generic;

namespace iParty.Services.Contract
{
    [Api("Apply for a party event.")]
    [Route("/parties/applications", "POST, OPTIONS")]
    public sealed class CreatePartyApplication : CommandRequestDto, IReturn<PartyApplicationCommandResponse>
    {
        [ApiMember(Name = "EventKey", ParameterType = "body", Description = "the unique identifier of the event", DataType = "guid", IsRequired = true)]
        public Guid EventKey { get; set; }

        [ApiMember(Name = "WorkShopId", ParameterType = "body", Description = "the work shop id of the application", DataType = "int", IsRequired = true)]
        public int WorkshopId { get; set; }


        [ApiMember(Name = "UnionConsultants", ParameterType = "body", Description = "the union consultant id list .", DataType = "IList<long>", IsRequired = false)]
        public IList<long> UnionConsultants { get; set; }

        [ApiMember(Name = "StartDateUtc", ParameterType = "body", Description = "the utc time of the application start", DataType = "datetime", IsRequired = true)]
        public DateTime StartDateUtc { get; set; }

        [ApiMember(Name = "EndDateUtc", ParameterType = "body", Description = "the utc time of the application end", DataType = "datetime", IsRequired = true)]
        public DateTime EndDateUtc { get; set; }

       
        [ApiMember(Name = "CreateDateUtc", ParameterType = "body", Description = "the utc time of the application creation; when not provided, it's default to the utc time when the request arrives at the service.", DataType = "datetime", IsRequired = false)]
        public DateTime? CreateDateUtc { get; set; }


    }

    public sealed class PartyApplicationCommandResponse : CommandResponseDto
    {
        public Guid PartyKey { get; set; }
        public Guid EventKey { get; set; }
        public DateTime CreateDateUtc { get; set; }
    }
}
