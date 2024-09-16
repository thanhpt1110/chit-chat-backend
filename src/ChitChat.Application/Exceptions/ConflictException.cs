    using ChitChat.Application.Models;

namespace ChitChat.Application.Exceptions;
public class ConflictException : ApplicationException
{
    public ConflictException(string message, bool transactionRollback = true) : base(message)
    {
        Code = ApiResultErrorCodes.Conflict;
        TransactionRollback = transactionRollback;
    }
}
