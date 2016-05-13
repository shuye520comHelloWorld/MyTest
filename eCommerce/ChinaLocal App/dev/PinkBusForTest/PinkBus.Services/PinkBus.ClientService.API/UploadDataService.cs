using PinkBus.ClientServices.Contract;
using PinkBus.ClientServices.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.ClientServices.API
{
    public class UploadDataService:ServiceStack.Service
    {
         private IUpLoadDataRepository UpLoadDataRepository;
        public UploadDataService(IUpLoadDataRepository upLoadDataRepository)
        {
            this.UpLoadDataRepository = upLoadDataRepository;
        }

        public UploadCheckinDataResponse Post(UploadCheckinData dto)
        {
            return UpLoadDataRepository.UploadCheckinData(dto);
        }
    }
}
