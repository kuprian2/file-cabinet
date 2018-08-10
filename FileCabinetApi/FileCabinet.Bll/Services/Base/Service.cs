using AutoMapper;
using FileCabinet.Bll.Contracts.Dtos.Base;
using FileCabinet.Bll.Contracts.Services.Base;
using FileCabinet.Dal.Contracts.Domain.Base;
using FileCabinet.Dal.Contracts.Repositories;
using FileCabinet.Dal.Contracts.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FileCabinet.Bll.Services.Base
{
    public abstract class Service<TEntityDto, TEntity> : IService<TEntityDto, int> 
        where TEntityDto : EntityDto<int>
        where TEntity : Entity<int>
    {
        protected readonly IRepository<TEntity, int> Repository;
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly IMapper Mapper;

        protected Service(IUnitOfWork unitOfWork, IRepository<TEntity, int> repository, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Repository = repository;
            Mapper = mapper;
        }

        public IEnumerable<TEntityDto> GetAll()
        {
            var entities = Repository.GetAll().ToList();
            return Mapper.Map<IEnumerable<TEntityDto>>(entities);
        }

        public TEntityDto Get(int id)
        {
            return Mapper.Map<TEntityDto>(Repository.Get(id));
        }

        public void Update(TEntityDto entityDto)
        {
            if (entityDto == null) throw new ArgumentNullException(nameof(entityDto));

            var entity = Mapper.Map<TEntity>(entityDto);
            Repository.Update(entity);
            UnitOfWork.SaveChanges();
        }

        public void Delete(int id)
        {
            Repository.Delete(id);
            UnitOfWork.SaveChanges();
        }

        public IEnumerable<TEntityDto> Find(Expression<Func<TEntityDto, bool>> predicate)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            var mappedPredicate = Mapper.Map<Expression<Func<TEntity, bool>>>(predicate);
            var foundEntities = Repository.Find(mappedPredicate).ToList();

            return Mapper.Map<IEnumerable<TEntityDto>>(foundEntities);
        }
    }
}