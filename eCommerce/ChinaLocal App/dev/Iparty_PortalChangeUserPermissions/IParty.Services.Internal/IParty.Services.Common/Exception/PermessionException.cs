using System.Runtime.Serialization;

namespace IParty.Services.Common
{
    public class PermessionException : System.Exception
    {
        public PermessionException() : base() { }
        public PermessionException(string message) : base(message) { }
        public PermessionException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public PermessionException(string message, System.Exception inner) : base(message, inner) { }
    }
}
