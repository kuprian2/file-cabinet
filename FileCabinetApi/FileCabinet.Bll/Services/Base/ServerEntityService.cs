using AutoMapper;
using FileCabinet.Bll.Contracts.Dtos.Base;
using FileCabinet.Bll.Contracts.Services.Base;
using FileCabinet.Dal.Contracts.Domain.Base;
using FileCabinet.Dal.Contracts.Repositories;
using FileCabinet.Dal.Contracts.UoW;
using System;
using System.IO;

namespace FileCabinet.Bll.Services.Base
{
    public abstract class ServerEntityService<TServerEntityDto, TEntity>
        : Service<TServerEntityDto, TEntity>, IServerEntityService<TServerEntityDto, int>
        where TServerEntityDto : EntityDto<int>
        where TEntity : Entity<int>
    {
        protected ServerEntityService(IUnitOfWork unitOfWork, IRepository<TEntity, int> repository, IMapper mapper) 
            : base(unitOfWork, repository, mapper)
        {
        }

        public abstract int Save(TServerEntityDto entityDto, Stream stream);

        public abstract void Update(TServerEntityDto entityDto, Stream stream);

        public abstract Stream Read(TServerEntityDto entityDto);
    }
}