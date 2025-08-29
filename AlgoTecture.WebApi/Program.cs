using System;
using System.IO;
using System.Threading.Tasks;
using Algotecture.Common;
using Algotecture.Data.Images;
using Algotecture.Data.Persistence;
using Algotecture.Libraries.GeoAdminSearch;
using Algotecture.Libraries.PriceSpecifications;
using Algotecture.Libraries.Reservations;
using Algotecture.Libraries.Spaces;
using Algotecture.Libraries.Users.Models.AppsettingsModels;
using Algotecture.WebApi.Implementations;
using Algotecture.WebApi.Interfaces;
using Algotecture.WebApi.Middleware.CustomExceptionMiddleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;


namespace Algotecture.WebApi
{
    public static class Program
    {
        
       private static bool IsDevelopmentEnvironment { get; set; }
        
       public static async Task Main(string[] args)
          {
              var webAppBuilder = WebAppBuilder.CreateWebApplicationBuilder(args);
              try
              {
                  var pathToLog = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "algotecture", "log", "webapi","serilog-log-.json");
      
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
                  
                  webAppBuilder.Services.AddOptions();
                  webAppBuilder.Services.Configure<AuthenticationOptions>(webAppBuilder.Configuration.GetSection("AuthenticationOptions"));
                  webAppBuilder.Services.TryAddTransient<ISubSpaceService, SubSpaceService>();

                 // var jwtIssuer = webAppBuilder.Configuration.GetSection("AuthenticationOptions").GetChildren().First(x=>x.Key == "JwtIssuer").Value;
                 // var jwtAlgotectureSecret = webAppBuilder.Configuration.GetSection("AuthenticationOptions").GetChildren().First(x=>x.Key == "JwtAlgotectureSecret").Value;
                 //
                 //  webAppBuilder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                 //      .AddJwtBearer(options =>
                 //      {
                 //          options.RequireHttpsMetadata = false;
                 //          options.TokenValidationParameters = new TokenValidationParameters
                 //          {
                 //              ValidateIssuer = true,
                 //              ValidIssuer = jwtIssuer,
                 //              ValidateAudience = true,
                 //              ValidAudience = AuthenticationConstants.Audience,
                 //              ValidateLifetime = true,
                 //              IssuerSigningKey = AuthenticationConstants.GetSymmetricSecurityKey(jwtAlgotectureSecret),
                 //              ValidateIssuerSigningKey = true
                 //          };
                 //      });
            
                  webAppBuilder.Services.AddControllers();
                  webAppBuilder.Services.AddSwaggerGen(ConfigureSwaggerGeneration);
                  
                  webAppBuilder.Services.UseSpaceLibrary();
                  webAppBuilder.Services.UsePersistenceLibrary();
                  webAppBuilder.Services.UseGeoAdminSearchLibrary();
                  webAppBuilder.Services.UseReservationLibrary();
                  webAppBuilder.Services.UsePriceSpecificationLibrary();

                  var app = webAppBuilder.Build();

                  IsDevelopmentEnvironment = app.Environment.IsDevelopment();
                  
                  app.UseMiddleware<ExceptionMiddleware>();
                  app.UseStaticFiles();
                  app.UseRouting();
                  app.UseAuthentication();
                  app.UseAuthorization();
                  
                  app.UseSwagger();
                  app.UseSwaggerUI();
                  
                  app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

                  await app.RunAsync();
              }
              catch (Exception ex)
              {
                  Console.WriteLine(ex);
              }
          }
       
       private static void ConfigureSwaggerGeneration(SwaggerGenOptions options)
       {
           if (!IsDevelopmentEnvironment)
           {
               options.AddServer(new OpenApiServer{Url = "/webapi"});
           }
       }
    }
}