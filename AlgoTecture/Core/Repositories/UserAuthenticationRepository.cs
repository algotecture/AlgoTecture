using System;
using System.Threading.Tasks;
using AlgoTecture.Core.Interfaces;
using AlgoTecture.Data;
using AlgoTecture.Models.RepositoryModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AlgoTecture.Core.Repositories
{
    public class UserAuthenticationRepository : GenericRepository<UserAuthentication>, IUserAuthenticationRepository
    {
        public UserAuthenticationRepository(ApplicationDbContext context, ILogger logger) : base(context, logger) { }

        public async Task<bool> IsValidPasswordAsync(long userId, string hashedPassword)
        {
            if (userId <= 0) throw new ArgumentOutOfRangeException(nameof(userId));
            if (string.IsNullOrEmpty(hashedPassword)) throw new ArgumentException("Value cannot be null or empty.", nameof(hashedPassword));

            var userAuth = await dbSet.FirstOrDefaultAsync(x =>
                x.UserId == userId && x.HashedPassword == hashedPassword);

            return userAuth != null;
        }
    }
}