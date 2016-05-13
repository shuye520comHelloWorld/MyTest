using MaryKay.ServiceHost.Workers;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IParty.SyncDirShops.WindowsService
{
    public class SyncDirShopsWorkStart : IGetWorkStrategy
    {
        readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public void GetWork(AddTask addTask)
        {
            try
            {
                _logger.Info("StartWorkForSyncLog  Begin");
                _logger.Warn("StartWorkForSyncLog  Begin");
                SyncDirShops worker = new SyncDirShops();
                worker.SyncDirShopsTask();
               
                _logger.Info("StartWorkForSyncLog End");
            }
            catch (System.Exception ex)
            {
                _logger.Error("StartWorkForSyncLog ex:" + ex.ToString());

            }
        }

        public void Initialize(System.Xml.XmlNode getWorkProperties)
        {
            //throw new NotImplementedException();
        }
    }
}
