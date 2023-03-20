using API.Dtos;
using API.Entities;
using AutoMapper;

namespace API.AutoMapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Product, ProductDto>().ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.UserName));
            CreateMap<Message, MessageDto>();
        }
    }
}