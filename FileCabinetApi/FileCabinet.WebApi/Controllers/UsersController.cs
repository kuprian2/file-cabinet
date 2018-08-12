using AutoMapper;
using FileCabinet.Bll.Contracts.Dtos;
using FileCabinet.Bll.Contracts.Services;
using FileCabinet.WebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace FileCabinet.WebApi.Controllers
{
    public class UsersController : ApiController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        // GET api/users
        [Authorize]
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var userDtos = await _userService.GetAllAsync();
            var userModels = _mapper.Map<IEnumerable<UserInfoModel>>(userDtos);
            return Ok(userModels);
        }

        // GET api/users/5
        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            var userDto = await _userService.GetAsync(id);

            if (userDto == null) return NotFound();

            return Ok(_mapper.Map<UserInfoModel>(userDto));
        }

        [Authorize]
        // PUT api/users/5
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] UserUpdateModel userModel)
        {
            if (userModel == null) return BadRequest();

            var userInDataSource = _userService.Get(id);

            if (userInDataSource == null) return NotFound();

            var userDto = _mapper.Map<UserDto>(userModel);
            userDto.Id = id;
            _userService.Update(userDto);
            return Ok();
        }
    }
}
