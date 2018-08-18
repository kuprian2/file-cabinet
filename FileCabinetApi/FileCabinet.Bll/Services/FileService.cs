using AutoMapper;
using FileCabinet.Bll.Contracts.Dtos;
using FileCabinet.Bll.Contracts.Services;
using FileCabinet.Bll.Services.Base;
using FileCabinet.Bll.StorageServices;
using FileCabinet.Dal.Contracts.Repositories;
using FileCabinet.Dal.Contracts.UoW;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileCabinet.Dal.Contracts.Domain;
using File = FileCabinet.Dal.Contracts.Domain.File;

namespace FileCabinet.Bll.Services
{
    public class FileService : ComplexEntityService<FileDto, File>, IFileService
    {
        protected readonly IFileStorageService FileStorageService;
        protected readonly IRepository<Tag, int> TagRepository;

        public FileService(IUnitOfWork unitOfWork, IRepository<File, int> repository, IRepository<Tag, int> tagRepository,
            IMapper mapper, IFileStorageService fileStorageService)
            : base(unitOfWork, repository, mapper)
        {
            TagRepository = tagRepository;
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

            var tagIds = mappedFile.Tags.Select(x => x.Id);
            mappedFile.Tags = TagRepository.Find(tag => tagIds.Contains(tag.Id)).ToList();

            Repository.Create(mappedFile);
            UnitOfWork.SaveChanges();

            return mappedFile.Id;
        }

        public override async Task<int> SaveAsync(FileDto fileDto, Stream stream)
        {
            if (fileDto == null) throw new ArgumentNullException(nameof(fileDto));
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            var storageFilePath = await FileStorageService.CreateAsync(stream, fileDto.Name);

            var mappedFile = Mapper.Map<File>(fileDto);
            mappedFile.Url = storageFilePath;
            mappedFile.SizeInBytes = FileStorageService.GetInfo(storageFilePath).Length;
            mappedFile.UploadDate = DateTime.Now;

            var tagIds = mappedFile.Tags.Select(x => x.Id);
            mappedFile.Tags = (await TagRepository.FindAsync(tag => tagIds.Contains(tag.Id))).ToList();

            await Repository.CreateAsync(mappedFile);
            await UnitOfWork.SaveChangesAsync();

            return mappedFile.Id;
        }

        public override void Update(FileDto fileDto, Stream stream)
        {
            if (fileDto == null) throw new ArgumentNullException(nameof(fileDto));
            
            var file = Mapper.Map<File>(Get(fileDto.Id));

            if (stream != null)
            {
                FileStorageService.Delete(file.Url);
                file.Url = FileStorageService.Create(stream, fileDto.Name);
                file.SizeInBytes = FileStorageService.GetInfo(fileDto.Url).Length;
            }

            //var tagIds = fileDto.Tags.Select(x => x.Id).ToList();
            //file.Tags = TagRepository.Find(tag => tagIds.Contains(tag.Id)).ToList();
            file.Name = fileDto.Name;
            file.Description = fileDto.Description;

            Repository.Update(file);
            UnitOfWork.SaveChanges();
        }

        public override async Task UpdateAsync(FileDto fileDto, Stream stream)
        {
            if (fileDto == null) throw new ArgumentNullException(nameof(fileDto));

            var file = await Repository.GetAsync(fileDto.Id);

            if (stream != null)
            {
                FileStorageService.Delete(file.Url);
                file.Url = await FileStorageService.CreateAsync(stream, fileDto.Name);
                file.SizeInBytes = FileStorageService.GetInfo(file.Url).Length;
            }
            
            file.Tags = Mapper.Map<ICollection<Tag>>(fileDto.Tags);
            file.Name = fileDto.Name;
            file.Description = fileDto.Description;

            await Repository.UpdateAsync(file);
            await UnitOfWork.SaveChangesAsync();
        }

        public override Stream Read(FileDto fileDto)
        {
            if (fileDto == null) throw new ArgumentNullException(nameof(fileDto));

            var file = Get(fileDto.Id);
            return file == null
                ? null
                : FileStorageService.Read(file.Url);
        }

        public override async Task<Stream> ReadAsync(FileDto fileDto)
        {
            if (fileDto == null) throw new ArgumentNullException(nameof(fileDto));

            var file = await GetAsync(fileDto.Id);
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

        public new async Task DeleteAsync(int id)
        {
            var fileInfo = await GetAsync(id);
            if (fileInfo == null) return;

            FileStorageService.Delete(fileInfo.Url);
            await base.DeleteAsync(id);
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

        public async Task<IEnumerable<FileDto>> GetByTagsAsync(IEnumerable<TagDto> tagDtos)
        {
            if (tagDtos == null) return await GetAllAsync();

            var tagNames = tagDtos.Select(x => x.Name).ToList();

            var filteredFiles = (await Repository
                .FindAsync(file =>
                    !tagNames.Except(
                            file.Tags.Select(x => x.Name))
                        .Any()))
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

        public async Task<IEnumerable<FileDto>> GetByFilterAsync(string keyword)
        {
            var filteredFiles = (await Repository.FindAsync(file =>
                file.Tags.Any(tag => tag.Name.Contains(keyword)) ||
                file.Name.Contains(keyword) ||
                file.Description.Contains(keyword))).ToList();

            return Mapper.Map<IEnumerable<FileDto>>(filteredFiles);
        }
    }
}