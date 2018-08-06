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

        // GET api/files/5
        [HttpGet]
        public FileInfoModel Get(int id)
        {
            return _mapper.Map<FileInfoModel>(_fileService.Get(id));
        }

        // GET api/files?tags=tag1,tag2,tag3
        [HttpGet]
        public IEnumerable<FileInfoModel> Get(
            [ModelBinder(typeof(CommaDelimitedArrayModelBinder))]IEnumerable<TagModel> tags)
            {
            var mappedTags = _mapper.Map<IEnumerable<TagDto>>(tags);
            var filteredFiles = _fileService.GetByTags(mappedTags);
            return _mapper.Map<IEnumerable<FileInfoModel>>(filteredFiles);
        }

        // GET api/files?keyword=somefilter
        [HttpGet]
        public IEnumerable<FileInfoModel> Search([FromUri] string keyword)
        {
            var filteredFiles = _fileService.GetByFilter(keyword);
            return _mapper.Map<IEnumerable<FileInfoModel>>(filteredFiles);
        }

        // DELETE api/files/5
        [HttpDelete]
        public void Delete(int id)
        {
            _fileService.Delete(id);
        }

        // POST api/files
        [HttpPost]
        public int Post([FromBody] FileCreateModel file)
        {
            var fileDto = _mapper.Map<FileDto>(file);
            return _fileService.Create(fileDto);
        }

        // PUT api/files/5
        [HttpPut]
        public void Put(int id, [FromBody] FileUpdateModel file)
        {
            var fileDto = _mapper.Map<FileDto>(file);
            fileDto.Id = id;
            _fileService.Update(fileDto);
        }
    }
}
