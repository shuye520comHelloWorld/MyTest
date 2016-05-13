using PinkBus.Services.Common;
using PinkBus.Services.Entity;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.ClientServices.Contract
{

    [Api("")]
    [Route("/client/downloaddata/{EventKey}", "GET, OPTIONS")]
    public class DownloadData : QueryRequestDto, IReturn<DownloadDataResponse>
    {
        [ApiMember(Name = "EventKey", ParameterType = "path", Description = "", DataType = "Guid", IsRequired = true)]
        public Guid EventKey { get; set; }

        [ApiMember(Name = "SyncDataType", ParameterType = "body", Description = "consultant=1,ticket=2,customer=3", DataType = "int", IsRequired = true)]
        public int SyncDataType { get; set; }

    }

    public class DownloadDataResponse : QueryResponseDto
    {
        public string data { get; set; }
    }
}
