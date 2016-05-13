using Microsoft.VisualStudio.TestTools.UnitTesting;
using iParty.Services.Contract;
using iParty.Services.Entity;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iParty.Services.IntegrationTests
{
    [TestClass]
    public class TestQueryPartyService
    {
        public TestQueryPartyService()
        {
        }

        [TestMethod]
        public void TestQueryOpenningParty()
        {
            var response = Helper.Client.Get(
                new QueryParty
                {
                });

            Assert.AreEqual(1, response.Results.Count);
            var partyEvent = response.Results[0] as ApplicationOpenedQueryResult;

            Assert.IsTrue(partyEvent.EventKey.HasValue);
            Assert.IsFalse(partyEvent.PartyKey.HasValue);

            Assert.AreEqual("亮采产品推荐活动", partyEvent.Title);
            Assert.AreEqual(PartyCategory.Love, partyEvent.Category);
            Assert.AreEqual(PartyStage.OpenForApplication, partyEvent.Stage);
            Assert.AreEqual("", partyEvent.ApplicationStartDate);
            Assert.AreEqual("", partyEvent.ApplicationEndDate);
        }
    }
}
