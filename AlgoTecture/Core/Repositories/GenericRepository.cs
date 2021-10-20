using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AlgoTecture.Core.Interfaces;
using AlgoTecture.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AlgoTecture.Core.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected ApplicationDbContext context;
        internal DbSet<T> dbSet;
        public readonly ILogger _logger;

        public GenericRepository(ApplicationDbContext context, ILogger logger)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
            _logger = logger;
        }

        public virtual async Task<T> GetById(long id)
        {
            return await dbSet.FindAsync(id);
        }
        
        public virtual async Task<T> GetByGuid(Guid id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual async Task<T> Add(T entity)
        {
          var createdEntity = await dbSet.AddAsync(entity);
            return createdEntity.Entity;
        }

        public virtual Task<bool> Delete(long id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IEnumerable<T>> All()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await dbSet.Where(predicate).ToListAsync();
        }

        public virtual Task<T> Upsert(T entity)
        {
            throw new NotImplementedException();
        }
    }
}