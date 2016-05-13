
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PinkBus.CheckInClient.Entitys
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
        public Guid ConsultantKey { get { return MappingKey; } set { MappingKey = value; } }
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

        public int Status { get; set; }

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

        public int ChangeStatus { get; set; }
        public int SyncTimes { get; set; }
    }

    public class Customer
    {
        public System.Guid CustomerKey { get; set; }
        public string CustomerName { get; set; }
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
        public string UsedSet { get; set; }

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
        public long? CustomerContactId { get; set; }
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

        public DateTime DownloadDate { get; set; }

        public int ChangeStatus { get; set; }
        public int SyncTimes { get; set; }

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
        public DateTime DownloadDate { get; set; }

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
        public DateTime DownloadDate { get; set; }


    }

    public class Ticket
    {
        /// <summary>
        /// The unique  identifier of event apply ticket  
        /// </summary>
        //[PrimaryKey]
        public Guid TicketKey { get; set; }
        /// <summary>
        /// The unique  identifier of consultant
        /// </summary>
        //[Alias("MappingKey")]
        public Guid MappingKey { get; set; }
        /// <summary>
        /// The unique  identifier of event
        /// </summary>
        public Guid EventKey { get; set; }
        /// <summary>
        /// The unique identifier of Session
        /// </summary>
        public Guid SessionKey { get; set; }
        /// <summary>
        /// The unique  identifier of customer
        /// </summary>
        public Guid? CustomerKey { get; set; }
        /// <summary>
        /// Ticket type, variable value are Vip/Normal
        /// </summary>
        //[Description("refer the enum type the TicketType, variable value are VIP,Normal")]
        public int TicketType { get; set; }
        /// <summary>
        /// Record the ticket pathway , variable value are Import,Apply,Bestowal,Rebuild
        /// </summary> 
        //[Description("refer the enum type the TicketType, variable value are Import,Apply,Bestowal,Rebuild")]
        public int TicketFrom { get; set; }
        /// <summary>
        /// Ticket stauts,variable value are Created,Inviting,Invited, Bestowed,Canceled,Checkin, UnCheckin,Expired 
        /// </summary>
        //[Description("refer the enum type the TicketType, variable are Created,Inviting,Invited,Bestowed,Canceled, Checkin,UnCheckin,Expired")]
        public int TicketStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>        
        public string SMSToken { get; set; }
        /// <summary>
        /// The unique identifier of Session
        /// </summary>
        public DateTime SessionStartDate { get; set; }
        /// <summary>
        /// The unique identifier of Session
        /// </summary>
        public DateTime SessionEndDate { get; set; }

        public DateTime? CheckinDate { get; set; }
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

        public int ChangeStatus { get; set; }
        public int SyncTimes { get; set; }
    }

    public class Provinces
    {
        public int Id { get; set; }
        public string ProvinceId { get; set; }

        public string Province { get; set; }
    }

    public class Cities
    {
        public int Id { get; set; }
        public string CityId { get; set; }

        public string City { get; set; }
        public string Father { get; set; }
    }

    public class Counties
    {
        public int Id { get; set; }
        public string CountyId { get; set; }

        public string County { get; set; }
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

    public class VolunteerCheckin
    {
        public Guid Key { get;set;}
        public Guid EventKey { get; set; }
        public Guid MappingKey { get; set; }
        public DateTime CheckinDate { get; set; }

        public DateTime CheckinDay { get; set; }
        public DateTime CreatedDate { get; set; }
        public string  CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        public int ChangeStatus { get; set; }
        public int SyncTimes { get; set; }
    }
}
