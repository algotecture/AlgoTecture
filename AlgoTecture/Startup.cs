using AlgoTecture.Core.Interfaces;
using AlgoTecture.CustomExceptionMiddleware;
using AlgoTecture.Data;
using AlgoTecture.Implementations;
using AlgoTecture.Interfaces;
using AlgoTecture.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();
            
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.TryAddTransient<ISpaceGetter, SpaceGetter>();
            services.TryAddTransient<IContractService, ContractService>();
            services.TryAddTransient<ISubSpaceService, SubSpaceService>();
            services.TryAddTransient<IBearerAuthenticator, BearerAuthenticator>();
            services.TryAddTransient<IUserCredentialsValidator, UserCredentialsValidator>();
            services.TryAddTransient<IPasswordEncryptor, PasswordEncryptor>();
            services.TryAddTransient<IUserService, UserService>();
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = AuthenticationOptions.Issuer,
                        ValidateAudience = true,
                        ValidAudience = AuthenticationOptions.Audience,
                        ValidateLifetime = true,
                        IssuerSigningKey = AuthenticationOptions.SymmetricSecurityKey,
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