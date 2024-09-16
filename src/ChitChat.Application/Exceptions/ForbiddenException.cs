using ChitChat.Application.Models;

namespace ChitChat.Application.Exceptions
{
    public class ForbiddenException : ApplicationException
    {
        public ForbiddenException(string message) : base(message) 
        {
            Code = ApiResultErrorCodes.Forbidden;
        }
    }
}
