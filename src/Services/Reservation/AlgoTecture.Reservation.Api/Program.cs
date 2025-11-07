using System.Globalization;
using AlgoTecture.Reservation.Application.Handlers;
using AlgoTecture.Reservation.Infrastructure;
using AlgoTecture.Reservation.Infrastructure.Persistence;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace AlgoTecture.Reservation.Api;

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

            // сервисы
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<IDbContextFactory<ReservationDbContext>, ReservationRuntimeContextFactory>();

            builder.Services.AddDbContext<ReservationDbContext>(options =>
            {
                ReservationRuntimeContextFactory.ConfigureOptions(
                    (DbContextOptionsBuilder<ReservationDbContext>)options);
            });

            builder.Services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(typeof(GetUserReservationsQueryHandler).Assembly);
            });

            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssemblyContaining<GetUserReservationsQueryHandler>();

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

            Log.Warning("ReservationService started successfully");

            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "ReservationService terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}