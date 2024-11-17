using ChitChat.Application.Models.Dtos.User;

namespace ChitChat.Application.Services.Interface
{
    public interface IUserService
    {
        Task<bool> RegisterAsync(RegisterationRequestDto registerationRequestDto);
        Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
        Task<RefreshTokenDto> RefreshTokenAsync(RefreshTokenDto refreshTokenDto);
        Task<bool> LogoutAsync(Guid loginHistoryId);
    }
}
