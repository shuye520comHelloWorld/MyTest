using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Services.ORM.Operations
{

    public class CustomerOperation : BaseEntityOperation<CustomerEntity>
    {
        public CustomerOperation() : this(ConfigurationManager.AppSettings["Community"]) { }
        public CustomerOperation(string dbStr) : base(dbStr) { }
    }

    [Alias("iParty_Customer")]
    public class CustomerEntity
    {
        [CustomField("uniqueidentifier ")]
        [Description("the primary key reference of a customer.")]
        [Required]
        public Guid CustomerKey { get; set; }
        [CustomField("NVARCHAR(50) ")]
        [Description("the full name to a customer.")]
        [Required]
        public String Name { get; set; }
        [CustomField("VARCHAR(25) ")]
        [Description("the mobile phone number to a customer.")]
        [Required]
        public String PhoneNumber { get; set; }
        [CustomField("INT ")]
        [Description("the career type to a customer.")]
        public int? Career { get; set; }
        [CustomField("INT ")]
        [Description("the age range to a customer.")]
        public int? AgeRange { get; set; }
        [CustomField("BIT ")]
        public bool IsVIP { get; set; }
        [CustomField("VARHAR(50)")]
        [Description("who create the customer.")]
        public string CreatedBy { get; set; }
        [CustomField("NVARCHAR(50)")]
        [Description("who is forward this invitation to a customer")]
        /// <summary>
        ///  who is forward this invitation to this customer 
        /// </summary>
        public string Reference { get; set; }
        [CustomField("NVARCHAR(50)")]
        [Description("the key of who is forward this invitation to a customer")]
        /// <summary>
        /// if the reference is Consultant, log her contact Id
        /// else log its CustomerKey
        /// </summary>
        public string ReferenceKey { get; set; }
        [CustomField("INT")]
        [Description("the type of who is forward this invitation.")]
        /// <summary>
        /// 1 consultant is the reference 
        /// 2 customer is the reference
        /// </summary>
        public int ReferenceType { get; set; }
        [CustomField("BIGINT ")]
        [Description("the contactid of the applicant.")]
        [Required]
        public long Inviter { get; set; }
        [CustomField("NVARCHAR(50) ")]
        [Description("the name of the applicant.")]
        public string InviterName { get; set; }
        [CustomField("NVARCHAR(50) ")]
        [Description("the union id of this  customer.")]
        /// <summary>
        ///  the union id of this  customer
        /// </summary>
        public string UnionId { get; set; }
        [CustomField("INT ")]
        [Description("the marital status to a customer.")]
        /// <summary>
        /// 0 single 1 married
        /// </summary>
        public int? MaritalStatus { get; set; }
        [CustomField("DATETIME")]
        [Description("the create date of the customer")]
        public DateTime dtCreated { get; set; }
        [CustomField("VARCHAR(5)")]
        [Description("the level code of the customer")]
        public string Level { get; set; }
        [CustomField("BIGINT")]
        [Description("the contact id of the customer")]
        public long? CustomerContactId { get; set; }
        [CustomField("BIT")]
        [Description("if the customer has orders or not")]
        public bool HasOrder { get; set; }
        [CustomField("BIT")]
        [Description("if the customer is newer or not")]
        public bool IsNewer { get; set; }
        [CustomField("BIT")]
        [Description("if the customer is promoted or not")]
        public bool IsPromoted { get; set; }
    }
}
