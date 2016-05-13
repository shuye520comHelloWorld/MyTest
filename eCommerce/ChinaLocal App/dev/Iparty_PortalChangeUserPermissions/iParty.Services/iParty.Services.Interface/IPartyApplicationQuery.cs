using iParty.Services.Entity;
using System.Collections.Generic;

namespace iParty.Services.Interface
{
    public interface IPartyApplicationQuery
    {
        IEnumerable<object> GetPartyApplications(out long total);
    }
}