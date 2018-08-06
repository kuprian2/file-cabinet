using AutoMapper;
using FileCabinet.Bll.Contracts.Dtos;
using FileCabinet.Dal.Contracts.Domain;

namespace FileCabinet.Bll.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Tag, TagDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<File, FileDto>().ReverseMap();
        }
    }
}
