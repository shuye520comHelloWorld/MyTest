using PinkBus.ClientServices.Contract;
using PinkBus.ClientServices.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.ClientServices.API
{
    public class UploadOfflineCustomerService : ServiceStack.Service
    {
        private IUpLoadOfflineCustomerRepository offlineCustomerRepository;
        public UploadOfflineCustomerService(IUpLoadOfflineCustomerRepository offlineCustomerRepository)
        {
            this.offlineCustomerRepository = offlineCustomerRepository;
        }

        public UploadOfflineCustomerResponse Post(UploadOfflineCustomer dto)
        {
            return offlineCustomerRepository.UploadOfflineCustomer(dto);
        }

        public UploadOfflineCustomerResponse Put(UploadOfflineCustomer dto)
        {
            return offlineCustomerRepository.UploadOfflineCustomer(dto);
        }
    }
}
