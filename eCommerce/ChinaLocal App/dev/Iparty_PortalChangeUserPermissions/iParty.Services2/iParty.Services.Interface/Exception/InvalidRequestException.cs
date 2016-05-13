using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Services.Interface.Exception
{
    public class InvalidRequestException : System.Exception
    {
        public InvalidRequestException() : base() { }
        public InvalidRequestException(string message) : base(message) { }
        public InvalidRequestException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public InvalidRequestException(string message, System.Exception inner) : base(message, inner) { }

    }
}
