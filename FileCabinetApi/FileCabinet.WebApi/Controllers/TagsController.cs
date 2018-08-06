using AutoMapper;
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
        public IEnumerable<TagModel> Get()
        {
            return _mapper.Map<IEnumerable<TagModel>>(_tagService.GetAll());
        }
    }
}
