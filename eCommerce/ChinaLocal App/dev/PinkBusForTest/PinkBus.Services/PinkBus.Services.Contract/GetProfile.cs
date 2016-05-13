using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PinkBus.Services.Common;
namespace PinkBus.Services.Contract
{

    [Api(@"Get consultant profile while Volunteer confirmed her address information ")]
    [Route("/consultant/profile", "GET, OPTIONS")]
    public class GetProfile :BaseRequestDto, IReturn<GetProfileResponse>
    {
        [ApiMember(Name = "EventKey", ParameterType = "body", DataType = "guid", Description = "the id of the Event ", IsRequired = true)]
        public Guid EventKey { get; set; }
    }

    public class GetProfileResponse : BaseResponseDto
    {
        [ApiMember(Name = "EventBaseInfo", ParameterType = "body", DataType = "object", Description = "including current event concrete information", IsRequired = true)]
        public EventBaseInfo EventBaseInfo { get; set; }

        //[ApiMember(Name = "ConsultantName", ParameterType = "body", Description = "the full name of the consultant", DataType = "string", IsRequired = true)]
        //public string ConsultantName { get; set; }
        [ApiMember(Name = "FirstName", ParameterType = "body", Description = "the first name of the consultant", DataType = "string", IsRequired = true)]
        public string FirstName { get; set; }

        [ApiMember(Name = "LastName", ParameterType = "body", Description = "the last name of the consultant", DataType = "string", IsRequired = true)]
        public string LastName { get; set; }

        [ApiMember(Name = "DirectSellerId", ParameterType = "body", Description = "the DirectSellerId of the consultant", DataType = "string", IsRequired = true)]
        public string DirectSellerId { get; set; }

        [ApiMember(Name = "ConsultantPhone", ParameterType = "body", Description = "the consultant phone of the consultant", DataType = "string", IsRequired = true)]
        public string ConsultantPhone { get; set; }

        [ApiMember(Name = "Province", ParameterType = "body", Description = "the Province of the consultant", DataType = "string", IsRequired = true)]
        public string Province { get; set; }

        [ApiMember(Name = "City", ParameterType = "body", Description = "the city of the consultant", DataType = "string", IsRequired = true)]
        public string City { get; set; }

        [ApiMember(Name = "County", ParameterType = "body", Description = "the county f the consultant", DataType = "string", IsRequired = true)]
        public string County { get; set; }

        [ApiMember(Name = "CountyList", ParameterType = "body", Description = "the County List ", DataType = "list", IsRequired = true)]
        public List<County> CountyList { get; set; }

    }
    public class County
    {
        [ApiMember(Name = "CountyCode", ParameterType = "body", Description = "the county code  ", DataType = "string", IsRequired = true)]
        public int CountyCode { get; set; }
        [ApiMember(Name = "CountyName", ParameterType = "body", Description = "the county name  ", DataType = "string", IsRequired = true)]
        public string CountyName { get; set; }
    }

}
