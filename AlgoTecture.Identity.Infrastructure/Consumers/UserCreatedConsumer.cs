using AlgoTecture.Identity.Infrastructure.Persistence;
using AlgoTecture.User.Contracts.Events;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace AlgoTecture.Identity.Infrastructure.Consumers;

public class UserCreatedConsumer : IConsumer<UserCreated>
{
    private readonly IdentityDbContext _db;
    
    public UserCreatedConsumer(IdentityDbContext db) => _db = db;

    public async Task Consume(ConsumeContext<UserCreated> ctx)
    {
        var msg = ctx.Message;
        var identity = await _db.Identities.FirstOrDefaultAsync(x => x.Id == msg.IdentityId);
        if (identity is null) return;
        if (identity.UserId == msg.UserId) return;

        identity.UserId = msg.UserId;
        await _db.SaveChangesAsync();
    }  
}