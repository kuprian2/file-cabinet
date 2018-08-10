using FileCabinet.Bll.Contracts.Dtos.Base;

namespace FileCabinet.Bll.Contracts.Services.Base
{
    public interface IPlainEntityService<TPlainEntityDto, TKey>
        : IService<TPlainEntityDto, TKey>
        where TPlainEntityDto : EntityDto<TKey>
    {
        int Create(TPlainEntityDto entityDto);
    }
}