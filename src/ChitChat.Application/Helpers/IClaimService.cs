using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ChitChat.Application.Helpers
{
    public interface IClaimService
    {
        string GetUserId();
        string GetUserName();
    }
}
