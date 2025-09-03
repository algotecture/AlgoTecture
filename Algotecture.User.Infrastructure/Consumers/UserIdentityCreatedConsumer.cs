using Algotecture.Identity.Contracts.Events;
using Algotecture.User.Contracts.Events;
using Algotecture.User.Infrastructure.Persistence;
using MassTransit;

namespace Algotecture.User.Infrastructure.Consumers;

public class UserIdentityCreatedConsumer : IConsumer<UserIdentityCreated>
{
    private readonly UserDbContext _db;
    private readonly IPublishEndpoint _publish;
    public UserIdentityCreatedConsumer(UserDbContext db, IPublishEndpoint publish)
    { _db = db; _publish = publish; }

    public async Task Consume(ConsumeContext<UserIdentityCreated> ctx)
    {
        var user = new Domain.User();
        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        await _publish.Publish(new UserCreated(user.Id, ctx.Message.IdentityId));
    }    
}