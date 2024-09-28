using ChitChat.Application.Models.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitChat.Application.Services.Interface
{
    public interface IUserService
    {
        Task<bool> RegisterAsync(RegisterationRequestDto registerationRequestDto);
        Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
        Task<bool> LogoutAsync(Guid loginHistoryId);
    }
}
