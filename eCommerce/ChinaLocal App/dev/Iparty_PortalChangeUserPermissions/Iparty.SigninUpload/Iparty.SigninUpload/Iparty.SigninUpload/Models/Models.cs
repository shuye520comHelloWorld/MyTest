using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Iparty.SigninUpload.Models
{
    public class DirInfo
    {
        public string Name { get; set; }
        public string AppliedContactID { get; set; }
        public string DirectSellerID { get; set; }
        public string Title { get; set; }
        public DateTime EventStartDate { get; set; }
        public Guid PartyKey { get; set; }
    }

    public class PartyInfo
    {
        public Guid PartyKey { get; set; }
        public string Name { get; set; }
        public string DirectSellerID { get; set; }
        public string Title { get; set; }
        public string Location {  get {
            return Province + City + County + ShopAddress;
        }  }
        public string Province { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string ShopAddress { get; set; }

        public string PartyTime { get {
            return DisplayStartDate.ToString("yyyy年MM月dd日") + " " + DisplayStartDate.ToString("HH:mm") + " ~ " + DisplayEndDate.ToString("HH:mm");
        }  }
        public DateTime DisplayStartDate { get; set; }
        public DateTime DisplayEndDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? ActualStart { get; set; }
        public DateTime? ActualEnd { get; set; }

    }

    public class Customer
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Status { get; set; }
        public string CheckInType { get; set; }
        public string Reference { get; set; }
        public string DirectSellerID { get; set; }
    }


    public class Consultant
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public int Level { get; set; }
        public string UnitID { get; set; }
        public string RecruiterOrDirId { get; set; }
    }

    public class partyUnitee
    {
        public string PartyKey { get; set; }
        public string UnitID { get; set; }
        public string ParUnitId { get; set; }
    }

    
}