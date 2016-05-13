using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PinkBus.Services.Entity;
using contract = PinkBus.Services.Contract;
namespace PinkBus.Services.Infrastructure.Mapper
{
    public static class EventSessionMapper
    {
        public static List<contract.EventSession> EventSessionToResponse(this List<EventSession> sessionList,int applyQuantityPerSession)
        {
            List<contract.EventSession> list = new List<contract.EventSession>();
            sessionList.ForEach((each) => {

                contract.EventSession  session= new contract.EventSession
                {
                    SessionKey = each.SessionKey,
                    SessionStartDate=each.SessionStartDate,
                    SessionEndDate=each.SessionEndDate,                           
                    CanApply = each.CanApply,                   
                    TicketOut=each.TicketOut,
                    ApplyQuantityPerSession = applyQuantityPerSession

                };
                list.Add(session);
            });

            return list;
        }
    }
}
