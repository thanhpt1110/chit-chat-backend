    using ChitChat.Application.Models;

namespace ChitChat.Application.Exceptions
{
    [Serializable]
    public abstract class ApplicationException : Exception
    {
        public ApiResultErrorCodes Code { get; protected set; }

        public bool TransactionRollback { get; protected set; } = true;

        public ApplicationException(string message) : base(message)
        {
        }
    }
}
