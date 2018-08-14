using FileCabinet.Bll.Contracts.Dtos.Base;
using System.Threading.Tasks;

namespace FileCabinet.Bll.Contracts.Services.Base
{
    public interface IPlainEntityService<TPlainEntityDto, TKey>
        : IService<TPlainEntityDto, TKey>
        where TPlainEntityDto : EntityDto<TKey>
    {
        int Create(TPlainEntityDto tagDto);

        Task<int> CreateAsync(TPlainEntityDto userDto);
    }
}