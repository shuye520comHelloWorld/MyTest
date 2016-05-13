using iParty.Services.Contract;
using iParty.Services.Interface;
using iParty.Services.Interface.Exception;
using QuartES.Services.Core;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace iParty.Services.Impl
{
    public sealed class InvitationService : Service
    {
        Regex _phoneRegex = new Regex(@"^1{1}\d{10}$", RegexOptions.Compiled);
        Regex _nameRegex = new Regex(@"^[a-zA-Z\u4e00-\u9fa5]{1,10}$", RegexOptions.Compiled);
        IInvitationRepository _invitationRepository;
        IContext _context;
        
        public InvitationService(IContext context, IInvitationRepository invitationRepository)
        {
            _context = context;
            _invitationRepository = invitationRepository;
        }

        public object Post(AcceptInvitation req)
        {
            if (string.IsNullOrWhiteSpace(req.Name) || string.IsNullOrWhiteSpace(req.PhoneNumber))
            {
                throw new HttpError(HttpStatusCode.BadRequest, "ParameterError", ConfigurationManager.AppSettings["PhoneNameEmptyError"]);
            }
            if (!_nameRegex.IsMatch(req.Name))
            {
                throw new HttpError(HttpStatusCode.BadRequest, "ParameterError", ConfigurationManager.AppSettings["InvalidCustomerNameError"]);
            }
            if(!_phoneRegex.IsMatch(req.PhoneNumber))
            {
                throw new HttpError(HttpStatusCode.BadRequest, "ParameterError", ConfigurationManager.AppSettings["InvalidPhoneNameError"]);
            }
            return _invitationRepository.Accept(req);
        }

        public object Delete(CancelInvitation req)
        {
            return _invitationRepository.Cancel(req);
        }

        public object Put(InvitationCheckIn req)
        {
            return _invitationRepository.CheckIn(req);
        }

        public object Get(QueryCustomer req)
        {
            return _invitationRepository.GetCustomerList(req);
        }

        public object Get(CustomerDetail req)
        {
            return _invitationRepository.GetCustomerDetail(req);
        }

        public object Post(CheckCustomerInvitation req)
        {
            return _invitationRepository.CheckCustomer(req);
        }
    }
}
