using FileCabinet.Bll.Contracts.Dtos;
using FileCabinet.Bll.Contracts.Services.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileCabinet.Bll.Contracts.Services
{
    public interface IFileService : IComplexEntityService<FileDto, int>
    {
        IEnumerable<FileDto> GetByTags(IEnumerable<TagDto> tags);

        Task<IEnumerable<FileDto>> GetByTagsAsync(IEnumerable<TagDto> tags);

        IEnumerable<FileDto> GetByFilter(string keyword);

        Task<IEnumerable<FileDto>> GetByFilterAsync(string keyword);
    }
}