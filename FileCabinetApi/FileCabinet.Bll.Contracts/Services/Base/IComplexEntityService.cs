using FileCabinet.Bll.Contracts.Dtos.Base;
using System.IO;
using System.Threading.Tasks;

namespace FileCabinet.Bll.Contracts.Services.Base
{
    public interface IComplexEntityService<TComplexEntityDto, TKey>
        : IService<TComplexEntityDto, TKey>
        where TComplexEntityDto : EntityDto<TKey>
    {
        int Save(TComplexEntityDto entityDto, Stream stream);

        Task<int> SaveAsync(TComplexEntityDto entityDto, Stream stream);

        void Update(TComplexEntityDto entityDto, Stream stream);

        Task UpdateAsync(TComplexEntityDto entityDto, Stream stream);

        Task<Stream> ReadAsync(TComplexEntityDto entityDto);
    }
}