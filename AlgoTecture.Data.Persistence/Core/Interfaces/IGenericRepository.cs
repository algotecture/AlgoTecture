﻿using System.Linq.Expressions;

namespace Algotecture.Data.Persistence.Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> All();
        Task<T?> GetById(long id);
        Task<T> GetByGuid(Guid id);
        Task<T> Add(T entity);
        Task<bool> Delete(long id);
        Task<T> Upsert(T entity);
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
    }
}