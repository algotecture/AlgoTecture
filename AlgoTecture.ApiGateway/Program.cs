using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace AlgoTecture.ApiGateway;

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

            builder.WebHost.UseConfiguration(builder.Configuration);
            builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

            var app = builder.Build();
            app.MapReverseProxy();

            var defaultCulture = CultureInfo.InvariantCulture;
            CultureInfo.DefaultThreadCurrentCulture = defaultCulture;
            CultureInfo.DefaultThreadCurrentUICulture = defaultCulture;

            Log.Information("ApiGateway started successfully");
            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "ApiGateway terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}