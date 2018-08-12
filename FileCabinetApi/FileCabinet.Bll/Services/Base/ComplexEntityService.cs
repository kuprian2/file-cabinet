using AutoMapper;
using FileCabinet.Bll.Contracts.Dtos.Base;
using FileCabinet.Bll.Contracts.Services.Base;
using FileCabinet.Dal.Contracts.Domain.Base;
using FileCabinet.Dal.Contracts.Repositories;
using FileCabinet.Dal.Contracts.UoW;
using System.IO;
using System.Threading.Tasks;

namespace FileCabinet.Bll.Services.Base
{
    public abstract class ComplexEntityService<TComplexEntityDto, TEntity>
        : Service<TComplexEntityDto, TEntity>, IComplexEntityService<TComplexEntityDto, int>
        where TComplexEntityDto : EntityDto<int>
        where TEntity : Entity<int>
    {
        protected ComplexEntityService(IUnitOfWork unitOfWork, IRepository<TEntity, int> repository, IMapper mapper)
            : base(unitOfWork, repository, mapper)
        {
        }

        public abstract int Save(TComplexEntityDto entityDto, Stream stream);

        public abstract Task<int> SaveAsync(TComplexEntityDto entityDto, Stream stream);

        public abstract void Update(TComplexEntityDto entityDto, Stream stream);

        public abstract Task UpdateAsync(TComplexEntityDto entityDto, Stream stream);

        public abstract Stream Read(TComplexEntityDto entityDto);

        public abstract Task<Stream> ReadAsync(TComplexEntityDto entityDto);
    }
}