using System.Runtime.Serialization;

namespace PinkBus.Services.Common
{
    public class NotFoundException : System.Exception
    {
        public NotFoundException() : base() { }
        public NotFoundException(string message) : base(message) { }
        public NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public NotFoundException(string message, System.Exception inner) : base(message, inner) { }
    }
}
