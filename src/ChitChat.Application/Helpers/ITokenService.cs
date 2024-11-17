using System.Security.Claims;

using ChitChat.Domain.Entities;
using ChitChat.Domain.Identity;

namespace ChitChat.Application.Helpers
{
    public interface ITokenService
    {
        (string token, int validDays) GenerateRefreshToken();

        Task<ClaimsIdentity> GetPrincipalFromExpiredToken(string? token);

        string GenerateAccessToken(UserApplication user, IEnumerable<string> roles, LoginHistory loginHistory);
    }
}
