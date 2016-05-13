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
    [Route("/client/checktoken/{Token}", "GET, OPTIONS")]
    public class CheckToken : QueryRequestDto, IReturn<CheckTokenResponse>
    {
        [ApiMember(Name = "Token", ParameterType = "path", Description = "", DataType = "string", IsRequired = true)]
        public string Token { get; set; }

        [ApiMember(Name = "TokenType", ParameterType = "body", Description = "tokentype is download or upload ;download is 1,upload is 2", DataType = "int", IsRequired = true)]
        public int TokenType { get; set; }

    }

    public class CheckTokenResponse : QueryResponseDto
    {
        //public bool Result { get; set; }
        public Event Event { get; set; }
        public List<EventSession> EventSessions { get; set; }
    }
}
