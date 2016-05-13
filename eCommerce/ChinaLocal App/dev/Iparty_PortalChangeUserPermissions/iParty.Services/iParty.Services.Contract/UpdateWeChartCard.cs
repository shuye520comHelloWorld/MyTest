using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Services.Contract
{
    [Api("Update a wecard.")]
    [Route("/wechart/{CardCode}/cards", "PUT, OPTIONS")]
    [Route("/wechart/{CardCode}/cards/put", "POST, OPTIONS")]
    public sealed class UpdateWeChartCard : CommandRequestDto, IReturn<CreateWeChartCardResponse>, IPublicRequestDto
    {
        [ApiMember(Name = "CardCode", ParameterType = "path", Description = "the key of a wecard.", DataType = "guid", IsRequired = true)]
        public Guid CardCode { get; set; }
        [ApiMember(Name = "Status", ParameterType = "body", Description = "the status of a wecard.", DataType = "int", IsRequired = true)]
        public int Status { get; set; }
    }
}
