using Algotecture.Common;
using Algotecture.Data.Persistence;
using Algotecture.Libraries.GeoAdminSearch;
using Algotecture.Libraries.PriceSpecifications;
using Algotecture.Libraries.Reservations;
using Algotecture.Libraries.Spaces;
using Serilog;

namespace Algotecture.WebApi.QrCode;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var webAppBuilder = WebAppBuilder.CreateWebApplicationBuilder(args);
        try
        {
            var pathToLog = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "algotecture", "log", "webapi-qrcode","serilog-log-.json");

            webAppBuilder.Host.UseSerilog((context, _, configuration) =>
            {
                var env = context.HostingEnvironment;

                configuration
                    .ReadFrom.Configuration(webAppBuilder.Configuration)
                    .Enrich.FromLogContext()
                    .Enrich.WithProperty("App", env.ApplicationName)
                    .Enrich.WithProperty("EnvironmentName", env.EnvironmentName)
                    .WriteTo.File(pathToLog, rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true, fileSizeLimitBytes: 104857600, retainedFileCountLimit: 31);
            });
            
            webAppBuilder.Services.AddRazorPages();
            
            webAppBuilder.Services.UseReservationLibrary();
            webAppBuilder.Services.UseSpaceLibrary();
            webAppBuilder.Services.UsePersistenceLibrary();
            webAppBuilder.Services.UseGeoAdminSearchLibrary();
            webAppBuilder.Services.UsePriceSpecificationLibrary();

            var app = webAppBuilder.Build();
            
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
                app.UseStaticFiles("/webapi-qrcode");
            }
            else
            {
                app.UseStaticFiles(); 
            }

            app.UseHttpsRedirection();
          
            app.UseRouting();
            app.MapRazorPages();

            await app.RunAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}