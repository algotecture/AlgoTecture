using AlgoTecture.User.Contracts.Dto;
using MediatR;

namespace AlgoTecture.User.Application.Commands;

public record AddUserCarNumberCommand(Guid UserId, string CarNumber) : IRequest<UserCarsDto>;
