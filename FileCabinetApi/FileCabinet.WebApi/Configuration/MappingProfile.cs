using AutoMapper;
using FileCabinet.Bll.Contracts.Dtos;
using FileCabinet.WebApi.Models;

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
            CreateMap<UserDto, UserInfoModel>();
        }
    }
}