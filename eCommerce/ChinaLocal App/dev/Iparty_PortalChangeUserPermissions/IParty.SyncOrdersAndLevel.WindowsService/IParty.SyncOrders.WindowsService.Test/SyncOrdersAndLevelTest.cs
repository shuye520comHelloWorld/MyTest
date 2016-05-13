using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IParty.SyncOrdersAndLevel.WindowsService.Test
{
    [TestClass]
    public class SyncOrdersAndLevelTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var exection = new SyncOrdersAndLevel();
            exection.SyncOrdersAndLevelTask();
        }
    }
}
