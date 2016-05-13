using PinkBus.ClientServices.Contract;
using PinkBus.ClientServices.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.ClientServices.API
{
    public class CheckTokenService : ServiceStack.Service
    {
        private ICheckTokenRepository checkTokenRepository;
        public CheckTokenService(ICheckTokenRepository CheckTokenRepository)
        {
            this.checkTokenRepository = CheckTokenRepository;
        }

        public object Get(CheckToken dto)
        {
            return checkTokenRepository.CheckToken(dto);
        }

        //public object Post(UploadCheckinData dto)
        //{
        //    return new object();
        //}

        //public object Post(UploadCustomerData dto)
        //{
        //    return new object();
        //}

    }
}
