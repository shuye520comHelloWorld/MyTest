using iParty.Services.Contract;
using iParty.Services.Interface;
using iParty.Services.Interface.Exception;
using iParty.Services.ORM.Operations;
using QuartES.Services.Core;
using ServiceStack;
using System;
using System.Configuration;
using System.Net;

namespace iParty.Services.Infrastructure
{
    public sealed class PartyRepository : IPartyRepository
    {
        private const string tokenFormat = "{0}|{1}|{2}|{3}|{4}";
        private PartyOperation partyOperation;
        private IThreeDES threeDes;
        private IContext _context;
        public PartyRepository(IThreeDES des, IContext context)
        {
            threeDes = des;
            _context = context;
            partyOperation = new PartyOperation();
        }
        public object GenerateToken(InvitationTokenRequest req)
        {
            PartyEntity party = partyOperation.GetSingleData("PartyKey", req.PartyKey);
            if (party == null)
            {
                throw new HttpError(HttpStatusCode.NotFound, "ResourceError", ConfigurationManager.AppSettings["PartyNotExistsError"]);
            }
            string ciphertext = GenerateToken(req.PartyKey, party.AppliedContactID, party.WorkshopId);
            return new InvitationTokenResponse
            {
                Token = ciphertext
            };
        }

        public InvitationTokenEntity GetTokenEntity(string token)
        {
            string original = threeDes.Decrypt(token);
            string[] formatted = original.Split('|');
            if (formatted.Length != 5)
            {
                throw new HttpError(HttpStatusCode.BadRequest, "ParameterError", ConfigurationManager.AppSettings["InvalidTokenError"]);
            }
            InvitationTokenEntity entity = new InvitationTokenEntity();
            Guid partyKey;
            long contactID;
            long currentUser;
            int workshopID;
            if (Guid.TryParse(formatted[0], out partyKey))
            {
                entity.PartyKey = partyKey;
            }
            if (long.TryParse(formatted[1], out contactID))
            {
                entity.AppliedContactID = contactID;
            }
            if (long.TryParse(formatted[2], out currentUser))
            {
                entity.CurrentUserContactID = currentUser;
            }
            if (int.TryParse(formatted[3], out workshopID))
            {
                entity.WorkshopId = workshopID;
            }
            entity.UnitID = formatted[4];
            if (entity.PartyKey == null || entity.AppliedContactID == null || entity.CurrentUserContactID == null || entity.WorkshopId == null)
            {
                throw new HttpError(HttpStatusCode.BadRequest, "ParameterError", ConfigurationManager.AppSettings["InvalidTokenError"]);
            }
            return entity;
        }


        public string GenerateToken(Guid partyKey, long hostContactID, int workshopID)
        {
            string original = string.Format(tokenFormat, partyKey, hostContactID, _context.ConsultantContactId, workshopID, _context.User.Unit);
            string ciphertext = threeDes.Encrypt(original);
            return ciphertext;
        }
    }
}
