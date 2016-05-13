using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Services.ORM.Operations
{
    public sealed class UnitOperation : BaseEntityOperation<UnitEntity>
    {
        public UnitOperation() : this(ConfigurationManager.AppSettings["ContactsLite"]) { }
        public UnitOperation(string dbStr) : base(dbStr) { }
    }

    [Alias("Units")]
    public sealed class UnitEntity
    {
        public string UnitID { get; set; }
        public long Director { get; set; }
        public DateTime DebutDate { get; set; }
    }
}
