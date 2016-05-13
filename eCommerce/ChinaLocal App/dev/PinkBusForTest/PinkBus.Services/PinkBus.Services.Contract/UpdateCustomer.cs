using PinkBus.Services.Common;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.Services.Contract
{

    [Api(@"Update customer status whether import my customer ")]
    [Route("/customers/{CustomerKey}", "PUT, OPTIONS")]
    [Route("/customers/{CustomerKey}/put", "POST, OPTIONS")]
    public class UpdateCustomer : CommandRequestDto, IReturn<UpdateCustomerResponse>
    {
        [ApiMember(Name = "CustomerKey", ParameterType = "path", Description = "the unique identifier of customer  ", DataType = "guid", IsRequired = true)]
        public Guid CustomerKey { get; set; }

        [ApiMember(Name = "IsImportMyCustomer", ParameterType = "body", DataType = "bool", Description = "indicate whether the customer imported to MyCustomer", IsRequired = false)]
        public bool IsImportMyCustomer { get; set; }
    }

    public class UpdateCustomerResponse : CommandResponseDto
    {
        [ApiMember(Name = "Result", ParameterType = "body", DataType = "bool", Description = "indicate the result of this operation", IsRequired = true)]
        public bool Result { get; set; }

        [ApiMember(Name = "ErrorMessage", ParameterType = "body", DataType = "string", Description = "ErrorMessage", IsRequired = false)]
        public string ErrorMessage { get; set; }
        
    }
}
