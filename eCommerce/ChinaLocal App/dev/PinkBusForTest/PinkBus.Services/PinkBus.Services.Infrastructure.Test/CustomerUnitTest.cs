using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinkBus.Services.Infrastructure.Test.Common;

using PinkBus.Services.Contract;
using PinkBus.Services.Entity;

namespace PinkBus.Services.Infrastructure.Test
{
    [TestClass]
    public class CusomterUnitTest : BaseUnitTest
    {
        [TestMethod]
        public void TestGetCustomerDetail()
        {
            GetCustomerDetailResponse response = base.helper.client.Get(new GetCustomerDetail
            {
                Device = "x",
                DeviceOS = "x",
                DeviceVersion = "x",
                CustomerKey = Guid.NewGuid()

            });
        }

        [TestMethod]
        public void TestupdateCustomerDetail()
        {
            UpdateCustomerResponse response = base.helper.client.Put(new UpdateCustomer
            {
                Device = "x",
                DeviceOS = "x",
                DeviceVersion = "x",
                CustomerKey = Guid.NewGuid(),
                UnionID = "",
                IsImportMyCustomer = true

            });
        }

    }
}
