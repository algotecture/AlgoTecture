using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AlgoTecture.Common;
using AlgoTecture.Data.Persistence;
using AlgoTecture.Libraries.GeoAdminSearch;
using AlgoTecture.Libraries.Spaces;
using AlgoTecture.Libraries.Users.Models;
using AlgoTecture.Libraries.Users.Models.AppsettingsModels;
using AlgoTecture.WebApi.Implementations;
using AlgoTecture.WebApi.Interfaces;
using AlgoTecture.WebApi.Middleware.CustomExceptionMiddleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Serilog;


namespace AlgoTecture.WebApi
{
    public static class Program
    {
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
                  
                  webAppBuilder.Services.UseSpaceLibrary();
                  webAppBuilder.Services.UsePersistenceLibrary();
                  webAppBuilder.Services.UseGeoAdminSearchLibrary();
      
                  var app = webAppBuilder.Build();
                  
                  app.UseMiddleware<ExceptionMiddleware>();
                  app.UseStaticFiles();
                  app.UseRouting();
                  app.UseAuthentication();
                  app.UseAuthorization();
                  
                  app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

                  await app.RunAsync();
              }
              catch (Exception ex)
              {
                  Console.WriteLine(ex);
              }
          }
    }
}