using iParty.Services.Contract;
using iParty.Services.Interface;
using NLog;
using QuartES.Services.Common;
using ServiceStack.Web;
using System;
using System.Collections.Generic;

namespace iParty.Services.Infrastructure
{
    public class Context : IServiceStackContext
    {
        public Context() { }

        readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public Guid? CommandId { get; private set; }
        public string ClientIdentity { get; private set; }
        public long ConsultantContactId { get; private set; }
        public string ClientKey { get; private set; }
        public string SubsidiaryCode { get; private set; }
        public string Timezone { get; private set; }
        public string Culture { get; private set; }
        public string UICulture { get; private set; }
        public string UserName { get; private set; }
        public IUser User { get; private set; }

        public void Initialize(IRequest req, IResponse res, object dto)
        {
            if (IsDevelopment())
            {
                EnforceDevelopmentContext(req);
            }
            else
            {
                var hasNoTokenString = !req.TryResolve<IOAuth>().HasTokenString(req);
                if (IsAllowedPublicEndPoints(req))
                {
                    if (hasNoTokenString)
                    {
                        ParsePublicContext(req);
                    }
                    else
                    {
                        ParseOAuthContext(req, res);
                    }
                }
                else
                {
                    ParseOAuthContext(req, res);
                }

                ApplyClientSignature(req);
            }
        }

        private static bool IsDevelopment()
        {
            return bool.Parse(ConfigHelper.GetLocalizedAppSetting("EnforceDevelopmentContext"));
        }

        private static bool IsAllowedPublicEndPoints(IRequest req)
        {
            return req.Dto as IPublicRequestDto != null;
        }

        private void ParsePublicContext(IRequest req)
        {
            var userProvider = req.TryResolve<IUserProvider>();
            this.User = userProvider.GetCurrentUser(null);
            this.ConsultantContactId = -1L;
            this.ClientIdentity = req.RemoteIp;
        }

        private void ParseOAuthContext(IRequest req, IResponse res)
        {
            _logger.Debug("OAuth validation enabled...");
            var oAuthToken = req.TryResolve<IOAuth>().AcquireToken(req, res);
            this.ClientIdentity = oAuthToken.Token.Length > 50 ? oAuthToken.Token.Substring(0, 50) : oAuthToken.Token;
            this.ConsultantContactId = oAuthToken.User.Value;
            var userProvider = req.TryResolve<IUserProvider>();
            this.User = userProvider.GetCurrentUser(this.ConsultantContactId);
            _logger.Debug("OAuth validation succeeded...");
        }

        private void ApplyClientSignature(IRequest req)
        {
            var baseDto = req.Dto as BaseRequestDto;
            if (baseDto == null) throw new ArgumentException("DTO should not be null.");
            if (!string.IsNullOrWhiteSpace(baseDto._ClientIdentity))
            {
                this.ClientIdentity = baseDto._ClientIdentity;
            }
            if (!string.IsNullOrWhiteSpace(baseDto._SubsidiaryCode))
            {
                this.SubsidiaryCode = baseDto._SubsidiaryCode;
            }
            if (!string.IsNullOrWhiteSpace(baseDto._ClientKey))
            {
                this.ClientKey = baseDto._ClientKey;
            }
            if (!string.IsNullOrWhiteSpace(baseDto._UserName))
            {
                this.UserName = baseDto._UserName;
            }
            if (!string.IsNullOrWhiteSpace(baseDto._Culture))
            {
                this.Culture = baseDto._Culture;
            }
            if (!string.IsNullOrWhiteSpace(baseDto._UICulture))
            {
                this.UICulture = baseDto._UICulture;
            }
            if (!string.IsNullOrWhiteSpace(baseDto._Timezone))
            {
                this.Timezone = baseDto._Timezone;
            }
        }

        private void EnforceDevelopmentContext(IRequest req)
        {
            var testConsultantContactId = req.QueryString["_ConsultantContactId"];
            if (!string.IsNullOrWhiteSpace(testConsultantContactId))
            {
                this.ConsultantContactId = long.Parse(testConsultantContactId);
            }
            else
            {
                this.ConsultantContactId = 1;
            }
            this.User = new User(this.ConsultantContactId, 60, "9000");
            this.ClientIdentity = "AccessToken";
            this.SubsidiaryCode = "CN";
            this.ClientKey = "MobileApp";
            this.UserName = "iPartyService";
            this.Culture = "zh-CN";
            this.UICulture = "zh";
            this.Timezone = "+0800";
        }

    
    }
}