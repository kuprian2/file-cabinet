using FileCabinet.Bll.Contracts.Dtos;
using FileCabinet.Bll.Contracts.Services.Base;
using System.Collections.Generic;

namespace FileCabinet.Bll.Contracts.Services
{
    public interface IFileService : IService<FileDto, int>
    {
        IEnumerable<FileDto> GetByTags(IEnumerable<TagDto> tags);

        IEnumerable<FileDto> GetByFilter(string keyword);
    }
}