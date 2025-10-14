using AlgoTecture.GeoAdminSearch;
using AlgoTecture.HttpClient;
using AlgoTecture.TelegramBot.Api.Extensions;
using AlgoTecture.TelegramBot.Application;
using AlgoTecture.TelegramBot.Application.Models;
using AlgoTecture.TelegramBot.Application.Services;
using AlgoTecture.TelegramBot.Infrastructure;
using AlgoTecture.TelegramBot.Infrastructure.Consumers;
using AlgoTecture.TelegramBot.Infrastructure.Persistence;
using Deployf.Botf;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseConfiguration(builder.Configuration);

var cfg = builder.Configuration;

builder.Services.AddOptions<GeoAdminSettings>()
    .BindConfiguration("GeoAdminSettings")
    .ValidateOnStart();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis")
                            ?? "localhost:6379"; 
    options.InstanceName = "TelegramBot_"; 
});

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

builder.Services.AddScoped<IUserCache, UserCache>();
builder.Services.AddScoped<ITelegramAccountDbContext, TelegramAccountDbContext>();
builder.Services.AddScoped<ITelegramBotService, TelegramBotService>();
builder.Services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
builder.Services.AddScoped<IReservationFlowService, ReservationFlowService>();
builder.Services.AddScoped<IGeoAdminSearcher, GeoAdminSearcher>();
builder.Services.AddScoped<IHttpService, HttpService>();

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseSwagger(); app.UseSwaggerUI();
}
app.UseBotf();
app.Run();