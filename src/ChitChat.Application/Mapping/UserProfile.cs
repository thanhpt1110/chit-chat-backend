using AutoMapper;
using ChitChat.Application.Models.Dtos.User;
using ChitChat.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitChat.Application.Mapping
{
    public class UserProfile: Profile
    {
        public UserProfile() {
            CreateMap<UserApplication, UserDto>();
            CreateMap<RegisterationRequestDto, UserApplication>();
            CreateMap<LoginRequestDto, UserApplication>();

        }
    }
}
