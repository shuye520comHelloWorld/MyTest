
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.Services.Infrastructure.Test.Common
{
    public class Helper
    {
        public JsonServiceClient client { get; set; }

        public HostType hostType { get; set; }

        public EnvType envType { get; set; }

        public bool enableOAuth { get; set; }

        public static string ILMKDBStr = ConfigurationManager.AppSettings["APIURL"];

        public enum EnvType
        {
            local,
            dev,
            stag,
            prod,
            uat,
            qa
        }

        public enum HostType
        {
            ILMK,
            MobileCore,
            InviteGuestH5,
            InviteGuestIntouch,
            Pinkbus
        }

        public Helper()
        {
            string serverUrl = GenerateServerUrl(hostType, envType);
            client = new JsonServiceClient(serverUrl);
            if (enableOAuth)
            {
                client.Headers.Add(HttpHeaders.Authorization, GetToken());
            }
        }

        public Helper(bool enableOAuth, EnvType envType, HostType hostType)
        {
            string serverUrl = GenerateServerUrl(hostType, envType);
            client = new JsonServiceClient(serverUrl);
            if (enableOAuth)
            {
                client.Headers.Add(HttpHeaders.Authorization, GetToken());
            }
        }

        /// <summary>
        /// generate the test server url
        /// </summary>
        /// <param name="hostype"></param>
        /// <param name="envType"></param>
        /// <returns></returns>
        public string GenerateServerUrl(HostType hostype, EnvType envType)
        {
            var preUrl = string.Empty;
            var hostName = string.Empty;
            var serverUrl = string.Empty;
            if (envType == EnvType.local)
            {
                switch (hostype)
                {
                    case HostType.ILMK: { serverUrl = "http://localhost:59842/v1"; } break;
                    case HostType.MobileCore: { serverUrl = "http://localhost:1321/v1"; } break;
                    case HostType.InviteGuestH5: { serverUrl = "http://localhost:57967/v1"; } break;
                    case HostType.InviteGuestIntouch: { serverUrl = "http://localhost:17336/v1"; } break;
                    case HostType.Pinkbus: { serverUrl = "http://localhost:56508/v1"; } break;
                }
            }
            else
            {
                switch (hostype)
                {
                    case HostType.ILMK: { hostName = "/ILMK/v1"; } break;
                    case HostType.MobileCore: { hostName = "/Mobilecore/v1"; } break;
                    case HostType.InviteGuestH5: { hostName = "/InviteGuest.h5/v1"; } break;
                    case HostType.InviteGuestIntouch: { hostName = "/InviteGuest.Intouch/v1"; } break;
                    case HostType.Pinkbus: { hostName = "/PinkBusIntouchService/v1"; } break;
                }
                switch (envType)
                {
                    case EnvType.dev: { preUrl = "http://dev.api.marykay.com.cn"; } break;
                    case EnvType.qa: { preUrl = "http://qa.api.marykay.com.cn"; } break;
                    case EnvType.uat: { preUrl = "https://uat.api.marykay.com.cn"; } break;
                    case EnvType.prod: { preUrl = "https://api.marykay.com.cn"; } break;
                }
                serverUrl = preUrl + hostName;
            }
            return serverUrl;
        }

        public string GetToken()
        {
            string result = ConfigurationManager.AppSettings["access_token"].ToString(); ;
            result = "Bearer " + result;
            return result;
        }
    }
}
