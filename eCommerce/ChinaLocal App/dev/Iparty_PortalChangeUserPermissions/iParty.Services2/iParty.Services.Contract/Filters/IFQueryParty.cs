using iParty.Services.Entity;
using System;

namespace iParty.Services.Contract.Filters
{
    public interface IFQueryParty
    {
        Guid? EventKey { get; set; }
        Guid? PartyKey { get; set; }
        PartyStage? PartyStage { get; set; }
        bool? IsFinished { get; set; }
    }
}
