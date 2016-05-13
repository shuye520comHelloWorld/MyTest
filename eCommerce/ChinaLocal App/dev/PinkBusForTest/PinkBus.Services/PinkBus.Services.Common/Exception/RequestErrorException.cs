using System.Runtime.Serialization;

namespace PinkBus.Services.Common
{
    public class RequestErrorException : System.Exception
    {
        public RequestErrorException() : base() { }
        public RequestErrorException(string message) : base(message) { }
        public RequestErrorException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public RequestErrorException(string message, System.Exception inner) : base(message, inner) { }
    }
}
