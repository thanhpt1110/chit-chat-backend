using System.Security.Claims;

using ChitChat.Application.Helpers;

using Microsoft.AspNetCore.Http;

namespace ChitChat.Infrastructure.Services
{
    public class ClaimService : IClaimService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClaimService(IHttpContextAccessor httpContextAccessor) => this._httpContextAccessor = httpContextAccessor;

        public string GetUserId() => this.GetClaim(ClaimTypes.NameIdentifier);

        public string GetUserName() => this.GetClaim(ClaimTypes.Name);

        private string GetClaim(string key) => this._httpContextAccessor.HttpContext?.User?.FindFirst(key)?.Value ?? string.Empty;

    }
}
