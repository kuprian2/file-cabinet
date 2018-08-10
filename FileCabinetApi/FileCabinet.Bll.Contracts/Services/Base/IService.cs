using FileCabinet.Bll.Contracts.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FileCabinet.Bll.Contracts.Services.Base
{
    public interface IService<TEntityDto, TKey> where TEntityDto : EntityDto<TKey>
    {
        IEnumerable<TEntityDto> GetAll();

        TEntityDto Get(TKey id);

        void Update(TEntityDto entityDto);

        void Delete(TKey id);

        IEnumerable<TEntityDto> Find(Expression<Func<TEntityDto, bool>> predicate);
    }
}