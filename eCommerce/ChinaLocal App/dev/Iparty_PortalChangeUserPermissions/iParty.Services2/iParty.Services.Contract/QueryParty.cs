using iParty.Services.Entity;
using iParty.Services.Contract.Filters;
using ServiceStack;
using System;
using System.Collections.Generic;

namespace iParty.Services.Contract
{
    [Api(Description = "Query parties of the fitting description.")]
    [Route("/query/parties", "GET, OPTIONS")]
    public sealed class QueryParty : QueryRequestDto, IReturn<QueryPartyResponse>, IFQueryParty
    {
        #region filters

        [ApiMember(Name = "EventKey", ParameterType = "path", Description = "the unique identifier of the event", DataType = "guid", IsRequired = false)]
        public Guid? EventKey { get; set; }

        [ApiMember(Name = "PartyKey", ParameterType = "path", Description = "the unique identifier of the party", DataType = "guid", IsRequired = false)]
        public Guid? PartyKey { get; set; }

        [ApiMember(Name = "PartyStage", ParameterType = "path", Description = "current stage of the party", DataType = "enum", IsRequired = false)]
        public PartyStage? PartyStage { get; set; }

        [ApiMember(Name = "IsFinished", ParameterType = "path", Description = "only include the finished parties", DataType = "boolean", IsRequired = false)]
        public bool? IsFinished { get; set; }

        #endregion
    }

    public sealed class QueryPartyResponse : QueryResponseDto
    {
        public List<object> Results { get; set; }
    }
}
