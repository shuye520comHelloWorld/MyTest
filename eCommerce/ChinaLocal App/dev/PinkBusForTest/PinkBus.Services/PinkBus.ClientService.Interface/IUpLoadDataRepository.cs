﻿using PinkBus.ClientServices.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.ClientServices.Interface
{
    public interface IUpLoadDataRepository
    {
        UploadCheckinDataResponse UploadCheckinData(UploadCheckinData dto);
    }
}
