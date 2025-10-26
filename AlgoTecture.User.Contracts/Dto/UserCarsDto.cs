namespace AlgoTecture.User.Contracts.Dto;

public record UserCarsDto(Guid UserId, List<string> CarNumbers);
