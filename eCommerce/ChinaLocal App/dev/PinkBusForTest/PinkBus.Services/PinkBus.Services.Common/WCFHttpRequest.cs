using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.Services.Common
{
    public class WCFHttpRequest
    {
        public static T CreateWCFServiceByURL<T>(string url)
        {
            if (string.IsNullOrEmpty(url)) throw new NotSupportedException("this url isn`t Null or Empty!");
            EndpointAddress address = new EndpointAddress(url);
            Binding binding = CreateBasicBinding();
            ChannelFactory<T> factory = new ChannelFactory<T>(binding, address);
            return factory.CreateChannel();
        }

        private static Binding CreateBasicBinding()
        {
            BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
            basicHttpBinding.MaxBufferSize = 2147483647;
            basicHttpBinding.MaxBufferPoolSize = 2147483647;
            basicHttpBinding.MaxReceivedMessageSize = 2147483647;
            basicHttpBinding.ReaderQuotas.MaxStringContentLength = 2147483647;
            basicHttpBinding.CloseTimeout = new TimeSpan(0, 10, 0);
            basicHttpBinding.OpenTimeout = new TimeSpan(0, 10, 0);
            basicHttpBinding.ReceiveTimeout = new TimeSpan(0, 10, 0);
            basicHttpBinding.SendTimeout = new TimeSpan(0, 10, 0);
            return basicHttpBinding as Binding;
        }
    }
}
