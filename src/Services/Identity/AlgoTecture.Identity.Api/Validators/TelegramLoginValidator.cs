using AlgoTecture.Identity.Contracts.Commands;
using FluentValidation;

namespace AlgoTecture.IdentityService.Validators;

public class TelegramLoginValidator : AbstractValidator<TelegramLoginCommand>
{
    public TelegramLoginValidator()
    {
        RuleFor(x => x.TelegramUserId).GreaterThan(0);
    } 
}