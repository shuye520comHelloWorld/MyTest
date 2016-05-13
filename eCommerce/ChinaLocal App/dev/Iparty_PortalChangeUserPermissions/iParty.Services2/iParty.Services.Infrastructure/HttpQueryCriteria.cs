using iParty.Services.Contract;
using iParty.Services.Interface;
using QuartES.Services.Core;
using ServiceStack.Web;

namespace iParty.Services.Infrastructure
{
    public class HttpQueryCriteria : QueryCriteria, IServiceStackQueryCriteria
    {
        public HttpQueryCriteria() { }

        public void Initialize(IRequest req, IResponse res, object dto)
        {
            var queryDto = dto as QueryRequestDto;
            if (queryDto != null)
            {
                this.Limit = queryDto.Limit;
                this.Offset = queryDto.Offset;
                if (!string.IsNullOrWhiteSpace(queryDto.Fields))
                {
                    this.Fields = queryDto.Fields.Split(',');
                }
                if (!string.IsNullOrWhiteSpace(queryDto.Sort))
                {
                    this.Sort = queryDto.Sort.Split(',');
                }
                if (!string.IsNullOrWhiteSpace(queryDto.Q))
                {
                    this.Q = queryDto.Q.Split(' ');
                }
                this.Filters = dto;
            }
        }
    }
}
