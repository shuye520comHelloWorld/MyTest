using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using ServiceStack.Web;
using PinkBus.Services.Common;
using PinkBus.Services.Contract;


namespace PinkBus.Services.OAuth.Attribute
{
    public class OAuthRequest : RequestFilterAttribute
    {
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
        public string Device { get; private set; }
        public string DeviceOS { get; private set; }
        public string DeviceVersion { get; private set; }
        public string UnionID { get; private set; }

        private ProjectType projectType;
        public OAuthRequest(ProjectType projectType)
        {
            this.projectType = projectType;
        }
        public void Initialize(IRequest req, IResponse res, object dto)
        {

            var enableOAuthATValidation = GlobalAppSettings.EnableOAuthATValidation;

            if (enableOAuthATValidation == "true")
            {
                _logger.Debug("OAuth validation enabled...");
                var oAuthToken = req.TryResolve<IOAuth>().AcquireToken(req, res);
                this.ClientIdentity = oAuthToken.Token.Length > 50 ? oAuthToken.Token.Substring(0, 50) : oAuthToken.Token;
                var ContactID = GetPropertyValue(oAuthToken, "contact");
                if (!string.IsNullOrEmpty(ContactID)) this.ConsultantContactId = long.Parse(ContactID);
                this.UnionID = GetPropertyValue(oAuthToken, "wechat");
                _logger.Debug("OAuth validation succeeded...");
                var baseDto = dto as BaseRequestDto;
                if (baseDto == null) throw new ArgumentException("DTO should not be null.");
                _logger.Debug("Extracting client characteristics...");
                this.SubsidiaryCode = baseDto._SubsidiaryCode;
                this.ClientKey = baseDto._ClientKey;
                this.UserName = baseDto._UserName;
                this.Culture = baseDto._Culture;
                this.UICulture = baseDto._UICulture;
                this.Timezone = baseDto._Timezone;
                this.Device = baseDto.Device;
                this.DeviceOS = baseDto.DeviceOS;
                this.DeviceVersion = baseDto.DeviceVersion;
                baseDto._UserId = this.ConsultantContactId;
                baseDto.UnionID = this.UnionID;

                if (this.projectType == ProjectType.Intouch)
                {
                    if (baseDto._UserId == 0)
                    {
                        var ErrorMessage = "OAuthRequest :ContactID should not be null in Intouch api. the AccessToken is: " + oAuthToken.Token;
                        if (oAuthToken.ResponseCode != "100013" && oAuthToken.ResponseCode != "100010")
                        {
                            LogHelper.Error(ErrorMessage);
                        }
                        throw new ArgumentException(ErrorMessage);
                    }
                }
                //else if (this.projectType == ProjectType.H5)
                //{
                //    if (string.IsNullOrEmpty(baseDto.UnionID))
                //    {
                //        var ErrorMessage = "OAuthRequest :UnionID should not be null in H5 api. the AccessToken is: " + oAuthToken.Token;
                //        if (oAuthToken.ResponseCode != "100013" && oAuthToken.ResponseCode != "100010")
                //        {
                //            LogHelper.Error(ErrorMessage);
                //        }
                //        throw new ArgumentException(ErrorMessage);
                //    }
                //}       
            }
            else
            {
                _logger.Debug("OAuth validation disabled...");
                this.ClientIdentity = "AccessTokenString";
                var testConsultantContactId = req.GetParam("_ConsultantContactId");
                if (!string.IsNullOrWhiteSpace(testConsultantContactId))
                {
                    _logger.Debug("Got test ConsultantContactId {0} from request parameter...", testConsultantContactId);
                    this.ConsultantContactId = long.Parse(testConsultantContactId);
                }
                else
                {
                    _logger.Debug("Got default ConsultantContactId 1");
                    this.ConsultantContactId = 1;
                }
                _logger.Debug("Applying default client characteristics...");
                this.SubsidiaryCode = "CN";
                this.ClientKey = "SomeApp";
                this.UserName = "ILMK";
                this.Culture = "zh-CN";
                this.UICulture = "zh";
                this.Timezone = "+0800";
                var baseDto = dto as BaseRequestDto;
                if (baseDto == null) throw new ArgumentException("DTO should not be null.");
                //baseDto._UserId = 100;
                //baseDto._UserId = 20002864080;
                baseDto.UnionID = "oaWQ6uBQK7T8Mf0_R9jLVoAp9M6c";
            }
        }


        private string GetPropertyValue(OAuthToken oauthData,string PropertyName) 
        {
            if (oauthData.User!=null && oauthData.User.Count > 0) 
            {
                foreach (var d in oauthData.User) 
                {
                    if (d.AccountType == PropertyName) 
                    {
                        return d.AccountID;
                    }
                }
            }
            return string.Empty;          
        }

        public override void Execute(IRequest req, IResponse res, object requestDto)
        {
            Initialize(req, res, requestDto);
        }
    }

    public enum ProjectType
    {
        None,
        Intouch,
        H5
    }
}
