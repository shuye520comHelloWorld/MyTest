using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Services.ORM.Operations
{
    public class PartyOperation : BaseEntityOperation<PartyEntity>
    {
        public PartyOperation() : this(ConfigurationManager.AppSettings["Community"]) { }
        public PartyOperation(string dbStr) : base(dbStr) { }
    }

    [Alias("iParty_PartyApplication")]
    public class PartyEntity
    {
        [CustomField("uniqueidentifier ")]
        [Description("this is the primary key.")]
        [Required]
        public Guid PartyKey { get; set; }
        [CustomField("uniqueidentifier ")]
        [Description("the foreign key reference to event.")]
        [Required]
        public Guid EventKey { get; set; }
        [CustomField("INT")]
        [Description("the foreign key reference to workshop.")]
        [Required]
        public int WorkshopId { get; set; }
        [CustomField("INT")]
        [Description("the type of the specific party.")]
        /// <summary>
        /// 活动类型
        /// 0-party，1-礼遇日
        /// </summary>
        public int PartyType { get; set; } 
       
        [CustomField("BITINT")]
        [Description("the contactid of the applicant.")]
        public long AppliedContactID { get; set; }

        [CustomField("NVARCHAR(50)")]
        [Description("the full name of the applicant.")]
        public string AppliedName { get; set; }

        [CustomField("INT")]
        [Description("the organizatioin way of the applicantion.")]
        /// <summary>
        /// 承办方式
        /// 0-独立，1-联合
        /// </summary>
        public int OrganizationType { get; set; }
        [CustomField("DATETIME")]
        [Description("the start date of the party.")]
        public DateTime StartDate { get; set; }
        [CustomField("DATETIME")]
        [Description("the end date of the party.")]
        public DateTime EndDate { get; set; }
        [CustomField("DATETIME")]
        [Description("the create date of the party.")]
        public DateTime CreateDate { get; set; }
        [CustomField("DATETIME")]
        [Description("the display start date of the party.")]
        public DateTime DisplayStartDate { get; set; }
        [CustomField("DATETIME")]
        [Description("the display end date of the party.")]
        public DateTime DisplayEndDate { get; set; }
    }

    public class PartyInfoOperation : BaseEntityOperation<PartyInfo>
    {
        public PartyInfoOperation() : this(ConfigurationManager.AppSettings["Community"]) { }
        public PartyInfoOperation(string dbStr) : base(dbStr) { }
    }

    public class PartyInfo : PartyEntity
    {
        public DateTime? FeedbackEndDate { get; set; }
    }
}
