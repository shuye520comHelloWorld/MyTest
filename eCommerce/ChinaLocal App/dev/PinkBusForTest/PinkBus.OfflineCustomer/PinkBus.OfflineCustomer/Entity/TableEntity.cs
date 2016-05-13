using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PinkBus.OfflineCustomer.Entity
{

    /// <summary>
    /// Beauty consutant who attend the event
    /// </summary>
    public partial class Consultant
    {
        /// <summary>
        /// The unique  identifier of consultant who attend the event
        /// </summary>
        //[Alias("MappingKey")]
        public Guid ConsultantKey
        {
            get { return MappingKey; }
            set { MappingKey = value; }
        }

        public Guid MappingKey { get; set; }
        /// <summary>
        /// The unique  indentifer of event
        /// </summary>
        public Guid EventKey { get; set; }
        /// <summary>
        /// The type of consultant who attend the event,arivable value  NormalBC,BestowalBC, VIPBC,VolunteerBC
        /// </summary>
        //[Alias("UserType")]
        public int EventUserType { get { return UserType; } set { UserType = value; } }
        public int UserType { get; set; }
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
        public long ContactId { get; set; }
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

        public bool Status { get; set; }

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

        public DateTime DownloadDate { get; set; }
    }

    public class VolunteerForExcel
    {
        public Guid EventKey { get; set; }
        public Guid MappingKey { get; set; }
        public string DirectSellerId { get; set; }
        public string ConsultantName { get; set; }
        public string ConsultantPhone { get; set; }
        public string ConsultantLevel { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public DateTime CheckinDate { get; set; }
        public DateTime CheckinDay { get; set; }
        public int CustomerCount { get; set; }
    }

    public class Volunteer
    {
        public Guid EventKey { get; set; }
        public Guid MappingKey { get; set; }
        public string DirectSellerId { get; set; }
        public string ConsultantName { get; set; }
        public string ConsultantPhone { get; set; }
        public string ConsultantLevel { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public DateTime CheckinDate { get; set; }

        public int CustomerQuantity { get; set; }
    }
    public class Customer
    {
        public System.Guid CustomerKey { get; set; }

        public string DirectSellerId { get; set; }

        /// <summary>
        /// the customer name 
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// the customer belong which event
        /// </summary>
        public string EventKey { get; set; }
        /// <summary>
        /// the contact type of customer
        /// </summary>
        public string ContactType { get; set; }

        /// <summary>
        /// the contact info of customer
        /// </summary>
        public string ContactInfo { get; set; }

        /// <summary>
        ///Age range of customer
        /// </summary>
        [Description("refer the enum type the AgeRange, variable value are  Blow25, Between2535, Between3545, Above45")]
        public string AgeRange { get; set; }

        /// <summary>
        ///Indicate whether customer heard marykay company
        /// </summary>
        public bool? IsHearMaryKay { get; set; }
        /// <summary>
        ///Inertesting topic of customer <see cref="InterestingTopic"/>
        /// </summary>  
        [Description("refer the enum type the InterestingTopic, variable value are SkinCare,MakeUp,DressUp,FamilyTies")]
        public string InterestingTopic { get; set; }
        /// <summary>
        /// Recore customer type
        /// </summary>
        [Description("refer the enum type the CustomerType, variable value are Old 0:,New 1,Student 2")]
        public string CustomerType { 
            get; set;
        
        }
        
        /// <summary>
        /// customer career type
        /// </summary>
        public string Career { get; set; }
        /// <summary>
        /// indicate whether customer will join company event
        /// </summary>
        public bool? IsJoinEvent { get; set; }
        /// <summary>
        /// customer response
        /// </summary>
        public int? CustomerResponse { get; set; }
        /// <summary>
        /// customer whether used product
        /// </summary>
        public bool? UsedProduct { get; set; }
        /// <summary>
        /// best contact datetime
        /// </summary>
        public string BestContactDate { get; set; }
        /// <summary>
        /// advice contact date
        /// </summary>
        public string AdviceContactDate { get; set; }
        /// <summary>
        ///  province name of customer
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// city name of customer
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// city name of customer
        /// </summary>
        public string County { get; set; }
        /// <summary>
        ///indicate the data whether upload to server
        /// </summary>
        public string State { get; set; }
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

    public class Event
    {
        public System.Guid EventKey { get; set; }
        /// <summary>
        /// The location of event happened
        /// </summary>
        public string EventLocation { get; set; }
        /// <summary>
        /// The title of event
        /// </summary>
        public string EventTitle { get; set; }

        /// <summary>
        /// The start date of event
        /// </summary>
        public System.DateTime EventStartDate { get; set; }
        /// <summary>
        /// The end date of event
        /// </summary>
        public System.DateTime EventEndDate { get; set; }
        /// <summary>
        /// The start date of check in
        /// </summary>
        public System.DateTime CheckinStartDate { get; set; }
        /// <summary>
        /// The end date of check in
        /// </summary>
        public System.DateTime CheckinEndDate { get; set; }
        /// <summary>
        /// The start date of apply ticket stage
        /// </summary>
        public System.DateTime ApplyTicketStartDate { get; set; }
        /// <summary>
        /// The end date of apply ticket stage
        /// </summary>
        public System.DateTime ApplyTicketEndDate { get; set; }
        /// <summary>
        /// The end date of invitation
        /// </summary>
        public System.DateTime InvitationEndDate { get; set; }
        /// <summary>
        /// The download token while downloaded data to client application
        /// </summary>
        public string DownloadToken { get; set; }
        /// <summary>
        /// The upload token while upload data to client application
        /// </summary>
        public string UploadToken { get; set; }
        /// <summary>
        /// Indicate whether client applicaiton upload data already.
        /// </summary>
        public bool IsUpload { get; set; }
        /// <summary>
        /// The flag indicate whether imported event-consultant BC already
        /// </summary>
        public bool BCImport { get; set; }
        /// <summary>
        /// The flag indicate whether imported event-consultant Volunteer already
        /// </summary>
        public bool VolunteerImport { get; set; }
        /// <summary>
        /// The flag indicate whether imported event-consultant VIP already
        /// </summary>
        public bool VipImport { get; set; }
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

    /// <summary>
    ///Event session infomation, an event including serveral session
    /// </summary>
    public class EventSession
    {
        /// <summary>
        /// The identifier of Session
        /// </summary>
        public System.Guid SessionKey { get; set; }
        /// <summary>
        /// The identifier of event, an event inlcuding several session 
        /// </summary>
        public System.Guid Eventkey { get; set; }
        /// <summary>
        /// Display Order of session
        /// </summary>
        public int DisplayOrder { get; set; }
        /// <summary>
        /// Start date of session
        /// </summary>
        public System.DateTime SessionStartDate { get; set; }
        /// <summary>
        /// End date of session
        /// </summary>
        public System.DateTime SessionEndDate { get; set; }
        /// <summary>
        /// Indicate the session whether can be applied
        /// </summary>
        public bool CanApply { get; set; }

        /// <summary>
        /// Indicate ticket is out of stock
        /// </summary>
        public bool TicketOut { get; set; }
        /// <summary>
        /// Total amount of vip ticket in current session
        /// </summary>
        public int VIPTicketQuantity { get; set; }
        /// <summary>
        /// Total amount of vip ticket in current session
        /// </summary>
        public int NormalTicketQuantity { get; set; }
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
        public System.DateTime UpdatedDate { get; set; }
        /// <summary>
        /// The person who updated the event 
        /// </summary>
        public string UpdatedBy { get; set; }

    }

    public class Province
    {
        public int Id { get; set; }

        public string ProvinceId { get; set; }

        public string ProvinceName { get; set; }
    }

    public class City
    {
        public int Id { get; set; }
        public string CityId { get; set; }

        public string CityName { get; set; }

        public string Father { get; set; }
    }

    public class County
    {
        public int Id { get; set; }
        public string CountyId { get; set; }

        public string CountyName { get; set; }

        public string Father { get; set; }
    }
    public class SyncStatusLog
    {
        public Guid EventKey { get; set; }
        public SyncType SyncType { get; set; }
        public bool Event { get; set; }
        public bool Session { get; set; }
        public bool Consultant { get; set; }
        public bool Ticket { get; set; }
        public bool Customer { get; set; }
        public bool HeaderImgs { get; set; }
        public bool Complete { get; set; }
    }
}
