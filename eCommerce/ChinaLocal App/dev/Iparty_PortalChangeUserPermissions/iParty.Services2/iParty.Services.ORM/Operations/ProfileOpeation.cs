using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Services.ORM.Operations
{ 
    public class ProfileOpeation : BaseEntityOperation<Profile>
    {
        public ProfileOpeation() : this(ConfigurationManager.AppSettings["Community"]) { }
        public ProfileOpeation(string dbStr) : base(dbStr) { }
    }
    public class Profile
    {
        public long ContactID { get; set; }
        public string DirectSellerID { get; set; }
        public string ResidenceId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LevelCode { get; set; }
        public string BusinessDirector { get; set; }
        public DateTime StartDate { get; set; }
        public string ConsultantStatus { get; set; }
        public string ConsultantLevelID { get; set; }
        public string UnitID { get; set; }
        public string PhoneNumber { get; set; }
        public string StreetAddress { get; set; }
        public string ProvinceName { get; set; }
        public int ProvinceID { get; set; }
        public string CityName { get; set; }
        public int CityID { get; set; }
        public string CountyName { get; set; }
        public int CountyID { get; set; }
        public string PostalCode { get; set; }
        public string DirectorLastName { get; set; }
        public string DirectorFirstName { get; set; }


    }
}
