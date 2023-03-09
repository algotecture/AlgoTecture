using AlgoTecture.Data.Persistence.Ef;
using Microsoft.Extensions.Configuration;

namespace AlgoTecture.EfCli
{
    class Program
    {
        private static IConfiguration? _configuration;

        static async Task<int> Main(string[] args)
        {
            Console.WriteLine("EfCli has been started");
            var currentDirectory = Directory.GetCurrentDirectory();
            _configuration = new ConfigurationBuilder().SetBasePath(currentDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
            var appConnectionString = string.Empty;
                
            if (OperatingSystem.IsLinux())
            {
                appConnectionString = Configurator.GetConfiguration().GetConnectionString("DemoConnection");
            }
            if (OperatingSystem.IsWindows())
            {
                appConnectionString = Configurator.GetConfiguration().GetConnectionString("WindowsSqlLiteDevelopingConnection");
            }
            if (OperatingSystem.IsMacOS())
            {
                appConnectionString = Configurator.GetConfiguration().GetConnectionString("DefaultConnection");
            }

            if (appConnectionString == null) return 0;
            
            var context = new ApplicationDbContext(appConnectionString);
            await context.Database.EnsureCreatedAsync();

            return 0;
        }
    }
}