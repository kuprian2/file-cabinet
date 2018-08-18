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
            //CreateMap<File, FileDto>()
            //    .ForMember(dest => dest.Tags, opt => opt.UseDestinationValue());

            //CreateMap<FileDto, File>()
            //    .ForMember(dest => dest.Tags, opt => opt.UseDestinationValue());

            //CreateMap<Tag, TagDto>()
            //    .ForMember(dest => dest.Files, opt => opt.UseDestinationValue());

            //CreateMap<TagDto, Tag>()
            //    .ForMember(dest => dest.Files, opt => opt.UseDestinationValue());

            //CreateMap<User, UserDto>()
            //    .ForMember(dest => dest.Bookmarks, opt => opt.UseDestinationValue());

            //CreateMap<UserDto, User>()
            //    .ForMember(dest => dest.Bookmarks, opt => opt.UseDestinationValue());
        }
    }
}
