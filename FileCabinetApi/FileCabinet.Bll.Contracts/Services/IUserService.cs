using FileCabinet.Bll.Contracts.Dtos;
using FileCabinet.Bll.Contracts.Services.Base;
using System.Threading.Tasks;

namespace FileCabinet.Bll.Contracts.Services
{
    public interface IUserService : IPlainEntityService<UserDto, int>
    {
        UserDto GetByName(string username);

        Task<UserDto> GetByNameAsync(string username);
    }
}