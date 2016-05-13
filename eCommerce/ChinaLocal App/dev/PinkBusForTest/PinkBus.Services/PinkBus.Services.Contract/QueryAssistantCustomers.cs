using PinkBus.Services.Common;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.Services.Contract
{
    [Api(@"Query customers for intouch my assistant")]
    [Route("/customers/query", "GET, OPTIONS")]
    public class QueryAssistantCustomers : QueryRequestDto, IReturn<QueryAssistantCustomerResponse>
    {
        [ApiMember(Name = "TimeStamp", ParameterType = "body", DataType = "datetime", Description = "the time stamp for checking data version ", IsRequired = false)]
        public DateTime? TimeStamp { get; set; }

        [ApiMember(Name = "DirectSellerId", ParameterType = "body", DataType = "string", Description = "the DirectSellerId of consultant ", IsRequired = true)]
        public string DirectSellerId { get; set; }
    }

    public class QueryAssistantCustomerResponse : QueryResponseDto
    {
        [ApiMember(Name = "Customers", ParameterType = "body", DataType = "array", Description = "the customers list ", IsRequired = true)]
        public List<AssistantCustomer> Customers { get; set; }
    }

    public class AssistantCustomer
    {
        [ApiMember(Name = "CustomerKey", ParameterType = "body", DataType = "string", Description = "the customer key ", IsRequired = true)]
        public Guid CustomerKey
        {
            get;
            set;
        }

        [ApiMember(Name = "CustomerName", ParameterType = "body", DataType = "string", Description = "the customer name ", IsRequired = true)]
        public string CustomerName
        {
            get;
            set;
        }

        [ApiMember(Name = "ContactType", ParameterType = "body", DataType = "enum", Description = "the customer ContactType,variable are PhoneNumber/QQ/Wechat/Other", IsRequired = false)]
        public string ContactType
        {
            get;
            set;
        }

        [ApiMember(Name = "ContactInfo", ParameterType = "body", DataType = "string", Description = "the customer ContactInfo ", IsRequired = false)]
        public string ContactInfo
        {
            get;
            set;
        }

        [ApiMember(Name = "Age", ParameterType = "body", DataType = "string", Description = "the customer age ", IsRequired = false)]
        public string Age
        {
            get;
            set;
        }

        [ApiMember(Name = "Career", ParameterType = "body", DataType = "string", Description = "the customer career ", IsRequired = false)]
        public string Career
        {
            get;
            set;
        }
    }
}
