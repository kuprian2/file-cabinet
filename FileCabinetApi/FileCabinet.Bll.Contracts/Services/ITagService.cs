using FileCabinet.Bll.Contracts.Dtos;
using FileCabinet.Bll.Contracts.Services.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileCabinet.Bll.Contracts.Services
{
    public interface ITagService : IPlainEntityService<TagDto, int>
    {
        IEnumerable<TagDto> FindByIds(IEnumerable<int> ids);

        Task<IEnumerable<TagDto>> FindByIdsAsync(IEnumerable<int> ids);
    }
}