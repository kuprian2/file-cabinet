using AutoMapper;
using FileCabinet.Bll.Contracts.Dtos.Base;
using FileCabinet.Bll.Contracts.Services.Base;
using FileCabinet.Dal.Contracts.Domain.Base;
using FileCabinet.Dal.Contracts.Repositories;
using FileCabinet.Dal.Contracts.UoW;
using System;
using System.Threading.Tasks;

namespace FileCabinet.Bll.Services.Base
{
    public abstract class PlainEntityService<TPlainEntityDto, TEntity>
        : Service<TPlainEntityDto, TEntity>, IPlainEntityService<TPlainEntityDto, int>
        where TPlainEntityDto : EntityDto<int>
        where TEntity : Entity<int>
    {
        protected PlainEntityService(IUnitOfWork unitOfWork, IRepository<TEntity, int> repository, IMapper mapper)
            : base(unitOfWork, repository, mapper)
        {
        }

        public abstract int Create(TPlainEntityDto tagDto);

        public abstract Task<int> CreateAsync(TPlainEntityDto userDto);
    }
}