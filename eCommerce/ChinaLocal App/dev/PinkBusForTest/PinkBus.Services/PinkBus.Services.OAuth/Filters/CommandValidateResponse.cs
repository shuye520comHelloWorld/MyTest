using ServiceStack;
namespace PinkBus.Services.OAuth.Filters
{
    public class CommandValidateResponse
    {
        public bool Result { get; set; }
        public string Message { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }
}
