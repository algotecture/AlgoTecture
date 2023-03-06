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
            var connectionString = _configuration?.GetConnectionString("DefaultConnection");
            if (connectionString == null) return 0;
            
            var context = new ApplicationDbContext(connectionString);
            await context.Database.EnsureCreatedAsync();

            return 0;
        }
    }
}