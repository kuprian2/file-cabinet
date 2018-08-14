using FileCabinet.Bll.Contracts.Dtos.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileCabinet.Bll.Contracts.Services.Base
{
    public interface IService<TEntityDto, TKey> where TEntityDto : EntityDto<TKey>
    {
        IEnumerable<TEntityDto> GetAll();

        Task<IEnumerable<TEntityDto>> GetAllAsync();

        TEntityDto Get(TKey id);

        Task<TEntityDto> GetAsync(TKey id);

        void Update(TEntityDto entityDto);

        Task UpdateAsync(TEntityDto entityDto);

        void Delete(TKey id);

        Task DeleteAsync(TKey id);
    }
}