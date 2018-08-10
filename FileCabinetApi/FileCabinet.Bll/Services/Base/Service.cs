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
using System.Threading.Tasks;

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

        public async Task<IEnumerable<TEntityDto>> GetAllAsync()
        {
            var entities = (await Repository.GetAllAsync()).ToList();
            return Mapper.Map<IEnumerable<TEntityDto>>(entities);
        }

        public TEntityDto Get(int id)
        {
            return Mapper.Map<TEntityDto>(Repository.Get(id));
        }

        public async Task<TEntityDto> GetAsync(int id)
        {
            return Mapper.Map<TEntityDto>(await Repository.GetAsync(id));
        }

        public void Update(TEntityDto entityDto)
        {
            if (entityDto == null) throw new ArgumentNullException(nameof(entityDto));

            var entity = Mapper.Map<TEntity>(entityDto);
            Repository.Update(entity);
            UnitOfWork.SaveChanges();
        }

        public async Task UpdateAsync(TEntityDto entityDto)
        {
            if (entityDto == null) throw new ArgumentNullException(nameof(entityDto));

            var entity = Mapper.Map<TEntity>(entityDto);
            await Repository.UpdateAsync(entity);
            await UnitOfWork.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            Repository.DeleteAsync(id);
            UnitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await Repository.DeleteAsync(id);
            await UnitOfWork.SaveChangesAsync();
        }

        public IEnumerable<TEntityDto> Find(Expression<Func<TEntityDto, bool>> predicate)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            var mappedPredicate = Mapper.Map<Expression<Func<TEntity, bool>>>(predicate);
            var foundEntities = Repository.Find(mappedPredicate).ToList();

            return Mapper.Map<IEnumerable<TEntityDto>>(foundEntities);
        }

        public async Task<IEnumerable<TEntityDto>> FindAsync(Expression<Func<TEntityDto, bool>> predicate)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            var mappedPredicate = Mapper.Map<Expression<Func<TEntity, bool>>>(predicate);
            var foundEntities = (await Repository.FindAsync(mappedPredicate)).ToList();

            return Mapper.Map<IEnumerable<TEntityDto>>(foundEntities);
        }
    }
}