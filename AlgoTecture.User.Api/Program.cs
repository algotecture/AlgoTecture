using System.Globalization;
using AlgoTecture.User.Application.Commands;
using AlgoTecture.User.Contracts.Events;
using AlgoTecture.User.Infrastructure;
using AlgoTecture.User.Infrastructure.Consumers;
using AlgoTecture.User.Infrastructure.Persistence;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace AlgoTecture.User.Api;

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
            var env = builder.Environment;
            var cfg = builder.Configuration;

            builder.Host.UseSerilog((context, _, lc) =>
            {
                lc.ReadFrom.Configuration(context.Configuration)
                    .Enrich.FromLogContext()
                    .Enrich.WithProperty("App", env.ApplicationName)
                    .Enrich.WithProperty("EnvironmentName", env.EnvironmentName);
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<UserDbContext>(o =>
                UserRuntimeContextFactory.ConfigureOptions((DbContextOptionsBuilder<UserDbContext>)o));

            builder.Services.AddMediatR(c =>
                c.RegisterServicesFromAssembly(typeof(AddUserCarNumberCommand).Assembly));

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
                    mq.Message<UserCreated>(e => e.SetEntityName("user-created"));
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

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapControllers();

            var defaultCulture = CultureInfo.InvariantCulture;
            CultureInfo.DefaultThreadCurrentCulture = defaultCulture;
            CultureInfo.DefaultThreadCurrentUICulture = defaultCulture;

            Log.Information("UserService started successfully");
            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "UserService terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}