using PinkBus.Services.Common;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.Services.Contract
{
    [Api(@"Query  apply ticket result in intouch")]
    [Route("/ticket/apply/query", "GET, OPTIONS")]
    public class QueryApplyTicketResult : QueryRequestDto, IReturn<QueryApplyTicketResultResponse>
    {
        [ApiMember(Name = "ApplyTicketTrackerId", ParameterType = "body", Description = "the unique identifier of apply ticket tracker", DataType = "string", IsRequired = true)]
        public Guid ApplyTicketTrackerId { get; set; }
    }

    public class QueryApplyTicketResultResponse : QueryResponseDto
    {
        [ApiMember(Name = "ApplyTicketResult", ParameterType = "body", Description = "the apply ticket result,variable value are Unkonw,Success,Fail", DataType = "enum", IsRequired = true)]
        public ApplyTicketResult ApplyTicketResult { get; set; }
    }

   
}
