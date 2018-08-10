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
            CreateMap<TagDto, TagInfoModel>().ReverseMap();
            CreateMap<TagCreateModel, TagDto>();
            CreateMap<TagUpdateModel, TagDto>();

            CreateMap<FileDto, FileInfoModel>();
            CreateMap<FileUpdateModel, FileDto>();
            CreateMap<FileCreateModel, FileDto>();
            
            CreateMap<UserDto, UserInfoModel>();
            CreateMap<UserUpdateModel, UserDto>();
            CreateMap<UserCreateModel, UserDto>();
        }
    }
}