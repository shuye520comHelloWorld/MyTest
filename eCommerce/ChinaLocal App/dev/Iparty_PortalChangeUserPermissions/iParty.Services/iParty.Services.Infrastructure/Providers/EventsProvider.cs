using iParty.Services.Entity;
using iParty.Services.Interface;
using iParty.Services.ORM.Operations;
using QuartES.Services.Core;
using System;
using System.Collections.Generic;

namespace iParty.Services.Infrastructure.Providers
{
    public class EventsProvider : IEventsProvider
    {
        readonly IContext _context;
        private const string EVENTSKEY = "iParty_Events";
        private const string EVENTDETAILSKEY = "iParty_EventDetail_";
        public EventsProvider(IContext context)
        {
            _context = context;
        }

        private const int cachedMiniutes = 60;

        private static List<EventEntity> GetAllEventFromCache()
        {
            var eventlist = CacheHelper.Get<List<EventEntity>>(EVENTSKEY);
            if (eventlist == null || eventlist.Count <= 0)
            {
                var eventOperation = new EventOperation();
                eventlist = eventOperation.GetAllData();
                CacheHelper.Insert<List<EventEntity>>(EVENTSKEY, eventlist, cachedMiniutes);
            }

            return eventlist;
        }

        private static List<EventDetailEntity> GetAllEventDetailFromCache(Guid eventkey)
        {
            var eventdetaillist = CacheHelper.Get<List<EventDetailEntity>>(EVENTDETAILSKEY + eventkey);
            if (eventdetaillist == null || eventdetaillist.Count <= 0)
            {
                var eventDetailOperation = new EventDetailOperation();
                eventdetaillist = eventDetailOperation.GetDataByFunc(t => t.EventKey == eventkey);
                CacheHelper.Insert<List<EventDetailEntity>>(EVENTDETAILSKEY + eventkey, eventdetaillist, cachedMiniutes);
            }

            return eventdetaillist;
        }
        public IEnumerable<EventEntry> GetBy()
        {
            var events = GetAllEventFromCache();
            var result = new List<EventEntry>();
            foreach (var item in events)
            {
                var eventItem = new EventEntry();
                eventItem.EventKey = item.EventKey;
                eventItem.Title = item.Title;
                if (item.Category == 1)
                {
                    eventItem.Category = PartyCategory.Love;
                }
                else
                {
                    eventItem.Category = PartyCategory.HighendVIP;
                }
                eventItem.ApplicationEndDate = item.ApplicationEndDate;
                eventItem.ApplicationStartDate = item.ApplicationStartDate;
                eventItem.EventStartDate = item.EventStartDate;
                eventItem.EventEndDate = item.EventEndDate;
                eventItem.Description = item.Description;
                //eventItem.de
                result.Add(eventItem);
            }
            return result;
            //  throw new NotImplementedException();
        }

        public IEnumerable<EventEntryWithDetail> GetDetailBy(Guid eventKey)
        {

            var events = GetAllEventFromCache();
            var myevent = new EventEntity();
            foreach (var item in events)
            {
                if (item.EventKey == eventKey)
                {
                    myevent = item;
                    break;
                }
            }
            if (myevent == null)
            {
                return null;
            }
            var result = new List<EventEntryWithDetail>();
            var eventItem = new EventEntryWithDetail();
            eventItem.EventKey = myevent.EventKey;
            eventItem.Title = myevent.Title;
            if (myevent.Category == 1)
            {
                eventItem.Category = PartyCategory.Love;
            }
            else
            {
                eventItem.Category = PartyCategory.HighendVIP;
            }
            eventItem.ApplicationEndDate = myevent.ApplicationEndDate;
            eventItem.ApplicationStartDate = myevent.ApplicationStartDate;
            eventItem.Description = myevent.Description;

            var eventDetailOperation = new EventDetailOperation();
            var eventDetails = GetAllEventDetailFromCache(eventKey);// eventDetailOperation.GetDataByFunc(t => t.EventKey == myevent.EventKey);
            var notes = new List<string>();
            foreach (var eventDetail in eventDetails)
            {
                notes.Add(eventDetail.Note);
            }
            eventItem.Notes = notes;
            eventItem.EventStartDate = myevent.EventStartDate;
            eventItem.EventEndDate = myevent.EventEndDate;
            eventItem.PartyAllowEndDate = myevent.PartyAllowEndDate;
            eventItem.PartyAllowStartDate = myevent.PartyAllowStartDate;
            result.Add(eventItem);

            return result;

        }

        public void GetNotAppliedEvents(List<object> partyList)
        {
            var eventOperation = new EventOperation();
            var events = eventOperation.LeftJoin<EventEntity, PartyEntity, PartyEntity>((e, p) => e.EventKey == p.EventKey, p => p.EventKey != null);
            foreach (var anEvent in events)
            {
                PartyCategory category;
                Enum.TryParse<PartyCategory>(anEvent.Category.ToString(), out category);
                partyList.Add(new ApplicationOpenedQueryResult
                {
                    EventKey = anEvent.EventKey,
                    Title = anEvent.Title,
                    Category = category,
                    Stage = PartyStage.OpenForApplication,
                    ApplicationStartDate = anEvent.ApplicationStartDate,
                    ApplicationEndDate = anEvent.ApplicationEndDate
                });
            }
        }
    }
}
