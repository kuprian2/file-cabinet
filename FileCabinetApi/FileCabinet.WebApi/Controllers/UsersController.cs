﻿using AutoMapper;
using FileCabinet.Bll.Contracts.Dtos;
using FileCabinet.Bll.Contracts.Services;
using FileCabinet.WebApi.Models;
using System.Collections.Generic;
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
        [HttpGet]
        public IHttpActionResult Get()
        {
            var users = _mapper.Map<IEnumerable<UserInfoModel>>(_userService.GetAll());
            return Ok(users);
        }

        // GET api/users/5
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var userDto = _userService.Get(id);

            if (userDto == null) return NotFound();

            return Ok(_mapper.Map<UserInfoModel>(userDto));
        }

        // DELETE api/users/5
        [HttpDelete]
        public void Delete(int id)
        {
            _userService.Delete(id);
        }

        // POST api/users
        [HttpPost]
        public IHttpActionResult Post([FromBody] UserCreateModel userModel)
        {
            if (userModel == null) return BadRequest();

            var userDto = _mapper.Map<UserDto>(userModel);
            var userCreatedId = _userService.Create(userDto);

            return Ok(userCreatedId);
        }

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
