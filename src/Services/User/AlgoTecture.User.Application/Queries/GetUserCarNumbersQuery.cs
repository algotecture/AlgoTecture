using AlgoTecture.User.Contracts.Dto;
using MediatR;

namespace AlgoTecture.User.Application.Queries;

public record GetUserCarNumbersQuery(Guid UserId) : IRequest<UserCarsDto>;