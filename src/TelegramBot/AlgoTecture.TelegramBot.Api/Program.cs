using System.Globalization;
using System.Net;
using AlgoTecture.GeoAdminSearch;
using AlgoTecture.HttpClient;
using AlgoTecture.Identity.Contracts;
using AlgoTecture.Reservation.Contracts;
using AlgoTecture.Space.Contracts;
using AlgoTecture.TelegramBot.Api.Extensions;
using AlgoTecture.TelegramBot.Application;
using AlgoTecture.TelegramBot.Application.Models;
using AlgoTecture.TelegramBot.Application.Services;
using AlgoTecture.TelegramBot.Infrastructure;
using AlgoTecture.TelegramBot.Infrastructure.Consumers;
using AlgoTecture.TelegramBot.Infrastructure.Persistence;
using AlgoTecture.User.Contracts;
using Deployf.Botf;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Refit;
using Serilog;

namespace AlgoTecture.TelegramBot.Api;

public class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateBootstrapLogger();

        try
        {
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Configuration
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            
            var env = builder.Environment;
            var cfg = builder.Configuration;

            builder.Host.UseSerilog((context, _, lc) =>
            {
                lc.ReadFrom.Configuration(context.Configuration)
                    .Enrich.FromLogContext()
                    .Enrich.WithProperty("App", env.ApplicationName)
                    .Enrich.WithProperty("EnvironmentName", env.EnvironmentName);
            });

            builder.WebHost.UseConfiguration(builder.Configuration);

            builder.Services.AddOptions<GeoAdminSettings>()
                .BindConfiguration("GeoAdminSettings")
                .ValidateOnStart();

            builder.Services.AddOptions<DeepSeekSettings>()
                .BindConfiguration("DeepSeek")
                .ValidateOnStart();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddStackExchangeRedisCache(o =>
            {
                o.Configuration = cfg.GetConnectionString("Redis") ?? "localhost:6379";
                o.InstanceName = "TelegramBot_";
            });

            builder.Services.AddDbContext<TelegramAccountDbContext>(o =>
                TelegramAccountRuntimeContextFactory.ConfigureOptions(
                    (DbContextOptionsBuilder<TelegramAccountDbContext>)o));

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

            var apiClientsSection = builder.Configuration.GetSection("ApiClients");
            var gatewayBaseUrl = apiClientsSection.GetValue<string>("GatewayBaseUrl");

            builder.Services.AddRefitClient<IAuthApi>()
                .ConfigureHttpClient(c =>
                    c.BaseAddress = new Uri($"{gatewayBaseUrl}{apiClientsSection["Identity:BasePath"]}"));

            builder.Services.AddRefitClient<IUserCarsApi>()
                .ConfigureHttpClient(c =>
                    c.BaseAddress = new Uri($"{gatewayBaseUrl}{apiClientsSection["User:BasePath"]}"));

            builder.Services.AddRefitClient<ISpaceApi>()
                .ConfigureHttpClient(c =>
                    c.BaseAddress = new Uri($"{gatewayBaseUrl}{apiClientsSection["Space:BasePath"]}"));

            builder.Services.AddRefitClient<IReservationApi>()
                .ConfigureHttpClient(c =>
                    c.BaseAddress = new Uri($"{gatewayBaseUrl}{apiClientsSection["Reservation:BasePath"]}"));

            var app = builder.Build();

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseBotf();

            var defaultCulture = CultureInfo.InvariantCulture;
            CultureInfo.DefaultThreadCurrentCulture = defaultCulture;
            CultureInfo.DefaultThreadCurrentUICulture = defaultCulture;

            Log.Information("TelegramBotService started successfully");
            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "TelegramBotService terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}