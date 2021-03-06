using System.Linq;
using AlgoTecture.EfCli;
using AlgoTecture.Implementations;
using AlgoTecture.Interfaces;
using AlgoTecture.Middleware.CustomExceptionMiddleware;
using AlgoTecture.Models;
using AlgoTecture.Libraries.GeoAdminSearch;
using AlgoTecture.Models.AppsettingsModels;
using AlgoTecture.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;

namespace AlgoTecture
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.UseEfCliLibrary();
            services.UsePersistenceLibrary();
            services.AddApplication<GeoAdminSearcherModule>();

            services.AddDatabaseDeveloperPageExceptionFilter();
            
            services.AddOptions();
            services.Configure<AuthenticationOptions>(Configuration.GetSection("AuthenticationOptions"));
            services.TryAddTransient<ISpaceGetter, SpaceGetter>();
            services.TryAddTransient<IContractService, ContractService>();
            services.TryAddTransient<ISubSpaceService, SubSpaceService>();
            services.TryAddTransient<IBearerAuthenticator, BearerAuthenticator>();
            services.TryAddTransient<IUserCredentialsValidator, UserCredentialsValidator>();
            services.TryAddTransient<IPasswordEncryptor, PasswordEncryptor>();
            services.TryAddTransient<IUserService, UserService>();

            var jwtIssuer = Configuration.GetSection("AuthenticationOptions").GetChildren().First(x=>x.Key == "JwtIssuer").Value;
            var jwtAlgotectureSecret = Configuration.GetSection("AuthenticationOptions").GetChildren().First(x=>x.Key == "JwtAlgotectureSecret").Value;
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtIssuer,
                        ValidateAudience = true,
                        ValidAudience = AuthenticationConstants.Audience,
                        ValidateLifetime = true,
                        IssuerSigningKey = AuthenticationConstants.GetSymmetricSecurityKey(jwtAlgotectureSecret),
                        ValidateIssuerSigningKey = true
                    };
                });
            
            services.AddControllers();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}