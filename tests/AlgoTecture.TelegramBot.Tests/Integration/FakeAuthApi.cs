using AlgoTecture.Identity.Contracts;
using AlgoTecture.Identity.Contracts.Commands;

namespace AlgoTecture.TelegramBot.Tests.Integration;

public class FakeAuthApi : IAuthApi
{
    public Task<TelegramLoginResult> TelegramLoginAsync(TelegramLoginCommand command)
        => Task.FromResult(new TelegramLoginResult(Guid.NewGuid(), Guid.NewGuid()));
}