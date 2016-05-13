using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Services.Contract
{
    [Api(Description = "Create a wechart card")]
    [Route("/wechart/cards", "POST, OPTIONS")]
    public sealed class CreateWeChartCard : CommandRequestDto, IReturn<CreateWeChartCardResponse>, IPublicRequestDto
    {
        [ApiMember(Name = "CardCode", ParameterType = "body", Description = "the key of an invitation.", DataType = "guid", IsRequired = true)]
        public Guid CardCode { get; set; }
        [ApiMember(Name = "CardID", ParameterType = "body", Description = "the wechart card id.", DataType = "string", IsRequired = true)]
        public string CardID { get; set; }
        [ApiMember(Name = "PartyKey", ParameterType = "body", Description = "the key of a party.", DataType = "guid", IsRequired = true)]
        public Guid PartyKey { get; set; }
    }

    public sealed class CreateWeChartCardResponse : CommandResponseDto
    {
        public Guid CardCode { get; set; }
        public string CardID { get; set; }
        public Guid PartyKey { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
