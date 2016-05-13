using PinkBus.Services.Common;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.ClientServices.Contract
{

    [Api("")]
    [Route("/client/uploadcustomerdata", "POST, OPTIONS")]
    public class UploadCustomerData : CommandRequestDto, IReturn<UploadCustomerDataResponse>
    {
        [ApiMember(Name = "UploadPassCode", ParameterType = "body", Description = "", DataType = "string", IsRequired = true)]
        public string UploadPassCode { get; set; }

        ///其他数据21lou 

    }

    public class UploadCustomerDataResponse : CommandResponseDto
    {

    }
}
