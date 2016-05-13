using System.Runtime.Serialization;

namespace PinkBus.Services.Common.Exception
{
    public class NotFoundWithNoEmailException: System.Exception
    {
        public NotFoundWithNoEmailException() : base() { }
        public NotFoundWithNoEmailException(string message) : base(message) { }
        public NotFoundWithNoEmailException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public NotFoundWithNoEmailException(string message, System.Exception inner) : base(message, inner) { }
    }
}
