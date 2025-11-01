using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

public class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .CreateBootstrapLogger();

        try
        {
            Log.Information("Starting up ApiGateway");

            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog((context, _, lc) =>
            {
                lc.ReadFrom.Configuration(context.Configuration)
                    .Enrich.FromLogContext()
                    .Enrich.WithProperty("App", builder.Environment.ApplicationName)
                    .Enrich.WithProperty("EnvironmentName", builder.Environment.EnvironmentName);
            });

            builder.Services.AddReverseProxy()
                .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

            var app = builder.Build();
            app.MapReverseProxy();

            app.Lifetime.ApplicationStarted.Register(() =>
                Log.Information("ApiGateway started successfully"));

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