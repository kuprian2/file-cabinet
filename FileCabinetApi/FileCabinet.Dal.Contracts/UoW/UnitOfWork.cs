using System;
using System.Threading.Tasks;

namespace FileCabinet.Dal.Contracts.UoW
{
    public interface IUnitOfWork: IDisposable
    {
        void SaveChanges();

        Task SaveChangesAsync();
    }
}
