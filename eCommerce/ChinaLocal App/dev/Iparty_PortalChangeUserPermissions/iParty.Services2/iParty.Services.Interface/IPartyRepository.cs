
using iParty.Services.Contract;
using System;
namespace iParty.Services.Interface
{
    public interface IPartyRepository
    {
        object GenerateToken(InvitationTokenRequest req);
        InvitationTokenEntity GetTokenEntity(string token);
        string GenerateToken(Guid partyKey, long hostContactID, int workshopID);
    }

    public class InvitationTokenEntity
    {
        public Guid? PartyKey { get; set; }
        public long? AppliedContactID { get; set; }
        public long? CurrentUserContactID { get; set; }
        public string UnitID { get; set; }
        public int? WorkshopId { get; set; }
    }
}
