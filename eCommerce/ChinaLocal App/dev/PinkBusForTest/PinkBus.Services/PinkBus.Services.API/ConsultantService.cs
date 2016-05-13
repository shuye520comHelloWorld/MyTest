using PinkBus.Services.Contract;
using PinkBus.Services.Interface;
using PinkBus.Services.OAuth.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.Services.API
{
     
    [OAuthRequest(ProjectType.Intouch)]
    public class ConsultantService:ServiceStack.Service
    {
        private IConsultantRepository consultantRepository;

        public ConsultantService(IConsultantRepository consultantRepository) 
        {
            this.consultantRepository = consultantRepository;
        }

        public GetProfileResponse Get(GetProfile dto)
        {
            return consultantRepository.GetProfile(dto);
        }

        public QueryUnitsBCResponse Get(QueryUnitsBC dto)
        {
            return consultantRepository.QueryUnitsBC(dto);
        }

        public UpdateProfileResponse Put(UpdateProfile dto)
        {
            return consultantRepository.UpdateProfile(dto);
        }

        public UpdateProfileResponse Post(UpdateProfile dto)
        {
            return consultantRepository.UpdateProfile(dto);
        }

    }
}
