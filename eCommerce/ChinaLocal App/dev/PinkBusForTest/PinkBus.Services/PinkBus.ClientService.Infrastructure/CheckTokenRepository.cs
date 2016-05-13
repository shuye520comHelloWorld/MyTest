using PinkBus.ClientServices.Contract;
using PinkBus.ClientServices.Interface;
using PinkBus.Services.Common;
using PinkBus.Services.Entity;
using PinkBus.Services.Entity.Operation;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.ClientServices.Infrastructure
{
    public class CheckTokenRepository : ICheckTokenRepository
    {

        private EventOperation eventOperation;
        private EventSessionOperation eventSessionOperation;

        public CheckTokenRepository()
        {
            eventOperation = new EventOperation(GlobalAppSettings.Community);
            eventSessionOperation = new EventSessionOperation(GlobalAppSettings.Community);
        }

        public CheckTokenResponse CheckToken(CheckToken dto)
        {
            CheckTokenResponse res = new CheckTokenResponse() ;
            List<Event> Events = new List<Event>();
            if (dto.TokenType == 1)
            {
                Events =eventOperation.GetDataByFunc(e => e.DownloadToken == dto.Token);
            }
            else if (dto.TokenType == 2)
            {
                Events =eventOperation.GetDataByFunc(e => e.UploadToken == dto.Token);
            }

            if (Events.Count > 0)
            {
                List<EventSession> Sessions = new List<EventSession>();
                Sessions = eventSessionOperation.GetDataByFunc(e => e.Eventkey == Events[0].EventKey);
                //res.Result = true;
                res.Event = Events[0];
                res.EventSessions = Sessions;

            }
            else
            {
                throw new HttpError(HttpStatusCode.NotFound, "NotFound", "活动码有误！");

            }

            return res;
        }

    }


}
