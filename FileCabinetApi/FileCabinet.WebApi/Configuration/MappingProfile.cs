using AutoMapper;
using FileCabinet.Bll.Contracts.Dtos;
using FileCabinet.WebApi.Models;
using System;
using System.IO;
using System.Linq;

namespace FileCabinet.WebApi.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<FileDto, FileInfoModel>();

            CreateMap<TagDto, TagModel>().ReverseMap();

            CreateMap<FileCreateModel, FileDto>()
                .ForMember(dest => dest.UploadDate, opt =>
                    opt.UseValue(DateTime.Now))
                .ForMember(dest => dest.Url,
                    opt => opt.MapFrom(src => FormFullPath(src.Name)));

            CreateMap<FileUpdateModel, FileDto>()
                .ForMember(dest => dest.Url,
                    opt => opt.MapFrom(src => FormFullPath(src.Name)));
        }

        public string FormFullPath(string fileName)
        {
            var normalizedFileName = new string(
                    fileName.
                        Trim()
                        .ToCharArray()
                        .Except(Path.GetInvalidFileNameChars())
                        .ToArray())
                .ToLowerInvariant()
                .Replace(' ', '-');

            return Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                normalizedFileName);
        }
    }
}