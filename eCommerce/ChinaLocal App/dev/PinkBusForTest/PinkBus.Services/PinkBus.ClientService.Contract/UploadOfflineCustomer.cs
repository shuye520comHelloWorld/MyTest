using PinkBus.Services.Common;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.ClientServices.Contract
{

    [Api("upload offline customer ")]
    [Route("/client/offlinecustomer/{EventKey}", "POST, OPTIONS")]   
    public class UploadOfflineCustomer : CommandRequestDto, IReturn<UploadOfflineCustomerResponse>
    {
        [ApiMember(Name = "EventKey", ParameterType = "path", Description = "", DataType = "Guid", IsRequired = true)]
        public Guid EventKey { get; set; }

        [ApiMember(Name = "UploadPassCode", ParameterType = "body", Description = "", DataType = "string", IsRequired = true)]
        public string UploadPassCode { get; set; }

        public string JsonData { get; set; }
    }

    public class UploadOfflineCustomerResponse : CommandResponseDto
    {
        public bool Result { get; set; }
        public string Message { get; set; }
    }
}
