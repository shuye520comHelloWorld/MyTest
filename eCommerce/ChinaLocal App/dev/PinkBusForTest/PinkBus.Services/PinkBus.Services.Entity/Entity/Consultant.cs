using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.Services.Entity
{

    /// <summary>
    /// Beauty consutant who attend the event
    /// </summary>
    [Alias("PB_Event-Consultant")]
    public partial class Consultant
    {
        /// <summary>
        /// The unique  identifier of consultant who attend the event
        /// </summary>
        [PrimaryKey]
        [Alias("MappingKey")]
        public Guid ConsultantKey { get; set; }
        /// <summary>
        /// The unique  indentifer of event
        /// </summary>
        public Guid EventKey { get; set; }
        /// <summary>
        /// The type of consultant who attend the event,arivable value  NormalBC,BestowalBC, VIPBC,VolunteerBC
        /// </summary>
        [Alias("UserType")]
        public int EventUserType { get; set; }
        /// <summary>
        /// Consultant has the normal ticket quantity in actual
        /// </summary>
        public int NormalTicketQuantity { get; set; }
        /// <summary>
        /// Consultant has the vip ticket quantity in actual
        /// </summary>
        public int VIPTicketQuantity { get; set; }

        /// <summary>
        /// Consultant has the normal ticket quantity  in setting
        /// </summary>
        public int NormalTicketSettingQuantity { get; set; }
        /// <summary>
        /// Consultant has the vip ticket quantity by setting
        /// </summary>
        public int VIPTicketSettingQuantity { get; set; }
        /// <summary>
        /// The unique identifier of beauty consultant in our business system
        /// </summary>
        public long? ContactId { get; set; }       
        /// <summary>
        /// The beauty consultant numer in our business sytem with length of 12 
        /// </summary>
        public string DirectSellerId { get; set; }
        /// <summary>
        /// The first name of beauty consultant in our business system
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// The last name of beauty consultant in our business system
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// The phone number of beauty consultant in our business system
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// The level of beauty consultant in our business system
        /// </summary>
        public string Level { get; set; }
        /// <summary>
        /// The ResidenceID of consultant in our business system
        /// </summary>
        public string ResidenceID { get; set; }

        /// <summary>
        /// The Province of consultant in our business system
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// The City consultant in our business system
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// The countyId of beauty consultant in our business system
        /// </summary>
        public string OECountyId { get; set; }
        /// <summary>
        /// The county name  of beauty consultant in our business system
        /// </summary>
        public string CountyName { get; set; }
        /// <summary>
        /// Indicate volunteer whether confirm her county information
        /// </summary>
        public bool IsConfirmed { get; set; }
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

        public int Status { get; set; }
      
    }

    //[Alias("PinkBus_BCInfo")]
    //public class BCInfo
    //{
    //    public long ContactId { get; set; }
    //    public string DirectSellerId { get; set; }
    //    public string Name { get; set; }
    //    public string PhoneNumber { get; set; }
    //    public string Level { get; set; }
    //    public string IDCardNo { get; set; }

    //    [Description("the county id in OE database county table ")]
    //    public int OECountyId { get; set; }
    //    public string CountyName { get; set; }

    //    public DateTime CreatedTime { get; set; }
    //    public string CreatedBy { get; set; }
    //    public DateTime UpdatedTime { get; set; }
    //    public string UpdatedBy { get; set; }
    //}
}
