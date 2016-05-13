using PinkBus.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.Services.Interface
{
    public interface IInvitationRepository
    {
        QueryInvitationResponse QueryInvitation(QueryInvitation dto);  

        QueryInvitationResponse QueryInvitationBrowser(QueryInvitationBrowser dto);
      
        AcceptInvitationResponse AcceptInvitation(AcceptInvitation dto);

        CheckEventInvitationStatusResponse CheckEventInvitationStatus(CheckEventInvitationStatus dto);
        
        //GetOptionsResponse GetOptions(GetOptions dto)
        //{
        //    return null; 
        //}
       
    }
}
