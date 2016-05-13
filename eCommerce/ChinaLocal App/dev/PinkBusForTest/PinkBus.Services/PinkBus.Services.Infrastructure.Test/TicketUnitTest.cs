using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinkBus.Services.Infrastructure.Test.Common;

using PinkBus.Services.Contract;
using PinkBus.Services.Entity;

namespace PinkBus.Services.Infrastructure.Test
{
    [TestClass]
    public class TicketUnitTest : BaseUnitTest
    {

        [TestMethod]
        public void QueryTicketsTest()
        {
            QueryTicketsResponse response = base.helper.client.Get(new QueryTickets
            {
                Device = "x",
                DeviceOS = "x",
                DeviceVersion = "x",
                EventKey = new Guid("C3165616-EC04-423F-A9D7-E049A01FAAC5")
            });
            Assert.IsNotNull(response);
        }
        [TestMethod]
        public void ApplyTicketTest()
        {
            ApplyTicketResponse response = base.helper.client.Get(new ApplyTicket
            {
                Device = "x",
                DeviceOS = "x",
                DeviceVersion = "x",
                SessionKey = new Guid("A7760A2C-66F5-4554-A657-4CABE6B731B3"),
                MappingKey = new Guid("1CD7FDBA-2B45-43F5-BCBE-9B7ABFEE7786")
            });
        }

        [TestMethod]
        public void QueryApplyResultTest()
        {
            QueryApplyTicketResultResponse response = base.helper.client.Get(new QueryApplyTicketResult
            {
                Device = "x",
                DeviceOS = "x",
                DeviceVersion = "x",
                ApplyTicketTrackerId = new Guid("F5618147-A830-4A80-B90A-2548A71DE6DE")
            });
        }

        [TestMethod]
        public void GetTicketDetailTest()
        {
            GetTicketDetailResponse response = base.helper.client.Get(new GetTicketDetail
            {
                Device = "x",
                DeviceOS = "x",
                DeviceVersion = "x",
                TicketKey = new Guid("9FBB5F76-9B30-4F33-961D-00066DEE6055"),
                EventKey = new Guid("C3165616-EC04-423F-A9D7-E049A01FAAC5")

            });
        }

        [TestMethod]
        public void BestowalTicketTest()
        {
            BestowalTicketResponse response = base.helper.client.Get(new BestowalTicket
            {
                Device = "x",
                DeviceOS = "x",
                DeviceVersion = "x",
                TicketKey = new Guid("9FBB5F76-9B30-4F33-961D-00066DEE6055")

            });
        }

    }
}
