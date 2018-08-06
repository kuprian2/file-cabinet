using FileCabinet.Dal.Contracts.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FileCabinet.Dal.Contracts.Repositories
{
    public interface IRepository<TEntity, TKey> where TEntity : Entity<TKey>
    {
        IEnumerable<TEntity> GetAll();

        TEntity Get(TKey id);

        void Create(TEntity entity);

        void Update(TEntity entity);

        void Delete(TKey id);

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    }
}