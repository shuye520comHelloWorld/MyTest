using iParty.Services.Contract;
using iParty.Services.Entity;
using iParty.Services.Interface;
using iParty.Services.Interface.Exception;
using iParty.Services.ORM.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Services.Infrastructure
{
    public sealed class WeChartCardRepository : IWeChartCardRepository
    {
        WeChartCardOperation _weCardOperation;
        WeChartCardCodeOperation _weCardCodeOperation;
        IPartyRepository _partyRepository;
        PartyOperation _partyOperation;
        public WeChartCardRepository(IPartyRepository partyRepository)
        {
            _weCardOperation = new WeChartCardOperation();
            _weCardCodeOperation = new WeChartCardCodeOperation();
            _partyOperation = new PartyOperation();
            _partyRepository = partyRepository;
        }

        public QueryWeChartResponse GetCardID(QueryWeChart dto)
        {
            var token = _partyRepository.GetTokenEntity(dto.InvitationToken);
            var events = _partyOperation.GetSingleDataByFunc(p => p.PartyKey == token.PartyKey.Value);
            if (events == null)
            {
                throw new NotFoundException("The request party not exists.");
            }
            var wecard = _weCardOperation.GetSingleDataByFunc(wc => wc.EventKey == events.EventKey);
            if (wecard == null)
            {
                throw new NotFoundException("The request wecard not exists.");
            }
            return new QueryWeChartResponse
            {
                CardID = wecard.CardID,
                CreateDate = wecard.CreateDate,
                EventKey = wecard.EventKey
            };
        }

        public CreateWeChartCardResponse CreateWeCard(CreateWeChartCard dto)
        {
            WeChartCardCodeEntity entity = new WeChartCardCodeEntity
            {
                CardID = dto.CardID,
                CreateDate = DateTime.Now,
                InvitationKey = dto.CardCode,
                PartyKey = dto.PartyKey,
                Status = (int)WeCardStatus.Created,
                UpdateDate = DateTime.Now
            };
            _weCardCodeOperation.Create(entity);
            return new CreateWeChartCardResponse
            {
                CardCode = entity.InvitationKey,
                CardID = entity.CardID,
                CreateDate = entity.CreateDate,
                PartyKey = entity.PartyKey,
                Status = entity.Status,
                UpdateDate = entity.UpdateDate
            };
        }

        public CreateWeChartCardResponse UpdateWeCard(UpdateWeChartCard dto)
        {
            var cardCode = _weCardCodeOperation.GetSingleDataByFunc(wcc => wcc.InvitationKey == dto.CardCode);
            if (cardCode == null)
            {
                throw new NotFoundException("The request wechart card not exists.");
            }
            cardCode.Status = dto.Status;
            cardCode.UpdateDate = DateTime.Now;
            _weCardCodeOperation.Update(cardCode);
            return new CreateWeChartCardResponse
            {
                CardCode = cardCode.InvitationKey,
                CardID = cardCode.CardID,
                CreateDate = cardCode.CreateDate,
                PartyKey = cardCode.PartyKey,
                Status = cardCode.Status,
                UpdateDate = cardCode.UpdateDate
            };
        }
    }
}
