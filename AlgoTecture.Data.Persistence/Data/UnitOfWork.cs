using Algotecture.Data.Persistence.Core.Interfaces;
using Algotecture.Data.Persistence.Core.Repositories;
using Algotecture.Data.Persistence.Ef;
using Algotecture.Domain.Models.RepositoryModels;
using Microsoft.Extensions.Logging;

namespace Algotecture.Data.Persistence.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public IUserRepository Users { get; private set; }
        
        public ISpaceRepository Spaces { get; private set; }

        public IUserAuthenticationRepository UserAuthentications { get; private set; }
        
        public ITelegramUserInfoRepository TelegramUserInfos { get; private set; }
        
        public IUtilizationTypeRepository UtilizationTypes { get; private set; }
        
        public IReservationRepository Reservations { get; private set; }

        public IPriceSpecificationRepository PriceSpecifications { get; set; }

        public UnitOfWork(ApplicationDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory != null ? loggerFactory.CreateLogger("logs") : null;

            Users = new UserRepository(context, _logger!);
            Spaces = new SpaceRepository(context, _logger!);
            UserAuthentications = new UserAuthenticationRepository(context, _logger!);
            TelegramUserInfos = new TelegramUserInfoRepository(context, _logger!);
            UtilizationTypes = new UtilizationTypeRepository(context, _logger!);
            Reservations = new ReservationRepository(context, _logger!);
            PriceSpecifications = new PriceSpecificationRepository(context, _logger!);
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