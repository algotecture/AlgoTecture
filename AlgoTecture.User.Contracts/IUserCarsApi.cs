using AlgoTecture.User.Contracts.Dto;
using AlgoTecture.User.Contracts.Requests;
using Refit;

namespace AlgoTecture.User.Contracts;

public interface IUserCarsApi
{
    [Get("/api/users/{userId}/cars")]
    Task<UserCarsDto> GetCarNumbersAsync(Guid userId);

    [Post("/api/users/{userId}/cars")]
    Task<UserCarsDto> AddCarNumberAsync(Guid userId, [Body] AddCarNumberRequest request);
}