using iParty.Services.Contract;
using iParty.Services.Interface;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Services.Impl
{
    public sealed class PartyService : Service
    {
        IPartyRepository _partyRepository;
        public PartyService(IPartyRepository partyRepository)
        {
            _partyRepository = partyRepository;
        }

        public object Get(InvitationTokenRequest req)
        {
            return _partyRepository.GenerateToken(req);
        }
    }
}
