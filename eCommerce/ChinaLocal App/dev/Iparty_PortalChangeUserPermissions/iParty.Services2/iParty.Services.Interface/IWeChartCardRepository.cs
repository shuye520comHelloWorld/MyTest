using iParty.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Services.Interface
{
    public interface IWeChartCardRepository
    {
        QueryWeChartResponse GetCardID(QueryWeChart dto);
        CreateWeChartCardResponse CreateWeCard(CreateWeChartCard dto);
        CreateWeChartCardResponse UpdateWeCard(UpdateWeChartCard dto);
    }
}
