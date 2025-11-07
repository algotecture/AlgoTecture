using AlgoTecture.Identity.Application.Handlers;
using AlgoTecture.Identity.Contracts.Events;
using AlgoTecture.Identity.Infrastructure;
using AlgoTecture.Identity.Infrastructure.Consumers;
using AlgoTecture.Identity.Infrastructure.Persistence;
using AlgoTecture.IdentityService.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace AlgoTecture.Identity.Api;

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

            builder.Host.UseSerilog((context, _, loggerConfig) =>
            {
                loggerConfig
                    .ReadFrom.Configuration(context.Configuration)
                    .Enrich.FromLogContext()
                    .Enrich.WithProperty("App", env.ApplicationName)
                    .Enrich.WithProperty("EnvironmentName", env.EnvironmentName);
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<IDbContextFactory<IdentityDbContext>, IdentityRuntimeContextFactory>();

            builder.Services.AddDbContext<IdentityDbContext>(options =>
            {
                IdentityRuntimeContextFactory.ConfigureOptions((DbContextOptionsBuilder<IdentityDbContext>)options);
            });

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
                    mq.Message<IdentityCreated>(e => e.SetEntityName("identity-events"));
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

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapControllers();

            Log.Warning("IdentityService started successfully");
            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "IdentityService terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}