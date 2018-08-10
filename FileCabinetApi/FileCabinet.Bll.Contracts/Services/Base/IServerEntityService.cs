using System.IO;
using FileCabinet.Bll.Contracts.Dtos.Base;

namespace FileCabinet.Bll.Contracts.Services.Base
{
    public interface IServerEntityService<TServerEntityDto, TKey>
        : IService<TServerEntityDto, TKey>
        where TServerEntityDto : EntityDto<TKey>
    {
        int Save(TServerEntityDto entityDto, Stream stream);

        void Update(TServerEntityDto entityDto, Stream stream);

        Stream Read(TServerEntityDto entityDto);
    }
}