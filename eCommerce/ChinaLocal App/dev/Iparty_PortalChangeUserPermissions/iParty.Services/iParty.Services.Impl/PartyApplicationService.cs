using iParty.Services.Domain;
using iParty.Services.Interface;
using iParty.Services.Interface.Exception;
using iParty.Services.Contract;
using iParty.Services.Entity;
using NLog;
using QuartES.Services.Core;
using ServiceStack;
using System.Collections.Generic;
using System;
using System.Configuration;
using System.Net;

namespace iParty.Services.Impl
{
    public sealed class PartyApplicationService : Service
    {
        IPartyApplicationRepository _repository;
        IContext _context;
        Logger _logger = LogManager.GetCurrentClassLogger();

        public PartyApplicationService(IPartyApplicationRepository repository, IContext context)
        {
            _repository = repository;
            _context = context;
        }
        /// <summary>
        /// create a party application
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public object Post(CreatePartyApplication dto)
        {
            if (dto.WorkshopId == 0)
            {
                throw new HttpError(HttpStatusCode.BadRequest, "ParameterError", ConfigurationManager.AppSettings["WorkshopEmptyError"]);
            }
            if (dto.StartDateUtc == DateTime.MinValue || dto.EndDateUtc == DateTime.MinValue)
            {
                throw new HttpError(HttpStatusCode.BadRequest, "ParameterError", ConfigurationManager.AppSettings["PartyDateEmptyError"]);
            }
            if (dto.EndDateUtc < dto.StartDateUtc)
            {
                throw new HttpError(HttpStatusCode.BadRequest, "ParameterError", ConfigurationManager.AppSettings["InvalidPartyDateError"]); 
            }

            if (string.Compare(dto.StartDateUtc.ToString("yyyy-MM-dd"), dto.EndDateUtc.ToString("yyyy-MM-dd"), true) != 0)
            {
                throw new HttpError(HttpStatusCode.BadRequest, "ParameterError", ConfigurationManager.AppSettings["PartyDateOneDayError"]); 
            }
            var span = dto.EndDateUtc - dto.StartDateUtc;
            if (span.TotalMinutes < 1)
            {
                throw new HttpError(HttpStatusCode.BadRequest, "ParameterError", ConfigurationManager.AppSettings["PartyDateOneMinuteError"]);
            }
            var partyApplication = _repository.Create(dto);
            return partyApplication;
        }

        public object Put(UpdatePartyApplication dto)
        {
            var updateResult = _repository.Update(dto);
            return updateResult;
        }

        public object Post(UpdatePartyApplication dto)
        {
            return this.Put(dto);
        }

        public object Delete(DeletePartyApplication dto)
        {
            var deleteResult = _repository.Delete(dto);
            return deleteResult;
        }

        public object Post(DeletePartyApplication dto)
        {
            return this.Delete(dto);
        }

        public object Post(PartyInvitation dto)
        {
            return _repository.PartyInvitationInfo(dto);
        }
    }
}
