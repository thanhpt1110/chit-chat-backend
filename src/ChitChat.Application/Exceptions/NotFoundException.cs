using ChitChat.Application.Localization;
using ChitChat.Application.Models;

namespace ChitChat.Application.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(Guid id, Type type) : base(ValidationTexts.NotFound.Format(type.Name, id.ToString()))
        {
            Code = ApiResultErrorCodes.NotFound;
        }

        public NotFoundException(string code, Type type) : base(ValidationTexts.NotFound.Format(type.Name, code))
        {
            Code = ApiResultErrorCodes.NotFound;
        }

        public NotFoundException(string message): base(message)
        {
            Code = ApiResultErrorCodes.NotFound;
        }

        public NotFoundException(int count, Type type) : base(ValidationTexts.NotFound.Format(count, type.Name))
        {
            Code = ApiResultErrorCodes.NotFound;
        }
    }
}