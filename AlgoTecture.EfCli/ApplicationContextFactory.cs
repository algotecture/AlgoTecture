using Microsoft.EntityFrameworkCore;

namespace AlgoTecture.EfCli
{
    public class ApplicationContextFactory : IContextFactory<ApplicationDbContext>
    {
        private readonly DbContextOptions<ApplicationDbContext> _appContextOptions;

        public ApplicationContextFactory(DbContextOptions<ApplicationDbContext> contextOptions = null)
        {
            _appContextOptions = contextOptions;
        }

        public ApplicationDbContext CreateContext()
        {
            return _appContextOptions == null ? new ApplicationDbContext() : new ApplicationDbContext(_appContextOptions);
        }

        public ApplicationDbContext CreateContext(int timeOut)
        {
            return _appContextOptions == null ? new ApplicationDbContext(timeOut) : new ApplicationDbContext(_appContextOptions);
        }
    }
}
