using FileCabinet.Bll.Contracts.Dtos;
using FileCabinet.Bll.Contracts.Services.Base;

namespace FileCabinet.Bll.Contracts.Services
{
    public interface ITagService : IPlainEntityService<TagDto, int>
    {
    }
}