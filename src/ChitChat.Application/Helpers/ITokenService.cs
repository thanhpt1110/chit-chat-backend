using ChitChat.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ChitChat.Application.Helpers
{
    public interface ITokenService
    {
        (string token, int validDays) GenerateRefreshToken();

        Task<ClaimsIdentity> GetPrincipalFromExpiredToken(string? token);

        string GenerateAccessToken(UserApplication user, IEnumerable<string> roles);
    }
}
