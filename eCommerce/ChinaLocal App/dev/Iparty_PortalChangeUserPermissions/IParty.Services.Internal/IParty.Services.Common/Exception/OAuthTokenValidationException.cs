using System.Runtime.Serialization;

namespace IParty.Services.Common
{
    public class OAuthTokenValidationException : System.Exception
    {
        public OAuthTokenValidationException() : base() { }
        public OAuthTokenValidationException(string message) : base(message) { }
        public OAuthTokenValidationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public OAuthTokenValidationException(string message, System.Exception inner) : base(message, inner) { }
    }
}
