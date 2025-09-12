using AlgoTecture.Identity.Contracts.Events;
using AlgoTecture.User.Contracts.Events;
using AlgoTecture.User.Infrastructure.Persistence;
using MassTransit;

namespace AlgoTecture.User.Infrastructure.Consumers;

public class IdentityCreatedConsumer : IConsumer<IdentityCreated>
{
    private readonly UserDbContext _db;
    private readonly IPublishEndpoint _publish;
    public IdentityCreatedConsumer(UserDbContext db, IPublishEndpoint publish)
    { _db = db; _publish = publish; }

    public async Task Consume(ConsumeContext<IdentityCreated> ctx)
    {
        var user = new Domain.User();
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        await _publish.Publish(new UserCreated(user.Id, ctx.Message.IdentityId));
    }    
}