using Algotecture.Identity.Contracts.Commands;
using FluentValidation;

namespace Algotecture.IdentityService.Validators;

public class TelegramLoginValidator : AbstractValidator<TelegramLoginCommand>
{
    public TelegramLoginValidator()
    {
        RuleFor(x => x.TelegramUserId).GreaterThan(0);
    } 
}