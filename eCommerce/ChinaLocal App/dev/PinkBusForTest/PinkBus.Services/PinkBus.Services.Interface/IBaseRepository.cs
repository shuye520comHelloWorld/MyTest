using PinkBus.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.Services.Interface
{
    public interface IBaseRepository
    {
        EventBaseInfo GetEventBaseInfo(Guid eventKey);

        List<County> GetCountyList(string cityName);
    }
}
