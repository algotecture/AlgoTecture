using AlgoTecture.Data.Persistence.Ef;
using Microsoft.Extensions.Configuration;

namespace AlgoTecture.EfCli
{
    class Program
    {
        private static IConfiguration configuration;

        static async Task<int> Main(string[] args)
        {
            Console.WriteLine("EfCli has been started");
            var currentDirectory = Directory.GetCurrentDirectory();
            configuration = new ConfigurationBuilder().SetBasePath(currentDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var context = new ApplicationDbContext(connectionString);
            await context.Database.EnsureCreatedAsync();
            return 0;
        }
    }
}