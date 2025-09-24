using AlgoTecture.TelegramBot.Api.Controllers;
using AlgoTecture.TelegramBot.Api.Extensions;
using AlgoTecture.TelegramBot.Api.Interfaces;
using AlgoTecture.TelegramBot.Infrastructure;
using AlgoTecture.TelegramBot.Infrastructure.Consumers;
using AlgoTecture.TelegramBot.Infrastructure.Persistence;
using Deployf.Botf;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("hosting.json", optional: true, reloadOnChange: true);
builder.Configuration.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);

var cfg = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TelegramAccountDbContext>(options =>
{
    TelegramAccountRuntimeContextFactory.ConfigureOptions((DbContextOptionsBuilder<TelegramAccountDbContext>)options);
});

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

        mq.ReceiveEndpoint("telegram-bot-user-created", e =>
        {
            e.ConfigureConsumeTopology = false; 
            e.Bind("user-created");           
            e.ConfigureConsumer<UserCreatedConsumer>(ctx);
        });
        mq.ConfigureEndpoints(ctx);
    });
});
builder = BotFExtensions.ConfigureBot(args, builder);

builder.Services.AddTransient<IMainController, MainController>();
builder.Services.AddTransient<IParkingController, ParkingController>();

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseSwagger(); app.UseSwaggerUI();
}

app.UseBotf();
app.Run();