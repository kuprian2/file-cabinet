using AutoMapper;
using FileCabinet.Bll.Contracts.Dtos;
using FileCabinet.Bll.Contracts.Services;
using FileCabinet.Bll.Services.Base;
using FileCabinet.Dal.Contracts.Domain;
using FileCabinet.Dal.Contracts.Repositories;
using FileCabinet.Dal.Contracts.UoW;
using System;
using System.Linq;
using System.Threading.Tasks;

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

        public UserDto GetByName(string username)
        {
            if (username == null) throw new ArgumentNullException();

            var user = Repository.Find(u => u.Name == username).FirstOrDefault();

            return Mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetByNameAsync(string username)
        {
            if (username == null) throw new ArgumentNullException();

            var user = (await Repository.FindAsync(u => u.Name == username)).First();

            return Mapper.Map<UserDto>(user);
        }
    }
}