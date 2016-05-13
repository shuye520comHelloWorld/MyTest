using PinkBus.Services.Common;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.Services.Internal.Contract
{
    [Api("return customer list while bc view her customer in wechat service h5 page")]
    [Route("/customers/query", "GET,Options")]
    public class QueryCustomer : QueryRequestDto, IReturn<QueryCustomerResponse>
    {
        [ApiMember(Name = "ContactId", ParameterType = "body", Description = "the contact id of consultant", DataType = "long", IsRequired = true)]
        public long ContactId { get; set; }
    }

    public class QueryCustomerResponse : QueryResponseDto
    {
        [ApiMember(Name = "Customer", ParameterType = "body", Description = "customer list ", DataType = "array", IsRequired = true)]
        public List<Customer> Customers { get; set; }
    }

    public class Customer
    {
        [ApiMember(Name = "CustomerName", ParameterType = "body", Description = "customer name ", DataType = "string", IsRequired = false)]
        public string CustomerName { get; set; }

        [ApiMember(Name = "PhoneNumber", ParameterType = "body", Description = "customer phone number ", DataType = "string", IsRequired = false)]
        public string PhoneNumber { get; set; }

        [ApiMember(Name = "HeadImgUrl", ParameterType = "body", Description = "customer wechat head image url ", DataType = "string", IsRequired = false)]
        public string HeadImgUrl { get; set; }

        [ApiMember(Name = "EventContent", ParameterType = "body", Description = "event content ", DataType = "string", IsRequired = true)]
        public string EventContent { get; set; }

        [ApiMember(Name = "ReceivedStartTime", ParameterType = "body", Description = "Received StartTime", DataType = "datetime", IsRequired = true)]
        public DateTime ReceivedStartTime { get; set; }

    }
}
