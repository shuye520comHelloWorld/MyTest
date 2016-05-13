using iParty.Services.Contract;
using ServiceStack.FluentValidation;

namespace iParty.Services.Validator
{
    public class QueryPartyValidator : AbstractValidator<QueryParty>
    {
        public QueryPartyValidator()
        {
            RuleFor(dto => dto.EventKey)
                .Must((dto, eventKey) =>
                    {
                        return !eventKey.HasValue;
                    })
                .When(dto => dto.IsFinished.HasValue && dto.IsFinished.Value)
                .WithMessage("History query is about party not event.");

            RuleFor(dto => dto.EventKey)
                .Must((dto, eventKey) =>
                    {
                        return !eventKey.HasValue;
                    })
                .When(dto => !dto.IsFinished.HasValue || dto.IsFinished.Value == false)
                .When(dto => dto.PartyKey.HasValue)
                .WithMessage("Cannot have both EventKey and PartyKey");

            RuleFor(dto => dto.PartyKey)
                .Must((dto, partyKey) =>
                    {
                        return !partyKey.HasValue;
                    })
                .When(dto => !dto.IsFinished.HasValue || dto.IsFinished.Value == false)
                .When(dto => dto.EventKey.HasValue)
                .WithMessage("Cannot have both EventKey and PartyKey");

            RuleFor(dto => dto.PartyStage)
                .Must((dto, partyStage) =>
                    {
                        return partyStage.HasValue;
                    })
                .When(dto => dto.PartyKey.HasValue)
                .WithMessage("Must specify a valid PartyStage when PartyKey present");
        }
    }
}
