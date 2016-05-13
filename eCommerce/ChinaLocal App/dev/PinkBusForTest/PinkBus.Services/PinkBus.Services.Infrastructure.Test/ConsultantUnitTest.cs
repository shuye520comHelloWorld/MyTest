using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinkBus.Services.Infrastructure.Test.Common;

using PinkBus.Services.Contract;
using PinkBus.Services.Entity;

namespace PinkBus.Services.Infrastructure.Test
{
    [TestClass]
    public class ConsultantUnitTest : BaseUnitTest
    {
        [TestMethod]
        public void TestUpdateProfile()
        {
            UpdateProfileResponse response = base.helper.client.Put(new UpdateProfile
            {
                Device = "x",
                DeviceOS = "x",
                DeviceVersion = "x",
                EventKey = new Guid("c3165616ec04423fa9d7e049a01faac5"),
                CountyCode = 722,
                CountyName = "长宁区"

            });
        }

        [TestMethod]
        public void TestGetProfile()
        {
            GetProfileResponse response = base.helper.client.Get(new GetProfile
            {
                Device = "x",
                DeviceOS = "x",
                DeviceVersion = "x",
                EventKey = new Guid("c3165616ec04423fa9d7e049a01faac5")
            });
        }


        [TestMethod]
        public void TestQueryUnitBc()
        {
            QueryUnitsBCResponse response = base.helper.client.Get(new QueryUnitsBC
            {
                Device = "x",
                DeviceOS = "x",
                DeviceVersion = "x"                
            });
        }
    }
}
