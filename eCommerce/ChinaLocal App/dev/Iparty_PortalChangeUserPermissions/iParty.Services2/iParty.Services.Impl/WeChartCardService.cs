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
    public sealed class WeChartCardService : Service
    {
        IWeChartCardRepository _weCardRepository;
        public WeChartCardService(IWeChartCardRepository weCardRepository)
        {
            _weCardRepository = weCardRepository;
        }

        public object Post(CreateWeChartCard dto)
        {
            return _weCardRepository.CreateWeCard(dto);
        }

        public object Put(UpdateWeChartCard dto)
        {
            return _weCardRepository.UpdateWeCard(dto);
        }

        public object Post(QueryWeChart dto)
        {
            return _weCardRepository.GetCardID(dto);
        }
    }
}
