using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Services.ORM.Operations
{
    public class WeChartCardCodeOperation : BaseEntityOperation<WeChartCardCodeEntity>
    {
         public WeChartCardCodeOperation() : this(ConfigurationManager.AppSettings["Community"]) { }
         public WeChartCardCodeOperation(string dbStr) : base(dbStr) { }
    }

    [Alias("IParty_WXCardCode")]
    public class WeChartCardCodeEntity
    {
        [CustomField("uniqueidentifier")]
        [Description("the primary key of a wechart card code.")]
        [Alias("CardCode")]
        [Required]
        public Guid InvitationKey { get; set; }        
        [CustomField("VARCHAR(50)")]
        [Alias("CardId")]
        public string CardID { get; set; }
        [CustomField("uniqueidentifier")]
        public Guid PartyKey { get; set; }
        [CustomField("INT")]
        public int Status { get; set; }
        [CustomField("DATETIME")]
        public DateTime CreateDate { get; set; }
        [CustomField("DATETIME")]
        public DateTime UpdateDate { get; set; }
    }
}
