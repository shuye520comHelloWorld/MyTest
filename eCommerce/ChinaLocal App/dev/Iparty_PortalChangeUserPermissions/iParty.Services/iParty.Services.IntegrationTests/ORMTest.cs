using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using iParty.Services.ORM.Operations;
using ServiceStack.OrmLite;
using System.Data;
using iParty.Services.Infrastructure;
using iParty.Services.Contract;
using iParty.Services.Interface;

namespace iParty.Services.IntegrationTests
{
    [TestClass]
    public class ORMTest
    {
        [TestMethod]
        public void GetSqlTest()
        {
            InvitationOperation io = new InvitationOperation();
            using (IDbConnection dbConn = io.dbFactory.OpenDbConnection())
            {
                dbConn.CreateTable<InvitationDataEntity>();
            }
            //var list = eo.GetNotAppliedEventsSql();
            //Console.WriteLine(list.ToSelectStatement());
        }

        [TestMethod]
        public void GetEntityListTest()
        {
            EventOperation eo = new EventOperation();
            var list = eo.LeftJoin<EventEntity, PartyEntity, PartyEntity>((e, p) => e.EventKey == p.EventKey, p => p.EventKey != null);
            foreach (EventEntity ce in list)
            {
                Console.WriteLine(string.Format("{0}-{1}-{2}-{3}-{4}", ce.EventKey, ce.Title, ce.Description, ce.Category, ce.EventStartDate.ToString("yyyy-MM-dd")));
            }
            Console.WriteLine(System.Web.HttpUtility.UrlEncode("1j9/dUZkCJA8fLhL+G6ZAbrUT1KVctTX98iWgtnz7qAJgEg/KixUmYo5FAwKts/AOJBlnIim6tf6SCpUqP025XHttGPSFX/y"));
        }

        [TestMethod]
        public void InvitationTokenTest()
        {
            //var party = new PartyOperation();
            //var resp = party.GetSingleDataByFunc(p => p.PartyKey == Guid.Parse("E8C9EA1D-5FE0-4B6E-A014-3D3DB59D63A6"));
            IThreeDES des = new ThreeDES();
            string original = "a357a41e-9628-4030-8792-e34af94752c3|20002875680|20002875680|1132|015455";//string.Format("{0}|{1}|{2}", resp.PartyKey, resp.AppliedContactID, resp.WorkshopId);
            string ciphertext = des.Encrypt(original);
            Console.WriteLine(original);
            Console.WriteLine(string.Format("{0}\t{1}", ciphertext, ciphertext.Length));
            Console.WriteLine(des.Decrypt(@"CESXJGsBnW0W5tz8XrlZEh73I1xXxYhO8HPN3eY3+Sp5pGdFfzo/REzXtTvmFeuygs+YrA6AAxp7k+Gw5SG6xJ58U1YN5yKUFPqbRKwcWds="));
        }

        [TestMethod]
        public void HttpRequestTest()
        {
            var http = new HttpWebRequestHandler();
            var result = http.HttpGet<WechartToken>("http://webservices.chndmz.com/highendapi/v1/wxoauth/accesstoken");
            Console.WriteLine(result.OAuthData);
        }
    }
}
