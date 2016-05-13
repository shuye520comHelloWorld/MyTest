using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.Services.Entity
{
    /// <summary>
    /// Customer,BC invite the customer to attend the event
    /// </summary>
    [Alias("PB_OfflineCustomer")]
    public partial class OfflineCustomer
    {
        /// <summary>
        /// The unique  identifier of customer
        /// </summary>
        [PrimaryKey]
        public System.Guid CustomerKey { get; set; }
        /// <summary>
        /// Name of cusotmer
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// contract type
        /// </summary>
        [Description("refer the enum type the ContactType, variable value are PhoneNumber/QQ/Wechat/Other")]
        public string ContactType { get; set; }
        /// <summary>
        /// ContactInfo
        /// </summary>
        public string ContactInfo { get; set; }

        [Description("refer the enum type the DirectSellerId, variable value are Old:0,New:1,Student:2")]
        public string DirectSellerId { get; set; }

        [Description("refer the type the EventKey, variable value are Old,New,Student")]
        public string EventKey { get; set; }
        /// <summary>
        ///Age range of customer
        /// </summary>
        [Description("refer the enum type the AgeRange, variable value are Blow25, Between2535, Between3545, Above45")]
        public string AgeRange { get; set; }
        /// <summary>
        ///Career of Cusotmer,<see cref="Career"/>
        /// </summary>
        [Description("refer the enum type the Career, variable value are  Clerk, PrivateOwner,Housewife, Freelancers")]
        public bool? IsHearMaryKay { get; set; }
        /// <summary>
        ///Inertesting topic of customer <see cref="InterestingTopic"/>
        /// </summary>  
        [Description("refer the enum type the InterestingTopic, variable value are SkinCare,MakeUp,DressUp,FamilyTies")]
        public string InterestingTopic { get; set; }
        /// <summary>
        /// Recore customer type
        /// </summary>
        [Description("refer the enum type the CustomerType, variable value are Old,New,VIP")]
        public int? CustomerType { get; set; }
        /// <summary>
        /// indicate customer whether attended beauty class 
        /// </summary>
        public string Career { get; set; }

        public bool? IsJoinEvent { get; set; }

        public string CustomerResponse { get; set; }
        /// <summary>
        /// Record customer whether used product
        /// </summary>
        public bool? UsedProduct { get; set; }
        /// <summary>
        /// Record which set customer used 
        /// </summary>
        [Description("refer the enum type the BestContactDate, variable value are 0:晚上,1:白天")]
        public int? BestContactDate { get; set; }

        /// <summary>
        /// Record customer is inertested for company
        /// </summary>
        [Description("refer the enum type the AdviceContactDate, variable value are 0:工作日,1:双休日")]
        public int? AdviceContactDate { get; set; }

        /// <summary>
        /// indicate customer whether attended beauty class 
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// indicate customer whether attended beauty class 
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// indicate customer whether attended beauty class 
        /// </summary>
        public string County { get; set; }
        /// <summary>
        /// The flag indicate whether imported to my customer
        /// </summary>
        public bool? IsImport { get; set; }
        /// <summary>
        /// The date when created the event
        /// </summary>
        public System.DateTime CreatedDate { get; set; }
        /// <summary>
        /// The person who created the event
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// The date when updated the event 
        /// </summary>
        public System.DateTime? UpdatedDate { get; set; }
        /// <summary>
        /// The person who updated the event 
        /// </summary>
        public string UpdatedBy { get; set; }

    }

    //[Alias("PinkBus_Customer")]
    //public class Customer
    //{
    //    public Guid CustomerKey { get; set; }
    //    public string CustomerName { get; set; }
    //    public string CustomerPhone { get; set; }
    //    public CustomerType CustomerType { get; set; }
    //    public AgeRange AgeRange { get; set; }
    //    public Career Career { get; set; }
    //    public InterestingTopic InterestingTopic { get; set; }
    //    public bool BeautyClass { get; set; }
    //    public bool UsedProduct { get; set; }
    //    public UsedSet UsedSet { get; set; }
    //    public InterestInCompany InterestInCompany { get; set; }
    //    public string AcceptLevel { get; set; }
    //    public long CustomerContactId { get; set; }
    //    public string UnionID { get; set; }
    //    public string HeadImgUrl { get; set; }
    //    public string Source { get; set; }
    //    [Description("ECustomer")]
    //    public bool IsImportMyCustomer { get; set; }

    //    public string CreatedBy { get; set; }
    //    public DateTime CreatedTime { get; set; }
    //    public string UpdatedBy { get; set; }
    //    public DateTime UpdatedTime { get; set; }
    //}

}
