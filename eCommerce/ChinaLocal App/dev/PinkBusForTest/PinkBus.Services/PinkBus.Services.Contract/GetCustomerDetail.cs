
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PinkBus.Services.Common;
namespace PinkBus.Services.Contract
{

    [Api(" new customer accepts the invitation.")]
    [Route("/customers/{CustomerKey}", "GET, OPTIONS")]
    public class GetCustomerDetail : BaseRequestDto, IReturn<GetCustomerDetailResponse>
    {
        [ApiMember(Name = "CustomerKey", ParameterType = "path", Description = "the unique identifier of customer", DataType = "Guid", IsRequired = true)]
        public Guid CustomerKey { get; set; }
    }

    public class GetCustomerDetailResponse : BaseResponseDto
    {
        [ApiMember(Name = "Customer", ParameterType = "body", Description = " cusomter information", DataType = "customer", IsRequired = true)]
        public Customer Customer { get; set; }
    }

    public class Customer
    {
        [ApiMember(Name = "Customerkey", ParameterType = "body", Description = " the unique identidier of customer ", DataType = "guid", IsRequired = true)]
        public Guid CustomerKey { get; set; }

        [ApiMember(Name = "CustomerName", ParameterType = "body", Description = " customer Name  ", DataType = "string ", IsRequired = true)]
        public string CustomerName { get; set; }

        [ApiMember(Name = "CustomerPhone", ParameterType = "body", Description = " customer phone number  ", DataType = "string ", IsRequired = true)]
        public string CustomerPhone { get; set; }

        [ApiMember(Name = "CustomerType", ParameterType = "body", Description = "the customer type, the variable are {Old/New/VIP}", DataType = "enum", IsRequired = true)]
        public CustomerType CustomerType { get; set; }

        [ApiMember(Name = "AgeRange", ParameterType = "body", Description = "the age range of the customer,varaible value are {Blow25/Between25And35/Between35And45/Above45}", DataType = "enum", IsRequired = true)]
        public AgeRange AgeRange { get; set; }

        [ApiMember(Name = "Career", ParameterType = "body", Description = "the career of the customer,varaible value are {Clerk/PrivateOwner/Housewife/Freelancers}", DataType = "enum", IsRequired = false)]
        public Career? Career { get; set; }

        [ApiMember(Name = "InterestingTopic", ParameterType = "body", Description = "the Interesting topic of the customer,variable value are {SkinCare/MakeUp/DressUp/FamilyTies}", DataType = "enum", IsRequired = false)]
        public string  InterestingTopic { get; set; }

        [ApiMember(Name = "BeautyClass", ParameterType = "body", Description = "indicate the customer whether a beauty class", DataType = "bool", IsRequired = false)]
        public bool? BeautyClass { get; set; }

        [ApiMember(Name = "UsedProduct", ParameterType = "body", Description = "indicate the customer whether used products", DataType = "bool", IsRequired = false)]
        public bool? UsedProduct { get; set; }

        [ApiMember(Name = "UsedSet", ParameterType = "body", Description = "the customer used series {TimeWise/WhiteningSystemFoaming/Cleanser/CalmingInfluence/Other}", DataType = "enum", IsRequired = false)]
        public string  UsedSet { get; set; }

        [ApiMember(Name = "InterestInCompany", ParameterType = "body", Description = "the company interesting  of the customer {BeautyConfidence/CompanyCulture/BusinessOpportunity/Other}", DataType = "enum", IsRequired = false)]
        public string InterestInCompany { get; set; }

        [ApiMember(Name = "IsImportMyCustomer", ParameterType = "body", DataType = "bool", Description = "indicate whether the customer imported to MyCustomer", IsRequired = false)]
        public bool? IsImportMyCustomer { get; set; }

        [ApiMember(Name = "CreatedDate", ParameterType = "body", Description = "customer create date", DataType = "datetim", IsRequired = false)]
        public DateTime CreatedDate { get; set; }

        [ApiMember(Name = "UpdatedDate", ParameterType = "body", Description = "customer update date", DataType = "datetim", IsRequired = false)]
        public DateTime UpdatedDate { get; set; }
    }

}
