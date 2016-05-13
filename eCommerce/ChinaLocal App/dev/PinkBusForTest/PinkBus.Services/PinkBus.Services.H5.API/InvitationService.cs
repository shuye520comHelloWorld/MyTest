using PinkBus.Services.Contract;
using PinkBus.Services.Interface;
using PinkBus.Services.OAuth.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.Services.H5.API
{
    public class InvitationService : ServiceStack.Service
    {
        private IInvitationRepository invitationRepository;


        public InvitationService(IInvitationRepository invitationRepository)
        {
            this.invitationRepository = invitationRepository;
        }


        public AcceptInvitationResponse Post(AcceptInvitation dto)
        {
            return invitationRepository.AcceptInvitation(dto);
        }

        public AcceptInvitationResponse Put(AcceptInvitation dto)
        {
            return invitationRepository.AcceptInvitation(dto);
        }

        [OAuthRequest(ProjectType.H5)]
        public QueryInvitationResponse Get(QueryInvitation dto)
        {
            return invitationRepository.QueryInvitation(dto);
        }

        public QueryInvitationResponse Get(QueryInvitationBrowser dto)
        {
            return invitationRepository.QueryInvitationBrowser(dto);
        }

     
    }
}
