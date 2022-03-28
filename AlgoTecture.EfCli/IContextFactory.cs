using Microsoft.EntityFrameworkCore;

namespace AlgoTecture.EfCli
{
    public interface IContextFactory<out T> where T : DbContext
    {
        T CreateContext();

        T CreateContext(int timeOut);
    }
}
