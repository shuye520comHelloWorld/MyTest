using ServiceStack;

namespace iParty.Services.IntegrationTests
{
    public static class Helper
    {
        public static JsonServiceClient Client { get; private set; }

        static Helper()
        {
            Client = new JsonServiceClient("http://localhost:8991/v1");
        }
    }
}
