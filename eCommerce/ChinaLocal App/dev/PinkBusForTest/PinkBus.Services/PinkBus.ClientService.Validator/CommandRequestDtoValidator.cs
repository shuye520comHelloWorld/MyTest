
using PinkBus.Services.Common;
using ServiceStack.FluentValidation;
using System;

namespace PinkBus.ClientServices.Validator
{
    public class CommandRequestDtoValidator<T> : AbstractValidator<T> where T : CommandRequestDto
    {
        public CommandRequestDtoValidator()
        {
            RuleFor(x => x.CommandId)
                .NotEmpty()
                .NotEqual(Guid.Empty)
                .NotNull()
                .WithName("CommandId")
                .WithMessage("a mandatory field of type <guid>, it's for all the operations which could modify Server-Side resources.");
        }
    }
}
