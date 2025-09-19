using AlgoTecture.Identity.Contracts.Commands;
using AlgoTecture.Identity.Contracts.Events;
using AlgoTecture.Identity.Infrastructure.Persistence;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AlgoTecture.Identity.Application.Handlers;

public class TelegramLoginHandler : IRequestHandler<TelegramLoginCommand, TelegramLoginResult>
{
    private readonly IdentityDbContext _db;
    private readonly IPublishEndpoint _publish;

    public TelegramLoginHandler(IdentityDbContext db, IPublishEndpoint publish)
    { _db = db; _publish = publish; }

    public async Task<TelegramLoginResult> Handle(TelegramLoginCommand request, CancellationToken ct)
    {
        const string provider = "Telegram";
        var providerUserId = request.TelegramUserId.ToString();
        var providerUserFullName = request.TelegramUserFullName;

        var identity = await _db.Identities
            .FirstOrDefaultAsync(x => x.Provider == provider && x.ProviderUserId == providerUserId, ct);

        if (identity is null)
        {
            identity = new Domain.Identity {
                Provider = provider,
                ProviderUserId = providerUserId,
                CreatedAt = DateTime.UtcNow
            };
            _db.Identities.Add(identity);
            
            await _publish.Publish(new IdentityCreated(identity.Id, provider, providerUserId, providerUserFullName), ct);
            
            await _db.SaveChangesAsync(ct);
            throw new Exception();
        }

        return new TelegramLoginResult(identity.Id, identity.UserId);
    }
}   
