using AlgoTecture.Data.Persistence.Ef;
using AlgoTecture.Libraries.Environments;
using Microsoft.Extensions.Configuration;

namespace AlgoTecture.EfCli
{
    internal static class Program
    {
        private static async Task<int> Main(string[] args)
        {
            Console.WriteLine("EfCli has been started");

            var appConnectionString = Configurator.GetConfiguration().GetConnectionString("Algotecture-Demo");

            if (appConnectionString == null)
            {
                Console.WriteLine("Connection string is null. EfCli is stopped");
                return 0;
            }

            var context = new ApplicationDbContext(appConnectionString);
            await context.Database.EnsureCreatedAsync();

            Console.WriteLine("done");

            return 1;
        }
    }
}