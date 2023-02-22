using AlgoTecture.Data.Persistence.Ef;

namespace AlgoTecture.EfCli
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            Console.WriteLine("EfCli has been started");
            var context = new ApplicationDbContext();
            await context.Database.EnsureCreatedAsync();
            return 0;

        }
    }
}