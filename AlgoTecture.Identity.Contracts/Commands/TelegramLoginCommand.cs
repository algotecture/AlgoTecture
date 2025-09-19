using MediatR;

namespace AlgoTecture.Identity.Contracts.Commands;

public record TelegramLoginCommand(long TelegramUserId, string? TelegramUserFullName)
    : IRequest<TelegramLoginResult>;

public record TelegramLoginResult(long IdentityId, long? UserId);