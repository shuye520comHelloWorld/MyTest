using PinkBus.Services.Contract;
using PinkBus.Services.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.Services.Infrastructure.Mapper
{
    public static class EventToBaseInfoMapper
    {
        public static EventBaseInfo ToEventBaseInfo(this Event entity)
        {
            return new EventBaseInfo
            {
                EventKey = entity.EventKey,
                EventTitle = entity.EventTitle,
                Location = entity.EventLocation,
                EventEndDate = entity.CheckInEndDate,
                EventStartDate = entity.CheckInStartDate,
               // InvitationEndDate= entity.InvitationEndDate,               
            };
        }
    }
}
