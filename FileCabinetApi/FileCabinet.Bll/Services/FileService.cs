using AutoMapper;
using FileCabinet.Bll.Contracts.Dtos;
using FileCabinet.Bll.Contracts.Services;
using FileCabinet.Bll.Services.Base;
using FileCabinet.Dal.Contracts.Repositories;
using FileCabinet.Dal.Contracts.UoW;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FileCabinet.Bll.StorageServices;
using File = FileCabinet.Dal.Contracts.Domain.File;

namespace FileCabinet.Bll.Services
{
    public class FileService : ServerEntityService<FileDto, File>, IFileService
    {
        protected readonly IFileStorageService FileStorageService;

        public FileService(IUnitOfWork unitOfWork, IRepository<File, int> repository, IMapper mapper,
            IFileStorageService fileStorageService)
            : base(unitOfWork, repository, mapper)
        {
            FileStorageService = fileStorageService;
        }

        public override int Save(FileDto fileDto, Stream stream)
        {
            if (fileDto == null) throw new ArgumentNullException(nameof(fileDto));
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            var storageFilePath = FileStorageService.Create(stream, fileDto.Name);

            var mappedFile = Mapper.Map<File>(fileDto);
            mappedFile.Url = storageFilePath;
            mappedFile.SizeInBytes = FileStorageService.GetInfo(storageFilePath).Length;
            mappedFile.UploadDate = DateTime.Now;

            Repository.Create(mappedFile);
            UnitOfWork.SaveChanges();

            return mappedFile.Id;
        }

        public override void Update(FileDto fileDto, Stream stream)
        {
            if (fileDto == null) throw new ArgumentNullException(nameof(fileDto));
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            var fileInfo = Get(fileDto.Id);

            FileStorageService.Delete(fileInfo.Url);
            fileDto.Url = FileStorageService.Create(stream, fileDto.Name);

            base.Update(fileDto);
        }

        public override Stream Read(FileDto fileDto)
        {
            if (fileDto == null) throw new ArgumentNullException(nameof(fileDto));

            var file = Get(fileDto.Id);
            return file == null
                ? null
                : FileStorageService.Read(file.Url);
        }

        public new void Delete(int id)
        {
            var fileInfo = Get(id);
            if (fileInfo == null) return;

            FileStorageService.Delete(fileInfo.Url);
            base.Delete(id);
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