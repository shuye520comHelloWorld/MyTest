using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IParty.SyncOrders.WindowsService.Test
{
    [TestClass]
    public class SyncOrdersTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var exection = new SyncOrders();
            exection.SyncOrdersTask();
        }
    }
}
