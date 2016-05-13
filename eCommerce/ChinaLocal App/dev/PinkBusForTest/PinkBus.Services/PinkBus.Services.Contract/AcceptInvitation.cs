
using PinkBus.Services.Common;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.Services.Contract
{
    [Api(@"Customer accept the invitation after typed her information both in Intouch/H5  ")]
    [Route("/customers/invitation/accept", "PUT, OPTIONS")]
    [Route("/customers/invitation/accept/put", "POST, OPTIONS")]
    public class AcceptInvitation : CommandRequestDto, IReturn<AcceptInvitationResponse>
    {
        [ApiMember(Name = "EventKey", ParameterType = "body", DataType = "guid", Description = "the unique identifier of the Event ", IsRequired = true)]
        public Guid EventKey { get; set; }

        [ApiMember(Name = "TicketKey", ParameterType = "body", Description = "the TicketKey", DataType = "Guid", IsRequired = true)]
        public Guid TicketKey { get; set; }

        [ApiMember(Name = "CustomerName", ParameterType = "body", Description = "the full name of the customer", DataType = "string", IsRequired = true)]
        public string CustomerName { get; set; }

        [ApiMember(Name = "CustomerPhone", ParameterType = "body", Description = "the mobile phone number of the customer", DataType = "string", IsRequired = true)]
        public string CustomerPhone { get; set; }

        [ApiMember(Name = "CustomerType", ParameterType = "body", Description = "type of the customer, variable value are {Old/New/VIP}", DataType = "enum", IsRequired = true)]
        public CustomerType CustomerType { get; set; }

        [ApiMember(Name = "AgeRange", ParameterType = "body", Description = "the age range of the customer,variable value are {Blow25/Between25And35/Between35And45/Above45}", DataType = "enum", IsRequired = true)]
        public AgeRange AgeRange { get; set; }

        [ApiMember(Name = "Career", ParameterType = "body", Description = "the career of the customer,variable value are {Clerk/PrivateOwner/Housewife/Freelancers}", DataType = "enum", IsRequired = false)]
        public Career? Career { get; set; }

        [ApiMember(Name = "InterestingTopic", ParameterType = "body", Description = "the array of interesting topic of the customer {SkinCare/MakeUp/DressUp/FamilyTies}", DataType = "string", IsRequired = false)]
        public string InterestingTopic { get; set; }

        [ApiMember(Name = "BeautyClass", ParameterType = "body", Description = "indicate the customer whether a beauty class", DataType = "bool", IsRequired = false)]
        public bool? BeautyClass { get; set; }

        [ApiMember(Name = "UsedProduct", ParameterType = "body", Description = "indicate the customer whether used products", DataType = "bool", IsRequired = false)]
        public bool? UsedProduct { get; set; }

        [ApiMember(Name = "UsedSet", ParameterType = "body", Description = "the customer used series,variable value are {TimeWise/WhiteningSystemFoaming/Cleanser/CalmingInfluence/Other}", DataType = "enum", IsRequired = false)]
        public string UsedSet { get; set; }

        [ApiMember(Name = "InterestInCompany", ParameterType = "body", Description = "the array of  company interesting  of the customer,varaible value are {BeautyConfidence/CompanyCulture/BusinessOpportunity/Other}", DataType = "string", IsRequired = false)]
        public string InterestInCompany { get; set; }

        [ApiMember(Name = "UnionID", ParameterType = "body", Description = "the union id of a customer", DataType = "string", IsRequired = false)]
        public new string UnionID { get; set; }

        [ApiMember(Name = "HeadImgUrl", ParameterType = "body", DataType = "string", Description = "wechart user head image url ", IsRequired = false)]
        public string HeadImgUrl { get; set; }

        [ApiMember(Name = "Source", ParameterType = "body", Description = "the input from which site,varaible value are  {Wechat/MobileBrowser/IntouchClient}", DataType = "enum", IsRequired = true)]
        public Source Source { get; set; }

    }

    public class AcceptInvitationResponse : CommandResponseDto
    {
        [ApiMember(Name = "EventBaseInfo", ParameterType = "body", DataType = "object", Description = "including current event concrete information", IsRequired = true)]
        public EventBaseInfo EventBaseInfo { get; set; }

        [ApiMember(Name = "TicketKey", ParameterType = "body", DataType = "guid", Description = "the unique identifier of Ticket", IsRequired = true)]
        public Guid TicketKey { get; set; }

        [ApiMember(Name = "TicketType", ParameterType = "body", DataType = "enum", Description = "ticket type,variable are normal,honner", IsRequired = true)]
        public TicketType TicketType { get; set; }

        [ApiMember(Name = "CustomerKey", ParameterType = "body", Description = "the unique identifier of customer  ", DataType = "string", IsRequired = true)]
        public Guid CustomerKey { get; set; }

        [ApiMember(Name = "CustomerName", ParameterType = "body", Description = " customer's name  ", DataType = "string ", IsRequired = true)]
        public string CustomerName { get; set; }

        [ApiMember(Name = "CustomerPhone", ParameterType = "body", Description = " customer's phone number  ", DataType = "string ", IsRequired = true)]
        public string CustomerPhone { get; set; }

        [ApiMember(Name = "ContactId", ParameterType = "body", Description = "contactid if customer is a bc", DataType = "long", IsRequired = false)]
        public long ContactId { get; set; }

        [ApiMember(Name = "Level", ParameterType = "body", Description = "consultant level if customer is a bc  ", DataType = "string", IsRequired = false)]
        public string Level { get; set; }

        [ApiMember(Name = "Result", ParameterType = "body", DataType = "bool", Description = "indicate the result of this operation", IsRequired = true)]
        public bool Result { get; set; }

        [ApiMember(Name = "ErrorMessage", ParameterType = "body", DataType = "string", Description = "ErrorMessage", IsRequired = false)]
        public string ErrorMessage { get; set; }


    }
}
