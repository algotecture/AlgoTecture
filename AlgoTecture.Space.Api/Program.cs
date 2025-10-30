using System.Globalization;
using AlgoTecture.Space.Application.Handlers;
using AlgoTecture.Space.Infrastructure;
using AlgoTecture.Space.Infrastructure.Persistence;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace AlgoTecture.Space.Api;

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

            builder.Host.UseSerilog((context, _, lc) =>
            {
                lc.ReadFrom.Configuration(context.Configuration)
                    .Enrich.FromLogContext()
                    .Enrich.WithProperty("App", env.ApplicationName)
                    .Enrich.WithProperty("EnvironmentName", env.EnvironmentName);
            });

            var cfg = builder.Configuration;

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<IDbContextFactory<SpaceDbContext>, SpaceRuntimeContextFactory>();
            builder.Services.AddDbContext<SpaceDbContext>(o =>
                SpaceRuntimeContextFactory.ConfigureOptions((DbContextOptionsBuilder<SpaceDbContext>)o));

            builder.Services.AddMediatR(c =>
                c.RegisterServicesFromAssembly(typeof(GetSpacesByTypeQueryHandler).Assembly));

            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssemblyContaining<GetSpacesByTypeQueryHandler>();

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

            Log.Information("SpaceService started successfully");
            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "SpaceService terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}