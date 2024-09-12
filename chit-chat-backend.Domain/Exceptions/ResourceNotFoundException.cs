using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace chit_chat_backend.Domain.Exceptions
{
    [Serializable]
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException() { }

        public ResourceNotFoundException(Type type) : base($"{type} is missing") { }

        protected ResourceNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public ResourceNotFoundException(string? message) : base(message) { }

        public ResourceNotFoundException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
