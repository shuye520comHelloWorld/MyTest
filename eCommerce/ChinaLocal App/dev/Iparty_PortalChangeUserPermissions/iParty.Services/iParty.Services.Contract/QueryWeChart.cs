using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Services.Contract
{
    [Api(Description = "Query wechart card ID of a wechart card.")]
    [Route("/query/wecards", "POST, OPTIONS")]
    public sealed class QueryWeChart : CommandRequestDto, IReturn<QueryWeChartResponse>, IPublicRequestDto
    {
        [ApiMember(Name = "InvitationToken", ParameterType = "body", Description = "the InvitationToken", DataType = "string", IsRequired = true)]
        public string InvitationToken { get; set; }
    }

    public sealed class QueryWeChartResponse : CommandResponseDto
    {
        public Guid EventKey { get; set; }
        public string CardID { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
