using System;
using System.Threading.Tasks;
using AlgoTecture.Core.Interfaces;
using AlgoTecture.Core.Repositories;
using Microsoft.Extensions.Logging;

namespace AlgoTecture.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public IUserRepository Users { get; private set; }
        
        public ISpaceRepository Spaces { get; private set; }
        
        public IContractRepository Contracts { get; private set; }
        
        public IUserAuthenticationRepository UserAuthentications { get; private set; }

        public UnitOfWork(ApplicationDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");

            Users = new UserRepository(context, _logger);
            Spaces = new SpaceRepository(context, _logger);
            Contracts = new ContractRepository(context, _logger);
            UserAuthentications = new UserAuthenticationRepository(context, _logger);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}