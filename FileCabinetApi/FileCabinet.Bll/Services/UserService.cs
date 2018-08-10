using System;
using System.Threading.Tasks;
using AutoMapper;
using FileCabinet.Bll.Contracts.Dtos;
using FileCabinet.Bll.Contracts.Services;
using FileCabinet.Bll.Services.Base;
using FileCabinet.Dal.Contracts.Domain;
using FileCabinet.Dal.Contracts.Repositories;
using FileCabinet.Dal.Contracts.UoW;

namespace FileCabinet.Bll.Services
{
    public class UserService : PlainEntityService<UserDto, User>, IUserService
    {
        public UserService(IUnitOfWork unitOfWork, IRepository<User, int> repository, IMapper mapper) 
            : base(unitOfWork, repository, mapper)
        {
        }

        public override int Create(UserDto tagDto)
        {
            if (tagDto == null) throw new ArgumentNullException(nameof(tagDto));

            var mappedUser = Mapper.Map<User>(tagDto);

            Repository.Create(mappedUser);
            UnitOfWork.SaveChanges();

            return mappedUser.Id;
        }

        public override async Task<int> CreateAsync(UserDto userDto)
        {
            if (userDto == null) throw new ArgumentNullException(nameof(userDto));

            var mappedUser = Mapper.Map<User>(userDto);

            await Repository.CreateAsync(mappedUser);
            await UnitOfWork.SaveChangesAsync();

            return mappedUser.Id;
        }
    }
}