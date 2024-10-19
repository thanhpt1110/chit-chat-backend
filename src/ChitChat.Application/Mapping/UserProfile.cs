using AutoMapper;
using ChitChat.Application.Models.Dtos.User;
using ChitChat.Domain.Identity;

namespace ChitChat.Application.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserApplication, UserDto>();
            CreateMap<RegisterationRequestDto, UserApplication>();
            CreateMap<LoginRequestDto, UserApplication>();
        }
    }
}
