using System;
using iParty.Services.Interface;
using System.Collections.Generic;

namespace QuartES.Services.Core
{
    public interface IContext
    {
        Guid? CommandId { get; }
        long ConsultantContactId { get; }
        IUser User { get; }
        string ClientIdentity { get; }
        string ClientKey { get; }
        string UserName { get; }
        string Culture { get; }
        string UICulture { get; }
        string SubsidiaryCode { get; }
        string Timezone { get; }
      
    }
}
