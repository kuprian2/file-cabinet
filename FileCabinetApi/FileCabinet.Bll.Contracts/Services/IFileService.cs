using FileCabinet.Bll.Contracts.Dtos;
using FileCabinet.Bll.Contracts.Services.Base;
using System.Collections.Generic;
using System.IO;

namespace FileCabinet.Bll.Contracts.Services
{
    public interface IFileService : IServerEntityService<FileDto, int>
    {
        IEnumerable<FileDto> GetByTags(IEnumerable<TagDto> tags);

        IEnumerable<FileDto> GetByFilter(string keyword);
    }
}