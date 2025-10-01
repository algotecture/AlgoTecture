using AlgoTecture.Identity.Application.Handlers;
using AlgoTecture.Identity.Contracts.Events;
using AlgoTecture.Identity.Infrastructure;
using AlgoTecture.Identity.Infrastructure.Consumers;
using AlgoTecture.Identity.Infrastructure.Persistence;
using AlgoTecture.IdentityService.Grpc;
using AlgoTecture.IdentityService.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("hosting.json", optional: true, reloadOnChange: true);

var cfg = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IDbContextFactory<IdentityDbContext>, IdentityRuntimeContextFactory>();

builder.Services.AddDbContext<IdentityDbContext>(options =>
{
    IdentityRuntimeContextFactory.ConfigureOptions((DbContextOptionsBuilder<IdentityDbContext>)options);
});

builder.Services.AddGrpc();

builder.Services.AddMediatR(configuration => 
{
    configuration.RegisterServicesFromAssembly(typeof(TelegramLoginHandler).Assembly);
});
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<TelegramLoginValidator>();

builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();
    x.AddConsumer<UserCreatedConsumer>();
    x.UsingRabbitMq((ctx, mq) =>
    {
        mq.Host(cfg["Rabbit:Host"] ?? "localhost", h =>
        {
            h.Username(cfg["Rabbit:Username"] ?? "guest");
            h.Password(cfg["Rabbit:Password"] ?? "guest");
        });
        mq.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5)));
        mq.Message<IdentityCreated>(e =>
        {
            e.SetEntityName("identity-events"); 
        });
        mq.PublishTopology.GetMessageTopology<IdentityCreated>().ExchangeType = "topic";
        mq.Send<IdentityCreated>(e => 
            e.UseRoutingKeyFormatter((SendContext<IdentityCreated> context) => "identity.*"));
        mq.ConfigureEndpoints(ctx);
    });
    x.AddEntityFrameworkOutbox<IdentityDbContext>(o =>
    {
        o.QueryDelay = TimeSpan.FromSeconds(2); 
        o.UsePostgres();                       
        o.DisableInboxCleanupService();        
        o.UseBusOutbox();                      
    });
    
});

var app = builder.Build();
app.MapGrpcService<TelegramAuthGrpcService>();

app.MapControllers();

if (builder.Environment.IsDevelopment())
{
    app.UseSwagger(); app.UseSwaggerUI();
}
app.Run();