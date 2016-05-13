using ServiceStack.FluentValidation;
using System;

namespace iParty.Services.Validator
{
    public class GuidValidator : AbstractValidator<Guid>
    {
        public GuidValidator()
        {
            RuleFor(g => g).NotEmpty();
        }

        public GuidValidator(string propertyName)
        {
            RuleFor(g => g)
                .NotEmpty()
                .NotEqual(Guid.Empty)
                .NotNull()
                .WithName(propertyName ?? "Guid");
        }
    }
}
