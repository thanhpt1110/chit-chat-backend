using ChitChat.Application.Models;

namespace ChitChat.Application.Exceptions;

[Serializable]
public class InvalidModelException : ApplicationException
{
    public InvalidModelException(string message, bool transactionRollback = true) : base(message)
    {
        Code = ApiResultErrorCodes.ModelValidation;
        TransactionRollback = transactionRollback;
    }
}