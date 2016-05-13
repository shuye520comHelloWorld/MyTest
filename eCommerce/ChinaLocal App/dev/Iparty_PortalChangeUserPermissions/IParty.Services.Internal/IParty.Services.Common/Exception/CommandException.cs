using System.Runtime.Serialization;

namespace IParty.Services.Common
{
    public class CommandException : System.Exception
    {
        public CommandException() : base() { }
        public CommandException(string message) : base(message) { }
        public CommandException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public CommandException(string message, System.Exception inner) : base(message, inner) { }
    }
}
