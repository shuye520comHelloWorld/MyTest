using System.Collections.Generic;

namespace QuartES.Services.Core
{
    public interface IQueryCriteria
    {
        int Limit { get; }
        long Offset { get; }
        string[] Fields { get; }
        string[] Sort { get; }
        string[] Q { get; }
        object Filters { get; }
        Dictionary<string, string> Terms { get; }
        object CustomQuery { get; }

        IQueryCriteria BuildCriteria(
            int? limit = null,
            long? offset = null,
            string[] fields = null,
            string[] sort = null,
            string[] q = null,
            object filters = null,
            Dictionary<string, string> terms = null,
            object customQuery = null);
    }
}
