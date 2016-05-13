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
    [Route("/client/uploadcheckindata/{EventKey}", "POST, OPTIONS")]
    public class UploadCheckinData : CommandRequestDto, IReturn<UploadCheckinDataResponse>
    {
        [ApiMember(Name = "EventKey", ParameterType = "path", Description = "", DataType = "Guid", IsRequired = true)]
        public Guid EventKey { get; set; }

        public string UploadJsonData { get; set; }

        [ApiMember(Name = "SyncDataType", ParameterType = "body", Description = "consultant=1,ticket=2,customer=3,volunteercheckin=4", DataType = "string", IsRequired = true)]    
        public int SyncDataType { get; set; }
    }

    public class UploadCheckinDataResponse : CommandResponseDto
    {
        public bool Result { get; set; }
        public string ContactId { get; set; } 
    }
}
