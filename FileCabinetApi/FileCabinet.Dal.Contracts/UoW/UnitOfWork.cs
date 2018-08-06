using System;

namespace FileCabinet.Dal.Contracts.UoW
{
    public interface IUnitOfWork: IDisposable
    {
        void SaveChanges();
    }
}
