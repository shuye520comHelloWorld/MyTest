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
    [Alias("PB_Customer")]
    public partial class Customer
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
        /// Phone number of cusotmer
        /// </summary>
        public string CustomerPhone { get; set; }
        /// <summary>
        /// Recore customer type
        /// </summary>
        [Description("refer the enum type the CustomerType, variable value are Old,New,VIP")]
        public int CustomerType { get; set; }
        /// <summary>
        ///Age range of customer
        /// </summary>
        [Description("refer the enum type the AgeRange, variable value are  Blow25, Between25And35, Between35And45, Above45")]
        public int AgeRange { get; set; }
        /// <summary>
        ///Career of Cusotmer,<see cref="Career"/>
        /// </summary>
        [Description("refer the enum type the Career, variable value are   Clerk, PrivateOwner,Housewife, Freelancers")]
        public int? Career { get; set; }
        /// <summary>
        ///Inertesting topic of customer <see cref="InterestingTopic"/>
        /// </summary>  
        [Description("refer the enum type the InterestingTopic, variable value are SkinCare,MakeUp,DressUp,FamilyTies")]
        public string InterestingTopic { get; set; }
        /// <summary>
        /// indicate customer whether attended beauty class 
        /// </summary>
        public bool? BeautyClass { get; set; }
        /// <summary>
        /// Record customer whether used product
        /// </summary>
        public bool? UsedProduct { get; set; }
        /// <summary>
        /// Record which set customer used 
        /// </summary>
        [Description("refer the enum type the UsedSet, variable value are TimeWise,WhiteningSystemFoaming,Cleanser,CalmingInfluence,Other")]
        public string  UsedSet { get; set; }

        /// <summary>
        /// Record customer is inertested for company
        /// </summary>
        [Description("refer the enum type the InterestInCompany, variable value are BeautyConfidence,CompanyCulture,BusinessOpportunity,Other")]
        public string InterestInCompany { get; set; }
        /// <summary>
        /// If customer is BC in our business system,store her level while accept the invitation 
        /// </summary>
        public string AcceptLevel { get; set; }
        /// <summary>
        /// If customer is BC in our business system, store her contactid
        /// </summary>
        public long CustomerContactId { get; set; }
        /// <summary>
        /// If customer open the h5 via wechat, store customer's wechat unionId
        /// </summary>
        public string UnionID { get; set; }
        /// <summary>
        /// If customer open the h5 via wechat, store customer's wechat head image url
        /// </summary>
        public string HeadImgUrl { get; set; }
        /// <summary>
        /// Record customer open h5 by which path,varible value are wechat/broswer 
        /// </summary>
        public int Source { get; set; }
        /// <summary>
        /// The flag indicate whether imported to my customer
        /// </summary>
        public bool IsImportMyCustomer { get; set; }
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
