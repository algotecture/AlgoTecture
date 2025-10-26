using System.Text.Json;
using AlgoTecture.User.Contracts.Dto;
using AlgoTecture.User.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AddUserCarNumberCommand = AlgoTecture.User.Application.Commands.AddUserCarNumberCommand;

namespace AlgoTecture.User.Application.Handlers.Commands;

public sealed class AddUserCarNumberHandler
    : IRequestHandler<AddUserCarNumberCommand, UserCarsDto>
{
    private readonly UserDbContext _db;
    public AddUserCarNumberHandler(UserDbContext db) => _db = db;

    public async Task<UserCarsDto> Handle(AddUserCarNumberCommand request, CancellationToken ct)
    {
        var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == request.UserId && !x.IsDeleted, ct);
        if (user is null)
            throw new InvalidOperationException($"User {request.UserId} not found");

        var current = new List<string>();
        if (!string.IsNullOrWhiteSpace(user.CarNumbers) && user.CarNumbers != "{}")
        {
            try
            {
                current = JsonSerializer.Deserialize<List<string>>(user.CarNumbers) ?? new();
            }
            catch
            {
                current = new();
            }
        }

        var normalized = Normalize(request.CarNumber);
        if (!current.Contains(normalized, StringComparer.OrdinalIgnoreCase))
            current.Add(normalized);

        user.CarNumbers = JsonSerializer.Serialize(current);
        await _db.SaveChangesAsync(ct);

        return new UserCarsDto(user.Id, current);
    }

    private static string Normalize(string input) =>
        input.Trim().ToUpperInvariant().Replace(" ", "").Replace("-", "");
}