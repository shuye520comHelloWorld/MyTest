using ServiceStack;
using ServiceStack.MiniProfiler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace IParty.Services.Internal.Host
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            var licenseKeyText = @"1643-e1JlZjoxNjQzLE5hbWU6Ik1hcnlLYXkgKENoaW5hKSBDb
3NtZXRpY3MgQ28uLEx0ZCIsVHlwZTpCdXNpbmVzcyxIYXNoOmY
xaDM4VDVSaUhNbDc5QUJQSWNEUHI1RWFoRGVLTWRldm84Z2VHd
DdFVExldk1IczZRdDNGTmw0UEdTcUF5MDhZVDlUSUlBUm1jWUN
LWnFqMzd4OFN1UkZYQ1NUOFVuL2puN1Y5eWdDSDdlUitrMGhFQ
k9nQmIvaFBhTHlOTXBGT1o3eCttN0svdk1yd3dtZ1krZUJOT3l
sYnlLUS9mNkdNemZBeHBUaWJucz0sRXhwaXJ5OjIwMTUtMDYtM
TN9";
            Licensing.RegisterLicense(licenseKeyText);
            new IPartyInternalHost().Init();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
#if DEBUG
            if (Request.IsLocal)
            {
                Profiler.Start();
            }
#endif
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
#if DEBUG
            if (Request.IsLocal)
            {
                Profiler.Stop();
            }
#endif
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}