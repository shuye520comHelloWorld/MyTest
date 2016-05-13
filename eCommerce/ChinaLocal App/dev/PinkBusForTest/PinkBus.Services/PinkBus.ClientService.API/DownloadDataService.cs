using PinkBus.ClientServices.Contract;
using PinkBus.ClientServices.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.ClientServices.API
{
    public class DownloadDataService:ServiceStack.Service
    {
        private IDownLoadDataRepository downLoadDataRepository;
        public DownloadDataService(IDownLoadDataRepository DownLoadDataRepository)
        {
            this.downLoadDataRepository = DownLoadDataRepository;
        }

        public DownloadDataResponse Get(DownloadData dto)
        {
            return downLoadDataRepository.DownloadData(dto);
        }
    }
}
