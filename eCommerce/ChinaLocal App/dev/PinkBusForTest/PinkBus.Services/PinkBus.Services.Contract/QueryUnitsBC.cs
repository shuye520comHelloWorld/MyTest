using PinkBus.Services.Common;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.Services.Contract
{
    [Api((@"Query BC list of currrent BC's unit while BC choose BestowalTicket to another"))]
    [Route("/consultant/units/query", "GET, OPTIONS")]
    public class QueryUnitsBC : QueryRequestDto, IReturn<QueryUnitsBCResponse>
    {

    }

    public class QueryUnitsBCResponse : QueryResponseDto
    {
        [ApiMember(Name = "UnitName", ParameterType = "body", Description = "the unit name of unit", DataType = "long", IsRequired = true)]
        public string UnitName { get; set; }

        [ApiMember(Name = "UnitsBCInfo", ParameterType = "body", Description = "bc list in the same unit", DataType = "list", IsRequired = true)]
        public List<UnitBCInfo> UnitsBCInfo { get; set; }   
     
    }

    public class UnitBCInfo
    {
        [ApiMember(Name = "ContactId", ParameterType = "body", Description = "the contact id of the consultant", DataType = "long", IsRequired = true)]
        public long ContactId { get; set; }

        [ApiMember(Name = "DirectSellerId", ParameterType = "body", Description = "the DirectSellerId of the consultant", DataType = "string", IsRequired = true)]
        public string DirectSellerId { get; set; }

        [ApiMember(Name = "FirstName", ParameterType = "body", Description = "the first name of the consultant", DataType = "string", IsRequired = true)]
        public string FirstName { get; set; }

        [ApiMember(Name = "LastName", ParameterType = "body", Description = "the last name of the consultant", DataType = "string", IsRequired = true)]
        public string LastName { get; set; }

        [ApiMember(Name = "PhoneNumber", ParameterType = "body", Description = "the phone number of the unit consultant", DataType = "string", IsRequired = true)]
        public string PhoneNumber { get; set; }
    }


}

