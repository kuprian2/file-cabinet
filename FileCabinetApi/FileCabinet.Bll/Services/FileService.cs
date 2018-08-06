using AutoMapper;
using FileCabinet.Bll.Contracts.Dtos;
using FileCabinet.Bll.Contracts.Services;
using FileCabinet.Bll.Services.Base;
using FileCabinet.Dal.Contracts.Domain;
using FileCabinet.Dal.Contracts.Repositories;
using FileCabinet.Dal.Contracts.UoW;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileCabinet.Bll.Services
{
    public class FileService : Service<FileDto, File>, IFileService
    {
        public FileService(IUnitOfWork unitOfWork, IRepository<File, int> repository, IMapper mapper)
            : base(unitOfWork, repository, mapper)
        {
        }

        public IEnumerable<FileDto> GetByTags(IEnumerable<TagDto> tagDtos)
        {
            if (tagDtos == null) return GetAll();

            var tagNames = tagDtos.Select(x => x.Name);

            var filteredFiles = Repository
                .Find(file => 
                    !tagNames.Except(
                        file.Tags.Select(x => x.Name))
                    .Any())
                .ToList();

            return Mapper.Map<IEnumerable<FileDto>>(filteredFiles);
        }

        private bool IsTaggedWithAll(File file, IEnumerable<TagDto> tags)
        {
            var tagNames = tags.Select(x => x.Name);
            var fileTagNames = file.Tags.Select(x => x.Name);
            return !tagNames.Except(fileTagNames).Any();
        }

        public IEnumerable<FileDto> GetByFilter(string keyword)
        {
            var filteredFiles = Repository.Find(file =>
                file.Tags.Any(tag => tag.Name.Contains(keyword)) ||
                file.Name.Contains(keyword) ||
                file.Description.Contains(keyword)).ToList();

            return Mapper.Map<IEnumerable<FileDto>>(filteredFiles);
        }
    }
}