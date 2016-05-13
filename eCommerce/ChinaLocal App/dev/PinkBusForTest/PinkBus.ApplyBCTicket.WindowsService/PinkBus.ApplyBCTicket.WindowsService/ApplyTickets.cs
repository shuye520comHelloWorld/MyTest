using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.ApplyBCTicket.WindowsService
{
    public class ApplyTickets
    {
        static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public void ApplyTrackersTickets()
        {
            //if (DateTime.Now > AppSetting.WorkStart && DateTime.Now < AppSetting.WorkEnd)
            //{
                _logger.Debug(" Working ");
                List<PB_ApplyTicketTracker> Trackers = DAO.queryTicketTrackers();

                foreach (var T in Trackers)
                {
                    //_logger.Error("TrackerKey:" + T.TrackerKey);
                    _logger.Debug("TrackerKey:" + T.TrackerKey);
                    DAO.ApplyTickets(T);
                }
           // }
        }

        //public async Task ApplyTicketsAsync()
        //{
        //    List<PB_ApplyTicketTracker> Trackers = DAO.queryTicketTrackers();

        //    foreach (var T in Trackers)
        //    {
        //        DAO.ApplyTickets(T);
        //    }
        //}


        //static void Main(string[] args)
        //{
        //    if (DateTime.Now > AppSetting.WorkStart && DateTime.Now < AppSetting.WorkEnd)
        //    {
        //        _logger.Debug("In Working Time");
        //        List<PB_ApplyTicketTracker> Trackers = DAO.queryTicketTrackers();

        //        foreach (var T in Trackers)
        //        {
        //            _logger.Error("TrackerKey:" + T.TrackerKey);
        //            _logger.Debug("TrackerKey:" + T.TrackerKey);
        //            DAO.ApplyTickets(T);
        //        }
        //    }
        //}
    }
}
