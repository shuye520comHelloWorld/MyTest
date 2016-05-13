using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using iParty.Services.Host;
using ServiceStack;
using iParty.Services.Impl;
using System.Collections.Generic;
using iParty.Services.Infrastructure;
using QuartES.Services.Core;
using Moq;
using iParty.Services.Interface;
using NLog;
using iParty.Services.ORM.Operations;
using iParty.Services.Contract;
using iParty.Services.Entity;
using System.Linq;
using ServiceStack.DataAnnotations;
using System.Configuration;
using iParty.Services.ORM;

namespace iParty.Services.UnitTests
{
    [TestClass]
    public class ServiceTests
    {
        private ServiceStackHost appHost;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        [TestInitialize]
        public void TestFixtureSetUp()
        {
            var licenseKeyText = @"1643-e1JlZjoxNjQzLE5hbWU6Ik1hcnlLYXkgKENoaW5hKSBDb
3NtZXRpY3MgQ28uLEx0ZCIsVHlwZTpCdXNpbmVzcyxIYXNoOmY
xaDM4VDVSaUhNbDc5QUJQSWNEUHI1RWFoRGVLTWRldm84Z2VHd
DdFVExldk1IczZRdDNGTmw0UEdTcUF5MDhZVDlUSUlBUm1jWUN
LWnFqMzd4OFN1UkZYQ1NUOFVuL2puN1Y5eWdDSDdlUitrMGhFQ
k9nQmIvaFBhTHlOTXBGT1o3eCttN0svdk1yd3dtZ1krZUJOT3l
sYnlLUS9mNkdNemZBeHBUaWJucz0sRXhwaXJ5OjIwMTUtMDYtM
TN9
";
            Licensing.RegisterLicense(licenseKeyText);

            appHost = new ASPNetHost().Init();
            TestHelper.Initialize(appHost.Container);
            TestHelper.MockUser();
        }

        [TestMethod]
        public void PartyApplicationService_Post_BasicTest()
        {
            var service = appHost.Container.Resolve<PartyApplicationService>();
            var result = service.Post(new Contract.CreatePartyApplication
            {
                EventKey = Guid.Parse("718822B8-8B18-442C-8BE2-18C8C6F9CF52"),
                WorkshopId = 88,
                UnionConsultants = new List<long> { 20003803315, 20003740521 },
                StartDateUtc = Convert.ToDateTime("2015-09-22 13:00:00"),
                EndDateUtc = Convert.ToDateTime("2015-09-22 15:00:00"),
                CreateDateUtc = DateTime.Now
            });

            Assert.IsInstanceOfType(result, typeof(PartyApplicationCommandResponse));

            //// clean up
            //var partyOperation = new PartyOperation();
            //partyOperation.DeleteById((result as PartyApplicationCommandResponse).PartyKey);
        }

        [TestMethod]
        public void QueryPartyService__Get_Summary__Before_Applied_End_After_App_Created()
        {
            #region Create Application
            var partyApplicationService = appHost.Container.Resolve<PartyApplicationService>();
            var application = partyApplicationService.Post(new Contract.CreatePartyApplication
            {
                EventKey = Guid.Parse("718822B8-8B18-442C-8BE2-18C8C6F9CF52"),
                WorkshopId = 88,
                UnionConsultants = new List<long> { 20003803315, 20003740521 },
                StartDateUtc = Convert.ToDateTime("2015-09-22 13:00:00"),
                EndDateUtc = Convert.ToDateTime("2015-09-22 15:00:00"),
                CreateDateUtc = DateTime.Now
            });
            #endregion

            var service = appHost.Container.Resolve<QueryPartyService>();

            // the summary result on progress page
            var result = service.Get(new QueryParty
            {
            });

            #region Validate
            Assert.IsInstanceOfType(result, typeof(QueryPartyResponse));

            var resp = result as QueryPartyResponse;
            var found = false;
            foreach (var i in resp.Results)
            {
                if (i is PartyCreatedQueryResult)
                {
                    var createdResult = i as PartyCreatedQueryResult;
                    var applicationResp = application as PartyApplicationCommandResponse;
                    if (createdResult.PartyKey == applicationResp.PartyKey)
                    {
                        found = true;
                        Assert.IsTrue(createdResult.Stage == PartyStage.Created);
                        break;
                    }
                }
            }

            Assert.IsTrue(found);
            #endregion

            // clean up
            var partyOperation = new PartyOperation();
            partyOperation.DeleteById((application as PartyApplicationCommandResponse).PartyKey);
        }

        [TestMethod]
        public void QueryPartyService__Get_EventDetail__Before_Applied_End_After_App_Created()
        {
            #region Create Application
            var partyApplicationService = appHost.Container.Resolve<PartyApplicationService>();
            var application = partyApplicationService.Post(new Contract.CreatePartyApplication
            {
                EventKey = Guid.Parse("718822B8-8B18-442C-8BE2-18C8C6F9CF52"),
                WorkshopId = 88,
                UnionConsultants = new List<long> { 20003803315, 20003740521 },
                StartDateUtc = Convert.ToDateTime("2015-09-22 13:00:00"),
                EndDateUtc = Convert.ToDateTime("2015-09-22 15:00:00"),
                CreateDateUtc = DateTime.Now
            });
            #endregion

            var service = appHost.Container.Resolve<QueryPartyService>();

            // the summary result on progress page
            var result = service.Get(new QueryParty
            {
                EventKey = Guid.Parse("718822B8-8B18-442C-8BE2-18C8C6F9CF52") // specified event
            });

            // clean up
            var partyOperation = new PartyOperation();
            partyOperation.DeleteById((application as PartyApplicationCommandResponse).PartyKey);

            #region Validate
            Assert.IsInstanceOfType(result, typeof(QueryPartyResponse));

            var resp = result as QueryPartyResponse;
            var found = false;
            foreach (var i in resp.Results)
            {
                if (i is ApplicationOpenedQueryResult)
                {
                    var openForQuery = i as ApplicationOpenedQueryResult;
                    var applicationResp = application as PartyApplicationCommandResponse;
                    if (openForQuery.EventKey == applicationResp.EventKey)
                    {
                        found = true;
                        Assert.IsTrue(openForQuery.Stage == PartyStage.OpenForApplication);
                        break;
                    }
                }
            }

            Assert.IsTrue(found);
            #endregion
        }

        [TestMethod]
        public void QueryPartyService__Get_PartyDetail__Before_Applied_End_After_App_Created()
        {
            #region Create Application
            var partyApplicationService = appHost.Container.Resolve<PartyApplicationService>();
            var application = partyApplicationService.Post(new Contract.CreatePartyApplication
            {
                EventKey = Guid.Parse("718822B8-8B18-442C-8BE2-18C8C6F9CF52"),
                WorkshopId = 88,
                UnionConsultants = new List<long> { 20003803315, 20003740521 },
                StartDateUtc = Convert.ToDateTime("2015-09-22 13:00:00"),
                EndDateUtc = Convert.ToDateTime("2015-09-22 15:00:00"),
                CreateDateUtc = DateTime.Now
            });
            #endregion

            var service = appHost.Container.Resolve<QueryPartyService>();

            // the summary result on progress page
            var result = service.Get(new QueryParty
            {
                PartyKey = (application as PartyApplicationCommandResponse).PartyKey,
                PartyStage = PartyStage.Created
            });

            // clean up
            var partyOperation = new PartyOperation();
            partyOperation.DeleteById((application as PartyApplicationCommandResponse).PartyKey);

            #region Validate
            Assert.IsInstanceOfType(result, typeof(QueryPartyResponse));

            var resp = result as QueryPartyResponse;
            var found = false;
            foreach (var i in resp.Results)
            {
                if (i is PartyCreatedQueryResult)
                {
                    var openForQuery = i as PartyCreatedQueryResult;
                    var applicationResp = application as PartyApplicationCommandResponse;
                    if (openForQuery.EventKey == applicationResp.EventKey)
                    {
                        found = true;
                        Assert.IsTrue(openForQuery.Stage == PartyStage.Created);
                        break;
                    }
                }
            }

            Assert.IsTrue(found);
            #endregion
        }

        [TestMethod]
        public void QueryPartyService__Get_Summary__Before_Invit_End_After_Invit_Start()
        {
            #region Create Application
            var partyOperation = new PartyOperation();
            partyOperation.Create(new PartyEntity
            {
                PartyKey = Guid.Parse("A8BA37CC-9DB5-470E-B346-95DB7D163ADA"),
                EventKey = Guid.Parse("C5B43B1E-6A57-432E-A661-9869CDEF419D"),
                WorkshopId = 88,
                PartyType = 0,
                AppliedContactID = 20003444542,
                AppliedName = @"向,丽华",
                OrganizationType = 1,
                StartDate = DateTime.Parse("2015-09-20 00:00:00.000"),
                EndDate = DateTime.Parse("2015-09-29 00:00:00.000"),
                CreateDate = DateTime.Now,
                DisplayStartDate = DateTime.Parse("2015-09-25 10:00:00.000"),
                DisplayEndDate = DateTime.Parse("2015-09-25 18:00:00.000")
            });
            #endregion

            var service = appHost.Container.Resolve<QueryPartyService>();

            // the summary result on progress page
            var result = service.Get(new QueryParty
            {
            });

            #region Validate
            Assert.IsInstanceOfType(result, typeof(QueryPartyResponse));

            var resp = result as QueryPartyResponse;
            var found = false;
            foreach (var i in resp.Results)
            {
                if (i is PartyrInvitationQueryResult)
                {
                    var createdResult = i as PartyrInvitationQueryResult;
                    if (createdResult.PartyKey == Guid.Parse("A8BA37CC-9DB5-470E-B346-95DB7D163ADA"))
                    {
                        found = true;
                        Assert.IsTrue(createdResult.Stage == PartyStage.OpenForInvitation);
                        break;
                    }
                }
            }

            Assert.IsTrue(found);
            #endregion

            // clean up
            //var partyOperation = new PartyOperation();
            partyOperation.DeleteById("A8BA37CC-9DB5-470E-B346-95DB7D163ADA");
        }

        [TestMethod]
        public void QueryPartyService__Get_EventDetail__Before_Invit_End_After_Invit_Start()
        {
            #region Create Application
            var partyOperation = new PartyOperation();
            partyOperation.Create(new PartyEntity
            {
                PartyKey = Guid.Parse("A8BA37CC-9DB5-470E-B346-95DB7D163ADA"),
                EventKey = Guid.Parse("C5B43B1E-6A57-432E-A661-9869CDEF419D"),
                WorkshopId = 88,
                PartyType = 0,
                AppliedContactID = 20003444542,
                AppliedName = @"向,丽华",
                OrganizationType = 1,
                StartDate = DateTime.Parse("2015-09-20 00:00:00.000"),
                EndDate = DateTime.Parse("2015-09-29 00:00:00.000"),
                CreateDate = DateTime.Now,
                DisplayStartDate = DateTime.Parse("2015-09-25 10:00:00.000"),
                DisplayEndDate = DateTime.Parse("2015-09-25 18:00:00.000")
            });
            #endregion

            var service = appHost.Container.Resolve<QueryPartyService>();

            // the summary result on progress page
            var result = service.Get(new QueryParty
            {
                EventKey = Guid.Parse("C5B43B1E-6A57-432E-A661-9869CDEF419D") // specified event
            });

            // clean up
            //var partyOperation = new PartyOperation();
            partyOperation.DeleteById("A8BA37CC-9DB5-470E-B346-95DB7D163ADA");

            #region Validate
            Assert.IsInstanceOfType(result, typeof(QueryPartyResponse));

            var resp = result as QueryPartyResponse;
            var found = false;
            foreach (var i in resp.Results)
            {
                if (i is ApplicationOpenedQueryResult)
                {
                    var createdResult = i as ApplicationOpenedQueryResult;
                    if (createdResult.PartyKey == Guid.Parse("A8BA37CC-9DB5-470E-B346-95DB7D163ADA"))
                    {
                        found = true;
                        Assert.IsTrue(createdResult.Stage == PartyStage.OpenForApplication);
                        break;
                    }
                }
            }

            Assert.IsTrue(found);
            #endregion
        }

        [TestMethod]
        public void QueryPartyService__Get_PartyDetail__Before_Invit_End_After_Invit_Start()
        {
            #region Create Application
            var partyOperation = new PartyOperation();
            partyOperation.Create(new PartyEntity
            {
                PartyKey = Guid.Parse("A8BA37CC-9DB5-470E-B346-95DB7D163ADA"),
                EventKey = Guid.Parse("C5B43B1E-6A57-432E-A661-9869CDEF419D"),
                WorkshopId = 88,
                PartyType = 0,
                AppliedContactID = 20003444542,
                AppliedName = @"向,丽华",
                OrganizationType = 1,
                StartDate = DateTime.Parse("2015-09-20 00:00:00.000"),
                EndDate = DateTime.Parse("2015-09-29 00:00:00.000"),
                CreateDate = DateTime.Now,
                DisplayStartDate = DateTime.Parse("2015-09-25 10:00:00.000"),
                DisplayEndDate = DateTime.Parse("2015-09-25 18:00:00.000")
            });
            #endregion

            var service = appHost.Container.Resolve<QueryPartyService>();

            // the summary result on progress page
            var result = service.Get(new QueryParty
            {
                PartyKey = Guid.Parse("A8BA37CC-9DB5-470E-B346-95DB7D163ADA"),
                PartyStage = PartyStage.OpenForInvitation
            });

            // clean up
            //var partyOperation = new PartyOperation();
            partyOperation.DeleteById("A8BA37CC-9DB5-470E-B346-95DB7D163ADA");

            #region Validate
            Assert.IsInstanceOfType(result, typeof(QueryPartyResponse));

            var resp = result as QueryPartyResponse;
            var found = false;
            foreach (var i in resp.Results)
            {
                if (i is PartyrInvitationQueryResult)
                {
                    var createdResult = i as PartyrInvitationQueryResult;
                    if (createdResult.PartyKey == Guid.Parse("A8BA37CC-9DB5-470E-B346-95DB7D163ADA"))
                    {
                        found = true;
                        Assert.IsTrue(createdResult.Stage == PartyStage.OpenForInvitation);
                        break;
                    }
                }
            }

            Assert.IsTrue(found);
            #endregion
        }

        [TestMethod]
        public void QueryPartyService__Get_Summary__Before_App_Start_After_SignIn_Start()
        {
            #region Create Application
            var partyOperation = new PartyOperation();
            partyOperation.Create(new PartyEntity
            {
                PartyKey = Guid.Parse("E80502D2-5585-45CD-A45B-92D45E16BF80"),
                EventKey = Guid.Parse("002440FF-F416-4740-891F-BACA4338DB8D"),
                WorkshopId = 88,
                PartyType = 0,
                AppliedContactID = 20003444542,
                AppliedName = @"向,丽华",
                OrganizationType = 1,
                StartDate = DateTime.Parse("2015-09-15 00:00:00.000"),
                EndDate = DateTime.Parse("2015-09-29 00:00:00.000"),
                CreateDate = DateTime.Now,
                DisplayStartDate = DateTime.Parse("2015-09-25 10:00:00.000"),
                DisplayEndDate = DateTime.Parse("2015-09-25 18:00:00.000")
            });
            #endregion

            var service = appHost.Container.Resolve<QueryPartyService>();

            // the summary result on progress page
            var result = service.Get(new QueryParty
            {
            });

            // clean up
            //var partyOperation = new PartyOperation();
            partyOperation.DeleteById("E80502D2-5585-45CD-A45B-92D45E16BF80");

            #region Validate
            Assert.IsInstanceOfType(result, typeof(QueryPartyResponse));

            var resp = result as QueryPartyResponse;
            var found = false;
            foreach (var i in resp.Results)
            {
                if (i is PartyrSigninQueryResult)
                {
                    var createdResult = i as PartyrSigninQueryResult;
                    if (createdResult.PartyKey == Guid.Parse("E80502D2-5585-45CD-A45B-92D45E16BF80"))
                    {
                        found = true;
                        Assert.IsTrue(createdResult.Stage == PartyStage.OpenForInviteeSignIn);
                        break;
                    }
                }
            }

            Assert.IsTrue(found);
            #endregion
        }

        [TestMethod]
        public void QueryPartyService__Get_EventDetail__Before_App_Start_After_SignIn_Start()
        {
            #region Create Application
            var partyOperation = new PartyOperation();
            partyOperation.Create(new PartyEntity
            {
                PartyKey = Guid.Parse("E80502D2-5585-45CD-A45B-92D45E16BF80"),
                EventKey = Guid.Parse("002440FF-F416-4740-891F-BACA4338DB8D"),
                WorkshopId = 88,
                PartyType = 0,
                AppliedContactID = 20003444542,
                AppliedName = @"向,丽华",
                OrganizationType = 1,
                StartDate = DateTime.Parse("2015-09-15 00:00:00.000"),
                EndDate = DateTime.Parse("2015-09-29 00:00:00.000"),
                CreateDate = DateTime.Now,
                DisplayStartDate = DateTime.Parse("2015-09-25 10:00:00.000"),
                DisplayEndDate = DateTime.Parse("2015-09-25 18:00:00.000")
            });
            #endregion

            var service = appHost.Container.Resolve<QueryPartyService>();

            // the summary result on progress page
            var result = service.Get(new QueryParty
            {
                EventKey = Guid.Parse("002440FF-F416-4740-891F-BACA4338DB8D")
            });

            // clean up
            //var partyOperation = new PartyOperation();
            partyOperation.DeleteById("E80502D2-5585-45CD-A45B-92D45E16BF80");

            #region Validate
            Assert.IsInstanceOfType(result, typeof(QueryPartyResponse));

            var resp = result as QueryPartyResponse;
            var found = false;
            foreach (var i in resp.Results)
            {
                if (i is ApplicationOpenedQueryResult)
                {
                    var createdResult = i as ApplicationOpenedQueryResult;
                    if (createdResult.PartyKey == Guid.Parse("E80502D2-5585-45CD-A45B-92D45E16BF80"))
                    {
                        found = true;
                        Assert.IsTrue(createdResult.Stage == PartyStage.OpenForApplication);
                        break;
                    }
                }
            }

            Assert.IsTrue(found);
            #endregion
        }

        [TestMethod]
        public void QueryPartyService__Get_PartyDetail__Before_App_Start_After_SignIn_Start()
        {
            #region Create Application
            var partyOperation = new PartyOperation();
            partyOperation.Create(new PartyEntity
            {
                PartyKey = Guid.Parse("E80502D2-5585-45CD-A45B-92D45E16BF80"),
                EventKey = Guid.Parse("002440FF-F416-4740-891F-BACA4338DB8D"),
                WorkshopId = 88,
                PartyType = 0,
                AppliedContactID = 20003444542,
                AppliedName = @"向,丽华",
                OrganizationType = 1,
                StartDate = DateTime.Parse("2015-09-15 00:00:00.000"),
                EndDate = DateTime.Parse("2015-09-29 00:00:00.000"),
                CreateDate = DateTime.Now,
                DisplayStartDate = DateTime.Parse("2015-09-25 10:00:00.000"),
                DisplayEndDate = DateTime.Parse("2015-09-25 18:00:00.000")
            });
            #endregion

            var service = appHost.Container.Resolve<QueryPartyService>();

            // the summary result on progress page
            var result = service.Get(new QueryParty
            {
                PartyKey = Guid.Parse("E80502D2-5585-45CD-A45B-92D45E16BF80"),
                PartyStage = PartyStage.OpenForInviteeSignIn
            });

            // clean up
            //var partyOperation = new PartyOperation();
            partyOperation.DeleteById("E80502D2-5585-45CD-A45B-92D45E16BF80");

            #region Validate
            Assert.IsInstanceOfType(result, typeof(QueryPartyResponse));

            var resp = result as QueryPartyResponse;
            var found = false;
            foreach (var i in resp.Results)
            {
                if (i is PartyrSigninQueryResult)
                {
                    var createdResult = i as PartyrSigninQueryResult;
                    if (createdResult.PartyKey == Guid.Parse("E80502D2-5585-45CD-A45B-92D45E16BF80"))
                    {
                        found = true;
                        Assert.IsTrue(createdResult.Stage == PartyStage.OpenForInviteeSignIn);
                        break;
                    }
                }
            }

            Assert.IsTrue(found);
            #endregion
        }

        [TestMethod]
        public void QueryPartyService__Get_Summary__On_App_HoldOn_Day()
        {
            #region Create Application
            var partyOperation = new PartyOperation();
            partyOperation.Create(new PartyEntity
            {
                PartyKey = Guid.Parse("766A8392-D6D9-4E54-B1C5-8700465D846B"),
                EventKey = Guid.Parse("699326F1-9EFF-4099-998D-108AA72CA15A"),
                WorkshopId = 88,
                PartyType = 0,
                AppliedContactID = 20003444542,
                AppliedName = @"向,丽华",
                OrganizationType = 1,
                StartDate = DateTime.Parse("2015-09-15 00:00:00.000"),
                EndDate = DateTime.Parse("2015-09-29 00:00:00.000"),
                CreateDate = DateTime.Now,
                DisplayStartDate = DateTime.Parse("2015-09-16 10:00:00.000"),
                DisplayEndDate = DateTime.Parse("2015-09-16 12:00:00.000")
            });
            #endregion

            var service = appHost.Container.Resolve<QueryPartyService>();

            // the summary result on progress page
            var result = service.Get(new QueryParty
            {
            });

            // clean up
            //var partyOperation = new PartyOperation();
            partyOperation.DeleteById("766A8392-D6D9-4E54-B1C5-8700465D846B");

            #region Validate
            Assert.IsInstanceOfType(result, typeof(QueryPartyResponse));

            var resp = result as QueryPartyResponse;
            var found = false;
            foreach (var i in resp.Results)
            {
                if (i is PartyrSigninQueryResult)
                {
                    var createdResult = i as PartyrSigninQueryResult;
                    if (createdResult.PartyKey == Guid.Parse("766A8392-D6D9-4E54-B1C5-8700465D846B"))
                    {
                        found = true;
                        Assert.IsTrue(createdResult.Stage == PartyStage.OpenForInviteeSignIn);
                        break;
                    }
                }
            }

            Assert.IsTrue(found);
            #endregion
        }

        [TestMethod]
        public void QueryPartyService__Get_Summary__Before_SignIn_End_After_App_End()
        {
            #region Create Application
            var partyOperation = new PartyOperation();
            partyOperation.Create(new PartyEntity
            {
                PartyKey = Guid.Parse("766A8392-D6D9-4E54-B1C5-8700465D846B"),
                EventKey = Guid.Parse("699326F1-9EFF-4099-998D-108AA72CA15A"),
                WorkshopId = 88,
                PartyType = 0,
                AppliedContactID = 20003444542,
                AppliedName = @"向,丽华",
                OrganizationType = 1,
                StartDate = DateTime.Parse("2015-09-15 00:00:00.000"),
                EndDate = DateTime.Parse("2015-09-29 00:00:00.000"),
                CreateDate = DateTime.Now,
                DisplayStartDate = DateTime.Parse("2015-09-15 10:00:00.000"),
                DisplayEndDate = DateTime.Parse("2015-09-15 12:00:00.000")
            });
            #endregion

            var service = appHost.Container.Resolve<QueryPartyService>();

            // the summary result on progress page
            var result = service.Get(new QueryParty
            {
            });

            // clean up
            //var partyOperation = new PartyOperation();
            partyOperation.DeleteById("766A8392-D6D9-4E54-B1C5-8700465D846B");

            #region Validate
            Assert.IsInstanceOfType(result, typeof(QueryPartyResponse));

            var resp = result as QueryPartyResponse;
            var found = false;
            foreach (var i in resp.Results)
            {
                if (i is PartyrSigninQueryResult)
                {
                    var createdResult = i as PartyrSigninQueryResult;
                    if (createdResult.PartyKey == Guid.Parse("766A8392-D6D9-4E54-B1C5-8700465D846B"))
                    {
                        found = true;
                        Assert.IsTrue(createdResult.Stage == PartyStage.FeedbackForInvitees);
                        break;
                    }
                }
            }

            Assert.IsTrue(found);
            #endregion
        }

        [TestMethod]
        public void QueryPartyService__Get_Summary__Before_Feedback_End_After_SignIn_End()
        {
            #region Create Application
            var partyOperation = new PartyOperation();
            partyOperation.Create(new PartyEntity
            {
                PartyKey = Guid.Parse("F7DC8EE2-47B2-495B-9594-6D26403808BC"),
                EventKey = Guid.Parse("AF7C0E2A-A13E-429B-AD26-F2C321506217"),
                WorkshopId = 88,
                PartyType = 0,
                AppliedContactID = 20003444542,
                AppliedName = @"向,丽华",
                OrganizationType = 1,
                StartDate = DateTime.Parse("2015-08-20 00:00:00.000"),
                EndDate = DateTime.Parse("2015-08-29 00:00:00.000"),
                CreateDate = DateTime.Now,
                DisplayStartDate = DateTime.Parse("2015-08-23 10:00:00.000"),
                DisplayEndDate = DateTime.Parse("2015-08-23 12:00:00.000")
            });
            #endregion

            var service = appHost.Container.Resolve<QueryPartyService>();

            // the summary result on progress page
            var result = service.Get(new QueryParty
            {
                IsFinished = true // if false, program should not load the data
            });

            // clean up
            //var partyOperation = new PartyOperation();
            partyOperation.DeleteById("F7DC8EE2-47B2-495B-9594-6D26403808BC");

            #region Validate
            Assert.IsInstanceOfType(result, typeof(QueryPartyResponse));

            var resp = result as QueryPartyResponse;
            var found = false;
            foreach (var i in resp.Results)
            {
                if (i is PartyCreatedQueryResult)
                {
                    var createdResult = i as PartyCreatedQueryResult;
                    if (createdResult.PartyKey == Guid.Parse("F7DC8EE2-47B2-495B-9594-6D26403808BC"))
                    {
                        found = true;
                        Assert.IsTrue(createdResult.Stage == PartyStage.FeedbackForInvitees);
                        break;
                    }
                }
            }

            Assert.IsTrue(found);
            #endregion
        }

        [TestMethod]
        public void QueryPartyService__Get_PartyDetail__Before_Feedback_End_After_SignIn_End()
        {
            #region Create Application
            var partyOperation = new PartyOperation();
            partyOperation.Create(new PartyEntity
            {
                PartyKey = Guid.Parse("F7DC8EE2-47B2-495B-9594-6D26403808BC"),
                EventKey = Guid.Parse("AF7C0E2A-A13E-429B-AD26-F2C321506217"),
                WorkshopId = 88,
                PartyType = 0,
                AppliedContactID = 20003444542,
                AppliedName = @"向,丽华",
                OrganizationType = 1,
                StartDate = DateTime.Parse("2015-08-20 00:00:00.000"),
                EndDate = DateTime.Parse("2015-08-29 00:00:00.000"),
                CreateDate = DateTime.Now,
                DisplayStartDate = DateTime.Parse("2015-08-23 10:00:00.000"),
                DisplayEndDate = DateTime.Parse("2015-08-23 12:00:00.000")
            });
            #endregion

            var service = appHost.Container.Resolve<QueryPartyService>();

            // the summary result on progress page
            var result = service.Get(new QueryParty
            {
                PartyKey = Guid.Parse("F7DC8EE2-47B2-495B-9594-6D26403808BC"),
                IsFinished = true // if false, program should not load the data
            });

            // clean up
            //var partyOperation = new PartyOperation();
            partyOperation.DeleteById("F7DC8EE2-47B2-495B-9594-6D26403808BC");

            #region Validate
            Assert.IsInstanceOfType(result, typeof(QueryPartyResponse));

            var resp = result as QueryPartyResponse;
            var found = false;
            foreach (var i in resp.Results)
            {
                if (i is PartyCreatedQueryResult)
                {
                    var createdResult = i as PartyCreatedQueryResult;
                    if (createdResult.PartyKey == Guid.Parse("F7DC8EE2-47B2-495B-9594-6D26403808BC"))
                    {
                        found = true;
                        Assert.IsTrue(createdResult.Stage == PartyStage.FeedbackForInvitees);
                        break;
                    }
                }
            }

            Assert.IsTrue(found);
            #endregion
        }

        [TestMethod]
        public void QueryPartyService__Get_Summary__After_Feedback_End()
        {
            #region Create Application
            var partyOperation = new PartyOperation();
            partyOperation.Create(new PartyEntity
            {
                PartyKey = Guid.Parse("E0C9FBB8-DB58-4801-B1F6-65E7D3E06107"),
                EventKey = Guid.Parse("FB05D2E5-DD4B-439F-8970-997955A502B0"),
                WorkshopId = 88,
                PartyType = 0,
                AppliedContactID = 20003444542,
                AppliedName = @"向,丽华",
                OrganizationType = 1,
                StartDate = DateTime.Parse("2015-08-20 00:00:00.000"),
                EndDate = DateTime.Parse("2015-08-29 00:00:00.000"),
                CreateDate = DateTime.Now,
                DisplayStartDate = DateTime.Parse("2015-08-23 10:00:00.000"),
                DisplayEndDate = DateTime.Parse("2015-08-23 12:00:00.000")
            });
            #endregion

            var service = appHost.Container.Resolve<QueryPartyService>();

            // the summary result on progress page
            var result = service.Get(new QueryParty
            {
                IsFinished = true // if false, program should not load the data
            });

            // clean up
            //var partyOperation = new PartyOperation();
            partyOperation.DeleteById("E0C9FBB8-DB58-4801-B1F6-65E7D3E06107");

            #region Validate
            Assert.IsInstanceOfType(result, typeof(QueryPartyResponse));

            var resp = result as QueryPartyResponse;
            var found = false;
            foreach (var i in resp.Results)
            {
                if (i is PartyCreatedQueryResult)
                {
                    var createdResult = i as PartyCreatedQueryResult;
                    if (createdResult.PartyKey == Guid.Parse("E0C9FBB8-DB58-4801-B1F6-65E7D3E06107"))
                    {
                        found = true;
                        Assert.IsTrue(createdResult.Stage == PartyStage.Finished);
                        break;
                    }
                }
            }

            Assert.IsTrue(found);
            #endregion
        }

        [TestMethod]
        public void InvitationService__Post__Invitation()
        {
            #region Create Application
            var partyOperation = new PartyOperation();
            partyOperation.Create(new PartyEntity
            {
                PartyKey = Guid.Parse("A678DCC5-D76F-464F-B7CA-99796D8A9A87"),
                EventKey = Guid.Parse("C5B43B1E-6A57-432E-A661-9869CDEF419D"),
                WorkshopId = 88,
                PartyType = 0,
                AppliedContactID = 20003444542,
                AppliedName = @"向,丽华",
                OrganizationType = 1,
                StartDate = DateTime.Parse("2015-09-20 00:00:00.000"),
                EndDate = DateTime.Parse("2015-09-29 00:00:00.000"),
                CreateDate = DateTime.Now,
                DisplayStartDate = DateTime.Parse("2015-09-25 10:00:00.000"),
                DisplayEndDate = DateTime.Parse("2015-09-25 18:00:00.000")
            });
            #endregion

            var partyService = appHost.Container.Resolve<QueryPartyService>();

            // the summary result on progress page
            var query = partyService.Get(new QueryParty
            {
                PartyKey = Guid.Parse("A678DCC5-D76F-464F-B7CA-99796D8A9A87"),
                PartyStage = PartyStage.OpenForInvitation
            });

            var service = appHost.Container.Resolve<InvitationService>();
            var resp = query as QueryPartyResponse;
            List<object> customerResult = new List<object>();
            foreach (var i in resp.Results)
            {
                if (i is PartyrInvitationQueryResult)
                {
                    var createdResult = i as PartyrInvitationQueryResult;
                    if (createdResult.PartyKey == Guid.Parse("A678DCC5-D76F-464F-B7CA-99796D8A9A87"))
                    {
                        var customer = service.Post(new AcceptInvitation
                        {
                            InvitationToken = createdResult.InvitationToken,
                            Name = "test",
                            Age = AgeRange.Between25And35,
                            Career = Career.Freelancers,
                            PhoneNumber = "18516101335",
                            UnionId = null,
                            IsOnSite = false,
                            MaritalStatus = 0,
                            ReferenceBy = "20003492369"
                        });

                        customerResult.Add(customer);
                    }
                }
            }

            query = partyService.Get(new QueryParty
            {
                PartyKey = Guid.Parse("A678DCC5-D76F-464F-B7CA-99796D8A9A87"),
                PartyStage = PartyStage.OpenForInvitation
            });

            Assert.IsTrue(((query as QueryPartyResponse).Results[0] as PartyrInvitationQueryResult).PartyInviteesCount > 0);


            // clean up
            var customerOperation = new CustomerOperation();
            var invitationOperation = new InvitationOperation();
            foreach (var c in customerResult)
            {
                customerOperation.DeleteById((c as InvitationCommandResponse).CustomerKey);
                invitationOperation.DeleteById((c as InvitationCommandResponse).InvitationKey);
            }

            partyOperation.DeleteById("A678DCC5-D76F-464F-B7CA-99796D8A9A87");
        }

        [TestMethod]
        public void InvitationService__Get__Customers()
        {
            #region Create Application
            var partyOperation = new PartyOperation();
            partyOperation.Create(new PartyEntity
            {
                PartyKey = Guid.Parse("A678DCC5-D76F-464F-B7CA-99796D8A9A87"),
                EventKey = Guid.Parse("C5B43B1E-6A57-432E-A661-9869CDEF419D"),
                WorkshopId = 88,
                PartyType = 0,
                AppliedContactID = 20003444542,
                AppliedName = @"向,丽华",
                OrganizationType = 1,
                StartDate = DateTime.Parse("2015-09-20 00:00:00.000"),
                EndDate = DateTime.Parse("2015-09-29 00:00:00.000"),
                CreateDate = DateTime.Now,
                DisplayStartDate = DateTime.Parse("2015-09-25 10:00:00.000"),
                DisplayEndDate = DateTime.Parse("2015-09-25 18:00:00.000")
            });
            #endregion

            var partyService = appHost.Container.Resolve<QueryPartyService>();

            // the summary result on progress page
            var query = partyService.Get(new QueryParty
            {
                PartyKey = Guid.Parse("A678DCC5-D76F-464F-B7CA-99796D8A9A87"),
                PartyStage = PartyStage.OpenForInvitation
            });

            var service = appHost.Container.Resolve<InvitationService>();
            var resp = query as QueryPartyResponse;
            List<object> customerResult = new List<object>();
            foreach (var i in resp.Results)
            {
                if (i is PartyrInvitationQueryResult)
                {
                    var createdResult = i as PartyrInvitationQueryResult;
                    if (createdResult.PartyKey == Guid.Parse("A678DCC5-D76F-464F-B7CA-99796D8A9A87"))
                    {
                        var customer = service.Post(new AcceptInvitation
                        {
                            InvitationToken = createdResult.InvitationToken,
                            Name = "test",
                            Age = AgeRange.Between25And35,
                            Career = Career.Freelancers,
                            PhoneNumber = "18516101335",
                            UnionId = null,
                            IsOnSite = false,
                            MaritalStatus = 0,
                            ReferenceBy = "20003492369"
                        });

                        customerResult.Add(customer);
                    }
                }
            }

            var queryCustomers = service.Get(new QueryCustomer
            {
                PartyKey = Guid.Parse("A678DCC5-D76F-464F-B7CA-99796D8A9A87")
            }) as IEnumerable<QueryCustomerResponse>;

            Assert.IsTrue(queryCustomers.Count() > 0);

            // clean up
            var customerOperation = new CustomerOperation();
            var invitationOperation = new InvitationOperation();
            foreach (var c in customerResult)
            {
                customerOperation.DeleteById((c as InvitationCommandResponse).CustomerKey);
                invitationOperation.DeleteById((c as InvitationCommandResponse).InvitationKey);
            }

            partyOperation.DeleteById("A678DCC5-D76F-464F-B7CA-99796D8A9A87");
        }

        [TestMethod]
        public void Test()
        {
            var partyApplicationService = appHost.Container.Resolve<PartyApplicationService>();
            var partyResponse = partyApplicationService.Post(new Contract.CreatePartyApplication
            {
                EventKey = Guid.Parse("A20E5728-7F9D-4A33-9069-976015B7F39A"),
                WorkshopId = 197,
                UnionConsultants = new List<long> { 20003839957, 20003492369 },
                StartDateUtc = Convert.ToDateTime("2015-10-18 19:00:00.000"),
                EndDateUtc = Convert.ToDateTime("2015-10-18 20:00:00.000"),
                CreateDateUtc = DateTime.Now
            });

            //var partyApplicationService = appHost.Container.Resolve<PartyApplicationService>();
            //var application = partyApplicationService.Post(new Contract.CreatePartyApplication
            //{
            //    EventKey = Guid.Parse("B56A3F70-EE11-49F4-B13A-38DB8ECA5036"),
            //    WorkshopId = 197,
            //    UnionConsultants = new List<long> { 20003119151, 20003450333, 20003479706 },
            //    StartDateUtc = Convert.ToDateTime("2015-09-29 10:00"),
            //    EndDateUtc = Convert.ToDateTime("2015-09-29 11:00"),
            //    CreateDateUtc = DateTime.Now
            //});

            //var partyOperation = new PartyOperation();
            //partyOperation.DeleteById((application as PartyApplicationCommandResponse).PartyKey);

            //var partyKey = Guid.Parse("58FEEBC8-F8A3-4C83-B7E0-B8DFA4291BEE");

            //var queryParty = appHost.Container.Resolve<QueryPartyService>();

            //var result = queryParty.Get(new QueryParty
            //{
            //    PartyKey = partyKey,
            //    PartyStage = PartyStage.Created
            //    //PartyStage = PartyStage.Finished
            //});

            //var invitationService = appHost.Container.Resolve<InvitationService>();

            //var checkInResult = invitationService.Put(new InvitationCheckIn 
            //{
            //    InvitationKey = Guid.Parse("a77e1c8cf7ea4aa5b8b1d3a36048af9c"),
            //    CheckInType = "00"
            //});
        }

        [TestMethod]
        public void Application_BasicTest()
        {
            var today = DateTime.Now.Date;

            var eventOperation = new EventOperation();
            var eventKey = Guid.NewGuid();
            eventOperation.Create(new EventEntity
            {
                EventKey = eventKey,
                Category = 1,
                Title = "Create by Unit Tests",
                Description = "活动申请",
                ApplicationStartDate = today.AddDays(-1).Date,
                ApplicationEndDate = today.AddDays(5).Date,
                EventStartDate = today.AddDays(-1).Date,
                EventEndDate = today.AddDays(25).Date,
                PartyAllowStartDate = today.AddDays(10).Date,
                PartyAllowEndDate = today.AddDays(20).Date,
                FeedbackEndDate = today.AddDays(30),
                CreateDate = DateTime.Now
            });

            var partyApplicationService = appHost.Container.Resolve<PartyApplicationService>();
            var partyResponse = partyApplicationService.Post(new Contract.CreatePartyApplication
            {
                EventKey = eventKey,
                WorkshopId = 197,
                UnionConsultants = new List<long> { 20003839957, 20003492369 },
                StartDateUtc = today.AddDays(11).AddHours(13).AddMinutes(1),
                EndDateUtc = today.AddDays(11).AddHours(13).AddMinutes(30),
                CreateDateUtc = DateTime.Now
            });

            var partyKey = (partyResponse as PartyApplicationCommandResponse).PartyKey;
            partyApplicationService.Post(new UpdatePartyApplication
            {
                PartyKey = partyKey,
                UnionConsultants = new List<long> { 20003880347 },
                StartDateUtc = today.AddDays(11).AddHours(14),
                EndDateUtc = today.AddDays(11).AddHours(16),
            });

            partyApplicationService.Delete(new DeletePartyApplication
            {
                PartyKey = partyKey
            });

            eventOperation.DeleteByFunc(e => e.EventKey == eventKey);
        }

        [TestMethod]
        public void Application_Two_Invitates_The_Same_Unitee()
        {
            var today = DateTime.Now.Date;

            //var eventOperation = new EventOperation();
            //var eventKey = Guid.NewGuid();
            //eventOperation.Create(new EventEntity
            //{
            //    EventKey = eventKey,
            //    Category = 1,
            //    Title = "Create by Unit Tests",
            //    Description = "活动申请",
            //    ApplicationStartDate = today.AddDays(-1).Date,
            //    ApplicationEndDate = today.AddDays(5).Date,
            //    EventStartDate = today.AddDays(-1).Date,
            //    EventEndDate = today.AddDays(25).Date,
            //    PartyAllowStartDate = today.AddDays(10).Date,
            //    PartyAllowEndDate = today.AddDays(20).Date,
            //    FeedbackEndDate = today.AddDays(30),
            //    CreateDate = DateTime.Now
            //});

            var partyApplicationService = appHost.Container.Resolve<PartyApplicationService>();
            //var partyResponse1 = partyApplicationService.Post(new Contract.CreatePartyApplication
            //{
            //    EventKey = eventKey,
            //    WorkshopId = 197,
            //    UnionConsultants = new List<long> { 20003839957 },
            //    StartDateUtc = today.AddDays(11).AddHours(13).AddMinutes(1),
            //    EndDateUtc = today.AddDays(11).AddHours(13).AddMinutes(30),
            //    CreateDateUtc = DateTime.Now
            //});

            //var partyApplicationService2 = appHost.Container.Resolve<PartyApplicationService>();

            var partyResponse2 = partyApplicationService.Post(new Contract.CreatePartyApplication
            {
                EventKey = Guid.Parse("2352f7cd-fad5-4373-ade6-63068acbe62d"),
                WorkshopId = 197,
                UnionConsultants = new List<long> { 20003839957 },
                StartDateUtc = today.AddDays(12).AddHours(15).AddMinutes(1),
                EndDateUtc = today.AddDays(12).AddHours(15).AddMinutes(30),
                CreateDateUtc = DateTime.Now
            });

            //var partyKey1 = (partyResponse1 as PartyApplicationCommandResponse).PartyKey;
            //partyApplicationService.Post(new UpdatePartyApplication
            //{
            //    PartyKey = partyKey1,
            //    UnionConsultants = new List<long> { 20003880347 },
            //    StartDateUtc = today.AddDays(11).AddHours(14),
            //    EndDateUtc = today.AddDays(11).AddHours(16),
            //});

            //partyApplicationService.Delete(new DeletePartyApplication
            //{
            //    PartyKey = partyKey1
            //});

            var partyKey2 = (partyResponse2 as PartyApplicationCommandResponse).PartyKey;
            partyApplicationService.Delete(new DeletePartyApplication
            {
                PartyKey = partyKey2
            });

            //eventOperation.DeleteByFunc(e => e.EventKey == eventKey);
        }

        [TestMethod]
        public void Signin_BasicTest()
        {
            var today = DateTime.Now.Date;

            var eventOperation = new EventOperation();
            var eventKey = Guid.NewGuid();
            eventOperation.Create(new EventEntity
            {
                EventKey = eventKey,
                Category = 1,
                Title = "Create by Unit Tests",
                Description = "活动签到",
                ApplicationStartDate = today.AddDays(-16).Date, // 2015-09-12
                ApplicationEndDate = today.AddDays(-7).Date, // 2015-09-21
                EventStartDate = today.AddDays(-16).Date, // 2015-09-12
                EventEndDate = today.AddDays(19).Date, // 2015-10-17
                PartyAllowStartDate = today.AddDays(-1).Date, // 2015-09-27
                PartyAllowEndDate = today.AddDays(4).Date, // 2015-10-02
                FeedbackEndDate = today.AddDays(10), // 2015-10-08
                CreateDate = DateTime.Now
            });

            var partyOperation = new PartyOperation();
            var partyKey = Guid.NewGuid();
            partyOperation.Create(new PartyEntity
            {
                PartyKey = partyKey,
                EventKey = eventKey,
                WorkshopId = 197,
                PartyType = 0,
                AppliedContactID = 20003605258,
                AppliedName = @"弓,喜英",
                OrganizationType = 1,
                StartDate = today.AddDays(-1).Date,
                EndDate = today.AddDays(4).Date,
                CreateDate = DateTime.Now,
                DisplayStartDate = today.Date.AddHours(10),
                DisplayEndDate = today.Date.AddHours(23)
            });

            // the summary result on progress page
            var partyService = appHost.Container.Resolve<QueryPartyService>();
            var invitationService = appHost.Container.Resolve<InvitationService>();

            var query = partyService.Get(new QueryParty
            {
                PartyKey = partyKey,
                PartyStage = PartyStage.OpenForInvitation
            });

            var resp = query as QueryPartyResponse;
            List<object> customerResult = new List<object>();
            foreach (var i in resp.Results)
            {
                if (i is PartyrInvitationQueryResult)
                {
                    var createdResult = i as PartyrInvitationQueryResult;
                    if (createdResult.PartyKey == partyKey)
                    {
                        var customer = invitationService.Post(new AcceptInvitation
                        {
                            InvitationToken = createdResult.InvitationToken,
                            Name = "测试",
                            Age = AgeRange.Between25And35,
                            Career = Career.Freelancers,
                            PhoneNumber = "18516101335",
                            UnionId = null,
                            IsOnSite = false,
                            MaritalStatus = 0,
                            ReferenceBy = "20003492369"
                        });

                        customerResult.Add(customer);

                        invitationService.Put(new InvitationCheckIn
                        {
                            InvitationKey = (customer as InvitationCommandResponse).InvitationKey,
                            CheckInType = CheckInType.Intouch,
                            CheckInDate = DateTime.Now
                        });

                        var customer2 = invitationService.Post(new AcceptInvitation
                        {
                            InvitationToken = createdResult.InvitationToken,
                            Name = "任卫莉",
                            Age = AgeRange.Between25And35,
                            Career = Career.Freelancers,
                            PhoneNumber = "13991562226",
                            UnionId = "001574",
                            IsOnSite = false,
                            MaritalStatus = 0,
                            ReferenceBy = "20003492369"
                        });

                        invitationService.Delete(new CancelInvitation 
                        {
                            InvitationKey = (customer2 as InvitationCommandResponse).InvitationKey
                        });

                        customer2 = invitationService.Post(new AcceptInvitation
                        {
                            InvitationToken = createdResult.InvitationToken,
                            Name = "任卫莉",
                            Age = AgeRange.Between25And35,
                            Career = Career.Freelancers,
                            PhoneNumber = "13991562226",
                            UnionId = "001574",
                            IsOnSite = false,
                            MaritalStatus = 0,
                            ReferenceBy = "20003492369"
                        });

                        customerResult.Add(customer2);

                        invitationService.Put(new InvitationCheckIn
                        {
                            InvitationKey = (customer2 as InvitationCommandResponse).InvitationKey,
                            CheckInType = CheckInType.Intouch,
                            CheckInDate = DateTime.Now
                        });
                    }
                }
            }

            var consultantOperation = new ConsultantTestOperation();
            consultantOperation.Create(new ConsultantTestEntity
            {
                ContactID = 29999999999,
                ConsultantID = "100000000",
                ConsultantSuffix = "CN",
                SubsidiaryID = 19,
                Level = 5,
                StartDate = DateTime.Parse("1901-01-01"),
                LastName = "蔚浩",
                FirstName = "俞",
                ConsultantStatus = "A1",
                UnitID = "001574"
            });

            var phoneOperation = new PhoneNumberOperation();
            phoneOperation.Create(new PhoneNumberEntity
            {
                PhoneNumberID = 1,
                ContactID = 29999999999,
                PhoneNumberTypeID = 5,
                PhoneNumber = "18516101335",
                IsPrimary = true
            });

            var consultantToDirOperation = new ConsultantToDirectorTestOperation();
            consultantToDirOperation.Create(new ConsultantToDirectorEntity 
            {
                Consultant = 29999999999,
                Director = 20003605258
            });

            var orderOperation = new OrderTestOperation();
            var orderID = Guid.NewGuid();
            orderOperation.Create(new OrderEntity
            {
                OrderID = orderID,
                PurchaserContactID = 20004666582,
                PurchaserCareerLevelID = 15,
                TotalCost = 200,
                OrderDate = DateTime.Now,
                InsertBy = "Service Test"
            });

            var promotedCustomer = consultantOperation.GetDataByFunc(c => c.ContactID == 20004666582).First();
            var olderlevel = promotedCustomer.Level;
            promotedCustomer.Level = 20;
            consultantOperation.Update(promotedCustomer);

            query = partyService.Get(new QueryParty
            {
                PartyKey = partyKey,
                PartyStage = PartyStage.OpenForInvitation
            });

            promotedCustomer.Level = olderlevel;
            consultantOperation.Update(promotedCustomer);

            orderOperation.DeleteByFunc(o => o.OrderID == orderID);

            consultantToDirOperation.DeleteByFunc(ctd => ctd.Consultant == 29999999999);
            phoneOperation.DeleteByFunc(p => p.PhoneNumberID == 1);
            consultantOperation.DeleteByFunc(c => c.ContactID == 29999999999);

            // clean up
            var customerOperation = new CustomerOperation();
            var invitationOperation = new InvitationOperation();
            foreach (var c in customerResult)
            {
                customerOperation.DeleteById((c as InvitationCommandResponse).CustomerKey);
                invitationOperation.DeleteById((c as InvitationCommandResponse).InvitationKey);
            }

            partyOperation.DeleteByFunc(p => p.PartyKey == partyKey);

            eventOperation.DeleteByFunc(e => e.EventKey == eventKey);
        }

        [TestCleanup]
        public void TestFixtureTearDown()
        {
            appHost.Dispose();
        }
    }

    public class ConsultantTestOperation : BaseEntityOperation<ConsultantTestEntity>
    {
        public ConsultantTestOperation() : this(ConfigurationManager.AppSettings["ContactsLite"]) { }
        public ConsultantTestOperation(string dbStr) : base(dbStr) { }
    }

    [Alias("Consultants")]
    public class ConsultantTestEntity
    {
        public long ContactID { get; set; }
        public string ConsultantID { get; set; }
        public string ConsultantSuffix { get; set; }
        public int SubsidiaryID { get; set; }
        [Alias("ConsultantLevelID")]
        public int Level { get; set; }
        public DateTime StartDate { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string UnitID { get; set; }
        public string ConsultantStatus { get; set; }
    }

    public class ConsultantToDirectorTestOperation : BaseEntityOperation<ConsultantToDirectorEntity>
    {
        public ConsultantToDirectorTestOperation() : this(ConfigurationManager.AppSettings["ContactsLite"]) { }
        public ConsultantToDirectorTestOperation(string dbStr) : base(dbStr) { }
    }

    [Alias("Consultant_To_Director")]
    public class ConsultantToDirectorEntity
    {
        public long Consultant { get; set; }
        public long Director { get; set; }
    }

    public class OrderTestOperation : BaseEntityOperation<OrderEntity>
    {
        public OrderTestOperation() : this(ConfigurationManager.AppSettings["Community"]) { }
        public OrderTestOperation(string dbStr) : base(dbStr) { }
    }

    [Alias("btc_OrderFromOE")]
    public class OrderEntity
    {
        public Guid OrderID { get; set; }
        public long PurchaserContactID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalCost { get; set; }
        public int PurchaserCareerLevelID { get; set; }
        public string InsertBy { get; set; }
    }
}
