using System.Collections.Generic;

namespace QuartES.Services.Core
{
    public class QueryCriteria : IQueryCriteria
    {
        public int Limit { get; protected set; }
        public long Offset { get; protected set; }
        public string[] Fields { get; protected set; }
        public string[] Sort { get; protected set; }
        public string[] Q { get; protected set; }
        public object Filters { get; protected set; }
        public Dictionary<string, string> Terms { get; protected set; }
        public object CustomQuery { get; protected set; }

        public QueryCriteria() { }

        public IQueryCriteria BuildCriteria(
            int? limit = null,
            long? offset = null,
            string[] fields = null,
            string[] sort = null,
            string[] q = null,
            object filters = null,
            Dictionary<string, string> terms = null,
            object customQuery = null)
        {
            if (limit.HasValue && limit > 0) this.Limit = limit.Value; else if (this.Limit <= 0) this.Limit = 20;
            if (offset.HasValue && offset >= 0) this.Offset = offset.Value; else if (this.Offset < 0) this.Offset = 0;
            if (fields != null) this.Fields = fields;
            if (sort != null) this.Sort = sort;
            if (q != null) this.Q = q;
            if (filters != null) this.Filters = filters;
            if (terms != null) this.Terms = terms;
            if (customQuery != null) this.CustomQuery = customQuery;

            return this;
        }
    }
}
