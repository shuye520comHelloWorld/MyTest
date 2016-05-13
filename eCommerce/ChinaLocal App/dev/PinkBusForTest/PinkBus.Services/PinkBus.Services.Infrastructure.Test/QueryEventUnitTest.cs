using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinkBus.Services.Infrastructure.Test.Common;

using PinkBus.Services.Contract;
using PinkBus.Services.Entity;
using System.Diagnostics;

namespace PinkBus.Services.Infrastructure.Test
{
    [TestClass]
    public class QueryEventUnitTest : BaseUnitTest
    {
        [TestMethod]
        public void TestQueryEvents()
        {
            Stopwatch watcher = new Stopwatch();
            watcher.Start(); 
            for (int i = 0; i < 500; i++)
            {
                QueryEventResponse response = base.helper.client.Get(new QueryEvent
                {
                    Device = "x",
                    DeviceOS = "x",
                    DeviceVersion = "x",
                });
            }
            watcher.Stop();
            Console.WriteLine(watcher.ElapsedMilliseconds);

        }
    }
}
