using ChitChat.Domain.Identity;
using System.Security.Claims;

namespace ChitChat.Application.Helpers
{
    public interface ITokenService
    {
        (string token, int validDays) GenerateRefreshToken();

        Task<ClaimsIdentity> GetPrincipalFromExpiredToken(string? token);

        string GenerateAccessToken(UserApplication user, IEnumerable<string> roles);
    }
}
