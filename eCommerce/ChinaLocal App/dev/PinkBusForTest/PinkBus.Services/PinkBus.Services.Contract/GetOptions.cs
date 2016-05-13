using PinkBus.Services.Common;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.Services.Contract
{
    [Api(@"Get options which show on customer page H5/Intouch ")]
    [Route("/customer/options", "GET, OPTIONS")]
    public class GetOptions : BaseRequestDto, IReturn<GetOptionsResponse>
    {

    }


    public class GetOptionsResponse : BaseResponseDto
    {
        [ApiMember(Name = "AgeRangOption", ParameterType = "body", Description = "customer's age range option ", DataType = "array", IsRequired = true)]
        public List<AgeRangeOption> AgeRangeOption { get; set; }
        [ApiMember(Name = "InterestInCompanyOption", ParameterType = "body", Description = "customer's interestincompany option", DataType = "array", IsRequired = true)]
        public List<InterestInCompanyOption> InterestInCompanyOption { get; set; }

        [ApiMember(Name = "Career", ParameterType = "body", Description = "customer's career option", DataType = "array", IsRequired = true)]       
        public List<CareerOption> CareerOption { get; set; }

        [ApiMember(Name = "UsedSetOption", ParameterType = "body", Description = "customer's usedset option", DataType = "array", IsRequired = true)]
        public List<UsedSetOption> UsedSetOption { get; set; }
    }

    public class AgeRangeOption : Option
    {

    }
    public class InterestInCompanyOption : Option
    {

    }
    public class CareerOption : Option
    {

    }
    public class UsedSetOption : Option
    {

    }
    public class Option
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }

}
