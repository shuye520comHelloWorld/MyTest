using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinkBus.Services.Infrastructure.Test.Common;

using PinkBus.Services.Contract;
using PinkBus.Services.Entity;

namespace PinkBus.Services.Infrastructure.Test
{
    [TestClass]
    public class InvitationUnitTest : BaseUnitTest
    {
        [TestMethod]
        public void QueryInvitationTest()
        {
            QueryInvitationResponse response = base.helper.client.Get(new QueryInvitation
            {
                Device = "x",
                DeviceOS = "x",
                DeviceVersion = "x",
                TicketKey = new Guid("9FBB5F76-9B30-4F33-961D-00066DEE6055")

            });
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void AcceptInvitationTest()
        {
            AcceptInvitationResponse response = base.helper.client.Put(new AcceptInvitation
            {
                Device = "x",
                DeviceOS = "x",
                DeviceVersion = "x",
                TicketKey = new Guid("9FBB5F76-9B30-4F33-961D-00066DEE6055")

            });
        }

    }
}
