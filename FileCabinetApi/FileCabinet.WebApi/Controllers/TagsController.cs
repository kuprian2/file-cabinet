using AutoMapper;
using FileCabinet.Bll.Contracts.Dtos;
using FileCabinet.Bll.Contracts.Services;
using FileCabinet.WebApi.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace FileCabinet.WebApi.Controllers
{
    public class TagsController : ApiController
    {
        private readonly ITagService _tagService;
        private readonly IMapper _mapper;

        public TagsController(ITagService tagService, IMapper mapper)
        {
            _tagService = tagService;
            _mapper = mapper;
        }

        // GET api/tags
        [HttpGet]
        public IHttpActionResult Get()
        {
            var tagDtos = _mapper.Map<IEnumerable<TagInfoModel>>(_tagService.GetAll());
            return Ok(tagDtos);
        }

        // GET api/tags/5
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var tagDto = _tagService.Get(id);

            if (tagDto == null) return NotFound();

            return Ok(_mapper.Map<TagInfoModel>(tagDto));
        }

        // DELETE api/tags/5
        [HttpDelete]
        public void Delete(int id)
        {
            _tagService.Delete(id);
        }

        // POST api/tags
        [HttpPost]
        public IHttpActionResult Post([FromBody] TagCreateModel tagModel)
        {
            if (tagModel == null) return BadRequest();

            var tagDto = _mapper.Map<TagDto>(tagModel);
            var createdTagId = _tagService.Create(tagDto);

            return Ok(createdTagId);
        }
    }
}
