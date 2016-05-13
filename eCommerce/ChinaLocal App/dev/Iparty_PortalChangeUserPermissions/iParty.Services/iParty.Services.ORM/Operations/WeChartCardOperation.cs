using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Services.ORM.Operations
{
    public class WeChartCardOperation : BaseEntityOperation<WeChartCardEntity>
    {
        public WeChartCardOperation() : this(ConfigurationManager.AppSettings["Community"]) { }
        public WeChartCardOperation(string dbStr) : base(dbStr) { }
    }

    [Alias("IParty_WXCard")]
    public class WeChartCardEntity
    {
        [CustomField("uniqueidentifier")]
        [Description("the primary key of a wechart card.")]
        [Required]
        public Guid EventKey { get; set; }
        [CustomField("VARCHAR(50)")]
        [Alias("CardId")]
        public string CardID { get; set; }
        [CustomField("DATETIME")]
        public DateTime CreateDate { get; set; }
    }
}
