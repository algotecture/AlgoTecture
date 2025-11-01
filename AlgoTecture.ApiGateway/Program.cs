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
            // builder.Host.UseSerilog((ctx, services, lc) => lc
            //     .ReadFrom.Configuration(ctx.Configuration)
            //     .ReadFrom.Services(services)
            //     .Enrich.WithProperty("App", builder.Environment.ApplicationName)
            //     .Enrich.WithProperty("EnvironmentName", builder.Environment.EnvironmentName)
            //     .WriteTo.Console()
            // );

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