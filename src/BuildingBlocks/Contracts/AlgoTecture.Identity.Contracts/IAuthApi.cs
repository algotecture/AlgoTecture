using AlgoTecture.Identity.Contracts.Commands;
using Refit;

namespace AlgoTecture.Identity.Contracts;

public interface IAuthApi
{
    [Post("/api/auth/telegram-login")]
    Task<TelegramLoginResult> TelegramLoginAsync([Body] TelegramLoginCommand command);
}