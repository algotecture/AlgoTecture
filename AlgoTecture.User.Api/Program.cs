using System;
using AlgoTecture.User.Contracts.Events;
using AlgoTecture.User.Infrastructure;
using AlgoTecture.User.Infrastructure.Consumers;
using AlgoTecture.User.Infrastructure.Persistence;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddJsonFile("hosting.json", optional: true, reloadOnChange: true);
}

var cfg = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<UserDbContext>(options =>
{
    UserRuntimeContextFactory.ConfigureOptions((DbContextOptionsBuilder<UserDbContext>)options);
});

builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();
    x.AddConsumer<IdentityCreatedConsumer>();
    x.UsingRabbitMq((ctx, mq) =>
    {
        mq.Host(cfg["Rabbit:Host"] ?? "localhost", h =>
        {
            h.Username(cfg["Rabbit:Username"] ?? "guest");
            h.Password(cfg["Rabbit:Password"] ?? "guest");
        });
        mq.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5)));
        mq.Message<UserCreated>(e =>
        {
            e.SetEntityName("user-created"); 
        });
        mq.ReceiveEndpoint("user-service-identity-events", e =>
        {
            e.ConfigureConsumeTopology = false;

            e.Bind("identity-events", s =>
            {
                s.ExchangeType = "topic";
                s.RoutingKey = "identity.*";
            });

            e.ConfigureConsumer<IdentityCreatedConsumer>(ctx);
        });
    });
});

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseSwagger(); app.UseSwaggerUI();
}

app.Run();