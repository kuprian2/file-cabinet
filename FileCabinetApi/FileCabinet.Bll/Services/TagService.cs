using AutoMapper;
using FileCabinet.Bll.Contracts.Dtos;
using FileCabinet.Bll.Contracts.Services;
using FileCabinet.Bll.Services.Base;
using FileCabinet.Dal.Contracts.Domain;
using FileCabinet.Dal.Contracts.Repositories;
using FileCabinet.Dal.Contracts.UoW;

namespace FileCabinet.Bll.Services
{
    public class TagService : Service<TagDto, Tag>, ITagService
    {
        public TagService(IUnitOfWork unitOfWork, IRepository<Tag, int> repository, IMapper mapper) 
            : base(unitOfWork, repository, mapper)
        {
        }
    }
}