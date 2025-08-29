﻿using Algotecture.Data.Persistence.Core.Interfaces;
using Algotecture.Data.Persistence.Ef;
using Algotecture.Domain.Models.RepositoryModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Algotecture.Data.Persistence.Core.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context, ILogger logger) : base(context, logger) { }

        public virtual async Task<User?> GetByEmail(string email)
        {
            return await dbSet.FirstOrDefaultAsync(x=>x.Email == email);
        }
        
        public virtual async Task<User> GetByTelegramChatId(long telegramChatId)
        {
            return await dbSet.Include(x=>x.TelegramUserInfo).FirstOrDefaultAsync(x=>x.TelegramUserInfo.TelegramChatId == telegramChatId);
        }
        
        public override async Task<IEnumerable<User>> All()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(UserRepository));
                return new List<User>();
            }
        }

        public override async Task<User> Upsert(User entity)
        {
            try
            {
                User? existingUser;
                if (entity.TelegramUserInfoId != null)
                {
                    existingUser = await dbSet.SingleOrDefaultAsync(x => x.TelegramUserInfoId == entity.TelegramUserInfoId);
                }
                else
                {
                    existingUser = await dbSet.SingleOrDefaultAsync(x => x.Id == entity.Id); 
                }

                if (existingUser == null)
                {
                    var newEntity = await Add(entity);
                    
                    _logger.LogInformation(newEntity.TelegramUserInfoId != default
                        ? $"New User from telegram bot: {newEntity.TelegramUserInfo?.TelegramUserFullName} registered in the system"
                        : $"New User from webapi: email {newEntity.Email}, phone {newEntity.Phone} registered in the system");

                    return newEntity;
                }
                
                existingUser.Phone = entity.Phone ?? existingUser.Phone;
                existingUser.TelegramUserInfoId = entity.TelegramUserInfoId ?? existingUser.TelegramUserInfoId;

                return existingUser;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Upsert function error", typeof(UserRepository));
                throw;
            }
        }

        public override async Task<bool> Delete(long id)
        {
            try
            {
                var exist = await dbSet.FirstOrDefaultAsync(x => x.Id == id);

                if (exist == null) return false;

                dbSet.Remove(exist);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Delete function error", typeof(UserRepository));
                return false;
            }
        }
    }
}