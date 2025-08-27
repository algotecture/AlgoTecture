using MediatR;

namespace AlgoTecture.Identity.Contracts.Commands;

public record TelegramLoginCommand(long TelegramUserId)
    : IRequest<TelegramLoginResult>;

public record TelegramLoginResult(long IdentityId, long? UserId);