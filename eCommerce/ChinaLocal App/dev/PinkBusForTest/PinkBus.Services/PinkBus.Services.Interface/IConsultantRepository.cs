using PinkBus.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.Services.Interface
{
    public interface IConsultantRepository
    {
        GetProfileResponse GetProfile(GetProfile dto);

        UpdateProfileResponse UpdateProfile(UpdateProfile dto);

        QueryUnitsBCResponse QueryUnitsBC(QueryUnitsBC dto);
    }
}
