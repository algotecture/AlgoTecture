using Algotecture.Identity.Contracts.Commands;
using Algotecture.Identity.Contracts.Events;
using Algotecture.Identity.Infrastructure.Persistence;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Algotecture.Identity.Application.Handlers;

public class TelegramLoginHandler : IRequestHandler<TelegramLoginCommand, TelegramLoginResult>
{
    private readonly IdentityDbContext _db;
    private readonly IPublishEndpoint _publish;

    public TelegramLoginHandler(IdentityDbContext db, IPublishEndpoint publish)
    { _db = db; _publish = publish; }

    public async Task<TelegramLoginResult> Handle(TelegramLoginCommand request, CancellationToken ct)
    {
        const string provider = "Telegram";
        var extId = request.TelegramUserId.ToString();

        var identity = await _db.Identities
            .FirstOrDefaultAsync(x => x.Provider == provider && x.ExternalId == extId, ct);

        if (identity is null)
        {
            identity = new Domain.Identity {
                Provider = provider,
                ExternalId = extId,
                CreatedAt = DateTime.UtcNow
            };
            _db.Identities.Add(identity);
            await _db.SaveChangesAsync(ct);

            // триггерим UsersService создать профиль
            await _publish.Publish(new UserIdentityCreated(identity.Id, provider, extId), ct);
        }

        return new TelegramLoginResult(identity.Id, identity.UserId);
    }
}   
