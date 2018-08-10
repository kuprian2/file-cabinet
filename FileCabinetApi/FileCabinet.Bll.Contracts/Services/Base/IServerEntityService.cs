using FileCabinet.Bll.Contracts.Dtos.Base;
using System.IO;
using System.Threading.Tasks;

namespace FileCabinet.Bll.Contracts.Services.Base
{
    public interface IServerEntityService<TServerEntityDto, TKey>
        : IService<TServerEntityDto, TKey>
        where TServerEntityDto : EntityDto<TKey>
    {
        int Save(TServerEntityDto entityDto, Stream stream);

        Task<int> SaveAsync(TServerEntityDto entityDto, Stream stream);

        void Update(TServerEntityDto entityDto, Stream stream);

        Task UpdateAsync(TServerEntityDto entityDto, Stream stream);

        Task<Stream> ReadAsync(TServerEntityDto entityDto);
    }
}