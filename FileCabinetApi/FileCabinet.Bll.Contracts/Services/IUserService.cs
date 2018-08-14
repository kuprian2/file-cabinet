using FileCabinet.Bll.Contracts.Dtos;
using FileCabinet.Bll.Contracts.Services.Base;

namespace FileCabinet.Bll.Contracts.Services
{
    public interface IUserService : IPlainEntityService<UserDto, int>
    {
    }
}