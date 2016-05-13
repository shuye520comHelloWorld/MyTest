using MaryKay.ServiceHost.Workers;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.SendActivitySMS.WindowsService
{
    public class StartWork : IGetWorkStrategy
    {
        readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public void GetWork(AddTask addTask)
        {
            try
            {
                _logger.Debug("begin GetWork----------------" );               
                ActivitySMS aSMS = new ActivitySMS();
                aSMS.Send();
                _logger.Debug("end GetWork----------------");
             
            }
            catch (System.Exception ex)
            {
                _logger.Error("StartWork err ex:" + ex.ToString());

            }
        }

        public void Initialize(System.Xml.XmlNode getWorkProperties)
        {
            // throw new NotImplementedException();
        }
    }
}
