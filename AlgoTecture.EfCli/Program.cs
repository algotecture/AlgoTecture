using Algotecture.Data.Persistence.Ef;

namespace Algotecture.EfCli
{
    internal static class Program
    {
        private static async Task<int> Main(string[] args)
        {
            Console.WriteLine("EfCli has been started");

            var context = new ApplicationDbContext(Provider.NpgSql);
            await context.Database.EnsureCreatedAsync();

            Console.WriteLine("done");

            return 1;
        }
    }
}