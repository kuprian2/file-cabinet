using AutoMapper;
using FileCabinet.Bll.Contracts.Dtos;
using FileCabinet.Bll.Contracts.Services;
using FileCabinet.WebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task<IHttpActionResult> Get()
        {
            var tagDtos = await _tagService.GetAllAsync();
            var tagModels = _mapper.Map<IEnumerable<TagInfoModel>>(tagDtos);
            return Ok(tagModels);
        }

        // GET api/tags/5
        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            var tagDto = await _tagService.GetAsync(id);

            if (tagDto == null) return NotFound();

            return Ok(_mapper.Map<TagInfoModel>(tagDto));
        }

        [Authorize]
        // DELETE api/tags/5
        [HttpDelete]
        public async Task Delete(int id)
        {
            await _tagService.DeleteAsync(id);
        }

        [Authorize]
        // POST api/tags
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody] TagCreateModel tagModel)
        {
            if (tagModel == null) return BadRequest();

            var tagDto = _mapper.Map<TagDto>(tagModel);
            var createdTagId = await _tagService.CreateAsync(tagDto);

            return Ok(createdTagId);
        }
    }
}
