
namespace iParty.Services.Interface
{
    public class OAuthToken
    {
        public string Token { get; set; }
        public string ResponseCode { get; set; }
        public long? User { get; set; }
        public string Scope { get; set; }
        public string ExpirationDateUTC { get; set; }
    }
}
