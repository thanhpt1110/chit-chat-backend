using System.Security.Claims;

namespace ChitChat.Application.Helpers
{
    public interface IClaimService
    {
        string GetUserId();
        string GetUserName();
        string GetLoginHistoryId(ClaimsIdentity? claimsIdentity = null);

    }
}
