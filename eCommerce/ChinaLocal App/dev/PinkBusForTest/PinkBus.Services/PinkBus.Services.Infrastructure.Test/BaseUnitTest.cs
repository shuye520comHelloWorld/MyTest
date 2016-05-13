using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinkBus.Services.Infrastructure.Test.Common;

namespace PinkBus.Services.Infrastructure.Test
{
    [TestClass]
    public class BaseUnitTest
    {
        public PinkBusHelper helper;
      
        [TestInitialize]
        public void SetUp()
        {
            helper = new PinkBusHelper(true, Helper.EnvType.local, Helper.HostType.Pinkbus);
        }
    }
}
