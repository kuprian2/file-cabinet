using FileCabinet.Dal.Contracts.Domain.Base;
using FileCabinet.Dal.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace FileCabinet.Dal.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity, int> where TEntity : Entity<int>
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.AsEnumerable();
        }

        public TEntity Get(int id)
        {
            return _dbSet.Find(id);
        }

        public void Create(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            var entityInDataSource = _dbSet.Find(entity.Id);
            _dbContext.Entry(entityInDataSource).CurrentValues.SetValues(entity);
        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);

            if (entity == null) return;

            _dbSet.Remove(entity);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate).AsEnumerable();
        }
    }
}
