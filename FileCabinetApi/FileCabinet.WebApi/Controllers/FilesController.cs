using AutoMapper;
using FileCabinet.Bll.Contracts.Dtos;
using FileCabinet.Bll.Contracts.Services;
using FileCabinet.WebApi.ModelBinders;
using FileCabinet.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace FileCabinet.WebApi.Controllers
{
    public class FilesController : ApiController
    {
        private readonly IFileService _fileService;
        private readonly ITagService _tagService;
        private readonly IMapper _mapper;

        public FilesController(IFileService fileService, ITagService tagService, IMapper mapper)
        {
            _fileService = fileService;
            _tagService = tagService;
            _mapper = mapper;
        }

        // GET api/files?tags=tag1,tag2,tag3
        [HttpGet]
        public async Task<IHttpActionResult> GetInfo([ModelBinder]IEnumerable<TagInfoModel> tags)
        {
            var mappedTags = _mapper.Map<IEnumerable<TagDto>>(tags);
            var filteredFiles = await _fileService.GetByTagsAsync(mappedTags);
            var mappedFiles = _mapper.Map<IEnumerable<FileInfoModel>>(filteredFiles);

            return Ok(mappedFiles);
        }

        // GET api/files/5
        [HttpGet]
        public async Task<IHttpActionResult> GetInfo(int id)
        {
            var fileDto = await _fileService.GetAsync(id);

            if (fileDto == null) return NotFound();

            return Ok(_mapper.Map<FileInfoModel>(fileDto));
        }

        // GET api/files?keyword=somefilter
        [HttpGet]
        public async Task<IHttpActionResult> Search([FromUri] string keyword)
        {
            var filteredFiles = await _fileService.GetByFilterAsync(keyword);
            var mappedFiles = _mapper.Map<IEnumerable<FileInfoModel>>(filteredFiles);

            return Ok(mappedFiles);
        }

        // DELETE api/files/5
        [Authorize]
        [HttpDelete]
        public async Task Delete(int id)
        {
            await _fileService.DeleteAsync(id);
        }

        // GET download/5
        [Route("api/download/{id}")]
        [Authorize]
        [HttpGet]
        public async Task<IHttpActionResult> Download(int id)
        {
            var fileDto = await _fileService.GetAsync(id);

            if (fileDto == null) return NotFound();

            var stream = await _fileService.ReadAsync(fileDto);

            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(stream);
            response.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/octet-stream");

            response.Content.Headers.ContentDisposition =
                new ContentDispositionHeaderValue("attachment") { FileName = fileDto.Url };

            return ResponseMessage(response);
        }

        // POST api/files/upload
        [Route("api/upload")]
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> PostFile()
        {
            var httpRequest = HttpContext.Current.Request;
            var file = httpRequest.Files["file"];

            if (file == null) return BadRequest();

            var fileName = httpRequest["fileName"];
            var fileDescription = httpRequest["fileDescription"];
            var fileTagsIds = httpRequest["fileTagsIds"];

            if (fileName == null || fileDescription == null || fileTagsIds == null)
                return BadRequest();

            var parsedTags = await ParseTags(fileTagsIds);

            if (parsedTags == null) return BadRequest();

            var fileDto = new FileDto
            {
                Name = fileName,
                Description = fileDescription,
                Tags = parsedTags
            };

            var newFileStream = file.InputStream;
            var createdFileId = await _fileService.SaveAsync(fileDto, newFileStream);

            return Created(fileDto.Name, createdFileId);
        }

        // PUT api/files/5
        [Route("api/upload/{id}")]
        [Authorize]
        [HttpPut]
        public async Task<IHttpActionResult> EditFile(int id)
        {
            var httpRequest = HttpContext.Current.Request;

            var fileName = httpRequest["fileName"];
            var fileDescription = httpRequest["fileDescription"];
            var fileTagsIds = httpRequest["fileTagsIds"];

            if (fileName == null || fileDescription == null || fileTagsIds == null)
                return BadRequest();

            var parsedTags = await ParseTags(fileTagsIds);

            if (parsedTags == null) return BadRequest();

            var fileDto = new FileDto
            {
                Name = fileName,
                Description = fileDescription,
                Tags = parsedTags
            };

            var file = httpRequest.Files["file"];

            if (file == null)
            {
                await _fileService.UpdateAsync(fileDto);
            }
            else
            {
                var newFileStream = file.InputStream;
                await _fileService.UpdateAsync(fileDto, newFileStream);
            }

            return Ok();
        }

        private async Task<ICollection<TagDto>> ParseTags(string fileTagsIdsValue)
        {
            var splitFileTagsIdsValue = fileTagsIdsValue
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(str => str.Trim()).ToList();

            // If unable to parse ids.
            if (splitFileTagsIdsValue.Count != 0 &&
                splitFileTagsIdsValue.Any(tag => !int.TryParse(tag, out var _)))
            {
                return null;
            }

            var fileTagsIds = splitFileTagsIdsValue.Select(int.Parse);

            return (await _tagService.FindByIdsAsync(fileTagsIds)).ToList();
        }
    }
}
