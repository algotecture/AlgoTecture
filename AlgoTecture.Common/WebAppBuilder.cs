using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace AlgoTecture.Common;

public static class WebAppBuilder
{
    public static WebApplicationBuilder CreateWebApplicationBuilder(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
        {
            config.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("hosting.json", true);
        }); ;
        
        return builder;
    }
}