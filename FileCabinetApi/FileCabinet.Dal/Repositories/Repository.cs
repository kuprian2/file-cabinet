using FileCabinet.Dal.Contracts.Domain.Base;
using FileCabinet.Dal.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public TEntity Get(int id)
        {
            return _dbSet.Find(id);
        }

        public async Task<TEntity> GetAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Create(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public async Task CreateAsync(TEntity entity)
        {
            await Task.Run(() => _dbSet.Add(entity));
        }

        public void Update(TEntity entity)
        {
            var entityInDataSource = _dbSet.Find(entity.Id);
            _dbContext.Entry(entityInDataSource).CurrentValues.SetValues(entity);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            try
            {
                //_dbSet.AddOrUpdate(entity);

                //var entityInDataSource = await _dbSet.FindAsync(entity.Id);
                //_dbContext.Entry(entityInDataSource).CurrentValues.SetValues(entity);

                _dbContext.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            await Task.CompletedTask;
        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);

            if (entity == null) return;

            _dbSet.Remove(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity == null) return;

            _dbSet.Remove(entity);
            await Task.CompletedTask;
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate).AsEnumerable();
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }
    }
}
