using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
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
            builder.Configuration
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            builder.Services.AddReverseProxy()
                 .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
            WebApplication app;
            try
            {
                app = builder.Build();
            
            }
            catch (Exception buildEx)
            {
                Log.Fatal(buildEx, "Error during builder.Build()");
                throw;
            }
            Log.Information("Gateway built");

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