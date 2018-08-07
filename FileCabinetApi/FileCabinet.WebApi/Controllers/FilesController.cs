using AutoMapper;
using FileCabinet.Bll.Contracts.Dtos;
using FileCabinet.Bll.Contracts.Services;
using FileCabinet.WebApi.ModelBinders;
using FileCabinet.WebApi.Models;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace FileCabinet.WebApi.Controllers
{
    public class FilesController : ApiController
    {
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public FilesController(IFileService fileService, IMapper mapper)
        {
            _fileService = fileService;
            _mapper = mapper;
        }

        // GET api/files?tags=tag1,tag2,tag3
        [HttpGet]
        public IHttpActionResult Get(
            [ModelBinder(typeof(CommaDelimitedArrayModelBinder))]IEnumerable<TagInfoModel> tags)
        {
            var mappedTags = _mapper.Map<IEnumerable<TagDto>>(tags);
            var filteredFiles = _fileService.GetByTags(mappedTags);
            var mappedFiles = _mapper.Map<IEnumerable<FileInfoModel>>(filteredFiles);

            return Ok(mappedFiles);
        }

        // GET api/files/5
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var fileDto = _fileService.Get(id);

            if (fileDto == null) return NotFound();

            return Ok(_mapper.Map<FileInfoModel>(fileDto));
        }

        // GET api/files?keyword=somefilter
        [HttpGet]
        public IHttpActionResult Search([FromUri] string keyword)
        {
            var filteredFiles = _fileService.GetByFilter(keyword);
            var mappedFiles = _mapper.Map<IEnumerable<FileInfoModel>>(filteredFiles);

            return Ok(mappedFiles);
        }

        // DELETE api/files/5
        [HttpDelete]
        public void Delete(int id)
        {
            _fileService.Delete(id);
        }

        // POST api/files
        [HttpPost]
        public IHttpActionResult Post([FromBody] FileCreateModel fileModel)
        {
            if (fileModel == null) return BadRequest();

            var fileDto = _mapper.Map<FileDto>(fileModel);
            var createdFileId = _fileService.Create(fileDto);

            return Ok(createdFileId);
        }

        // PUT api/files/5
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] FileUpdateModel fileModel)
        {
            if (fileModel == null) return BadRequest();

            var fileInDataSource = _fileService.Get(id);

            if (fileInDataSource == null) return NotFound();
                
            var fileDto = _mapper.Map<FileDto>(fileModel);
            fileDto.Id = id;
            _fileService.Update(fileDto);
            return Ok();
        }
    }
}
