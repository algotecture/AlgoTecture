using MediatR;

namespace AlgoTecture.Identity.Contracts.Commands;

public record TelegramLoginCommand(long TelegramUserId, string? TelegramUserFullName)
    : IRequest<TelegramLoginResult>;

public record TelegramLoginResult(Guid IdentityId, Guid? UserId);