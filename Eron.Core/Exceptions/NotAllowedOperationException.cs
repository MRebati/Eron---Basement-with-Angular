using System;
using System.Runtime.Serialization;

namespace Eron.Core.Exceptions
{
    public class NotAllowedOperationException : Exception
    {
        public NotAllowedOperationException()
        {
        }

        public NotAllowedOperationException(string message) : base(message)
        {
        }

        public NotAllowedOperationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotAllowedOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}