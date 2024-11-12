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
            CreateMap<UserApplication, ProfileDto>();
            CreateMap<ProfileDto, ChitChat.Domain.Entities.UserEntities.Profile>();
            CreateMap<ProfileRequestDto, ChitChat.Domain.Entities.UserEntities.Profile>();
            CreateMap<ChitChat.Domain.Entities.UserEntities.Profile, ProfileDto>()
               .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.UserApplication != null ? src.UserApplication.DisplayName : string.Empty))
               .ForMember(dest => dest.AvatarUrl, opt => opt.MapFrom(src => src.UserApplication != null ? src.UserApplication.AvatarUrl : string.Empty))
               .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.UserApplication != null ? src.UserApplication.Email : string.Empty))
               .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.UserApplication != null ? src.UserApplication.PhoneNumber : string.Empty));
            CreateMap<ProfileRequestDto, UserApplication>();
        }
    }
}
