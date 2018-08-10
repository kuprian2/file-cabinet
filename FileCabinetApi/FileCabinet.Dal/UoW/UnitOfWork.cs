using FileCabinet.Dal.Contracts.UoW;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace FileCabinet.Dal.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext _dbContext;

        public UnitOfWork (DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_dbContext != null)
                {
                    _dbContext.Dispose();
                    _dbContext = null;
                }
            }
        }
    }
}