using System.Text.Json;
using AlgoTecture.User.Application.Queries;
using AlgoTecture.User.Contracts.Dto;
using AlgoTecture.User.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AlgoTecture.User.Application.Handlers.Queries;

public sealed class GetUserCarNumbersHandler
    : IRequestHandler<GetUserCarNumbersQuery, UserCarsDto>
{
    private readonly UserDbContext _db;
    public GetUserCarNumbersHandler(UserDbContext db) => _db = db;

    public async Task<UserCarsDto> Handle(GetUserCarNumbersQuery request, CancellationToken ct)
    {
        var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == request.UserId && !x.IsDeleted, ct);
        if (user is null)
            throw new InvalidOperationException($"User {request.UserId} not found");

        var json = string.IsNullOrWhiteSpace(user.CarNumbers) ? "[]" : user.CarNumbers;
        List<string>? list;
        try
        {
            list = JsonSerializer.Deserialize<List<string>>(json);
        }
        catch
        {
            list = new();
        }

        return new UserCarsDto(user.Id, list ?? new());
    }
}