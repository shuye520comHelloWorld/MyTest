using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Services.ORM.Operations
{
    public class UniteeOperation : BaseEntityOperation<UniteeEntity>
    {
        public UniteeOperation() : this(ConfigurationManager.AppSettings["Community"]) { }
        public UniteeOperation(string dbStr) : base(dbStr) { }
    }

    [Alias("iParty_Unitee")]
    public class UniteeEntity
    {
        [CustomField("uniqueidentifier")]
        [Description("the foreign key reference to party.")]
        [Required]
        public Guid PartyKey { get; set; }
        [CustomField("BIGINT")]
        [Required]
        public long UniteeContactID { get; set; }
        [CustomField("NVARCHAR(50)")]
        public string FullName { get; set; }
        [CustomField("INT")]
        public int LeveLID { get; set; }
        [CustomField("DATETIME")]
        public DateTime ConsultantStartDate { get; set; }

        [CustomField("DATETIME")]
        public DateTime CreatedDate { get; set; }
        [CustomField("VARCHAR(10)")]
        public string UnitId { get; set; }
        [CustomField("VARCHAR(50)")]
        public String CreatedBy { get; set; }

    }
}
