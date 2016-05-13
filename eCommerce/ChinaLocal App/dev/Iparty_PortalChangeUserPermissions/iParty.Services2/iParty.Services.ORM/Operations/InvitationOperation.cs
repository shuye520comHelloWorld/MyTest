using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Services.ORM.Operations
{
    public class InvitationOperation : BaseEntityOperation<InvitationDataEntity>
    {
        public InvitationOperation() : this(ConfigurationManager.AppSettings["Community"]) { }
        public InvitationOperation(string dbStr) : base(dbStr) { }
    }

    [Alias("iParty_Invitation")]
    public class InvitationDataEntity
    {
        [CustomField("uniqueidentifier ")]
        [Description("the primary key of an invitation.")]
        public Guid InvitationKey { get; set; }
        [CustomField("uniqueidentifier ")]
        [Description("the foreign key reference to party.")]
        public Guid PartyKey { get; set; }
        [CustomField("uniqueidentifier ")]
        [Description("the foreign key reference to a customer.")]
        public Guid CustomerKey { get; set; }
        [CustomField("VARCHAR(25) ")]
        [Description("the mobile phone number to a customer.")]
        [Required]
        public String PhoneNumber { get; set; }
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
        [CustomField("CHAR(2)")]
        [Description("the status of this invitation.")]
        /// <summary>
        /// 00 the accepted state; 01 the checked in state; 03 the invitation is cancelled
        /// </summary>
        public string Status { get; set; }
        [CustomField("BIGINT")]
        [Description("who is in charge of this invitation.")]
        public long OwnerContactID { get; set; }
        [CustomField("VARCHAR(50)")]
        [Description("the unit ID of who is in charge of this invitation.")]
        public string OwnerUnitID { get; set; }
        [CustomField("CHAR(2)")]
        [Description("the check in type of this invitation.")]
        /// <summary>
        /// 00-WechartWithCard, 01-Browser, 02-Album, 03-Letter, 04-Intouch, 05-WechartNoCard
        /// </summary>
        public string CheckInType { get; set; }
        [CustomField("DATETIME")]
        [Description("the check in date time of this invitation.")]
        public DateTime? CheckinDate { get; set; }
    }
}
