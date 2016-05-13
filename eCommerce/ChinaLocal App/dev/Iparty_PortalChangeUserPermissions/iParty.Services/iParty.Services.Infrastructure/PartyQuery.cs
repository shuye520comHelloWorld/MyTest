using iParty.Services.Entity;
using iParty.Services.Interface;
using iParty.Services.ORM.Operations;
using QuartES.Services.Core;
using System;
using System.Collections.Generic;

namespace iParty.Services.Infrastructure
{
    public sealed class PartyQuery : IPartyQuery
    {
        readonly IContext _context;
        readonly IQueryCriteria _queryCriteria;
        private IPartyApplicationRepository _partyApplicationRepository;
        private IInvitationRepository _invitationRepository;
        public PartyQuery(IContext context, IQueryCriteria queryCriteria,
            IPartyApplicationRepository partyApplicationRepository,
            IInvitationRepository invitationRepository)
        {
            _context = context;
            _queryCriteria = queryCriteria;
            _partyApplicationRepository = partyApplicationRepository;
            _invitationRepository = invitationRepository;
        }

        public List<ApplicationEntry> GetHistory()
        {
           

           var Historys =  _partyApplicationRepository.GetHistory();
           //total = Historys.Count;
           return Historys;
        }

        public List<ApplicationEntryWithDetail> GetHistory(Guid partyKey)
        {
            throw new NotImplementedException();
        }

        public List<ApplicationEntry> GetApplication()
        {
            return _partyApplicationRepository.Get();
            
          
        }

        public List<ApplicationEntryWithDetail> GetApplicationDetail(Guid partyKey)
        {

            return _partyApplicationRepository.GetDetail(partyKey); 
        }

        public List<InvitationEntry> GetInvitation()
        {
            return _partyApplicationRepository.GetPartyInvitation();
        }

        public List<InvitationEntryWithDetail> GetInvitationDetail(Guid partyKey)
        {
            return _partyApplicationRepository.GetOpenForInvitationDetail(partyKey);
        }

        public List<SigninEntry> GetSignin()
        {
            return _invitationRepository.GetSignin();
        }

        public List<SigninEntryWithDetail> GetSigninDetail(Guid partyKey)
        {
            return _partyApplicationRepository.GetSigninDetail(partyKey);
        }
    }
}
