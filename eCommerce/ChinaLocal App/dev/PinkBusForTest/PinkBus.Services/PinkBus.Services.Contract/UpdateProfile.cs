using PinkBus.Services.Common;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.Services.Contract
{
    [Api(@"Update Volunteer address information while Volunteer confirmed in intouch ")]
    [Route("/consultant/profile/put", "POST, OPTIONS")]
    [Route("/consultant/profile", "PUT, OPTIONS")]
    public class UpdateProfile : CommandRequestDto, IReturn<UpdateProfileResponse>
    {
        [ApiMember(Name = "CountyCode", ParameterType = "body", Description = "the county code  ", DataType = "string", IsRequired = false)]
        public int CountyCode { get; set; }

        [ApiMember(Name = "CountyName", ParameterType = "body", Description = "the county name  ", DataType = "string", IsRequired = true)]
        public string CountyName { get; set; }
      
        [ApiMember(Name = "EventKey", ParameterType = "body", DataType = "guid", Description = "the id of the Event ", IsRequired = true)]
        public Guid EventKey { get; set; }
    }

    public class UpdateProfileResponse : CommandResponseDto
    {
        [ApiMember(Name = "Result", ParameterType = "body", DataType = "bool", Description = "indicate the result of this operation", IsRequired = true)]
        public bool Result { get; set; }

        [ApiMember(Name = "ErrorMessage", ParameterType = "body", DataType = "string", Description = "erorMessage", IsRequired = false)]
        public string ErrorMessage { get; set; }
    }



}
