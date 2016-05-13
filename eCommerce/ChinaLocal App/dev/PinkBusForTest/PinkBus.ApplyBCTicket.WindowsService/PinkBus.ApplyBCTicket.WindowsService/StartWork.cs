using MaryKay.ServiceHost.Workers;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.ApplyBCTicket.WindowsService
{
    public class StartWork : IGetWorkStrategy
    {
        readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public void GetWork(AddTask addTask)
        {
            try
            {
                //_logger.Debug("StartWork  Begin");
                //_logger.Warn("StartWorkForVoteLog form bill Begin");
                ApplyTickets worker = new ApplyTickets();
                worker.ApplyTrackersTickets();
                //var _ = worker.ApplyTicketsAsync().ContinueWith(t =>
                //    _logger.Error("StartWorkForVoteLog ex:" + t.Exception.ToString())
                //    );

                //_logger.Info("StartWork End");
            }
            catch (System.Exception ex)
            {
                _logger.Error("StartWork err ex:" + ex.ToString());

            }
        }

        public void Initialize(System.Xml.XmlNode getWorkProperties)
        {
            //throw new NotImplementedException();
        }

    }

    //public class StartWorkASync : IGetWorkStrategy
    //{
    //    readonly Logger _logger = LogManager.GetCurrentClassLogger();

    //    public void GetWork(AddTask addTask)
    //    {
    //        try
    //        {
    //            _logger.Info("StartWork  Begin");
    //            //_logger.Warn("StartWorkForVoteLog form bill Begin");
    //            ApplyTickets worker = new ApplyTickets();
    //            //worker.ApplyTrackersTickets();
    //            var _ = worker.ApplyTicketsAsync().ContinueWith(t =>
    //                _logger.Error("StartWorkForVoteLog ex:" + t.Exception.ToString())
    //                );

    //            _logger.Info("StartWork End");
    //        }
    //        catch (System.Exception ex)
    //        {
    //            _logger.Error("StartWork err ex:" + ex.ToString());

    //        }
    //    }

    //    public void Initialize(System.Xml.XmlNode getWorkProperties)
    //    {
    //        //throw new NotImplementedException();
    //    }

    //}
}
