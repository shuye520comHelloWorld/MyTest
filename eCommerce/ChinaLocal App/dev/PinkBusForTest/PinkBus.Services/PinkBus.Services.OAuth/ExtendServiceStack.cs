using ServiceStack.Web;

namespace PinkBus.Services.OAuth
{
   public static class ExtendServiceStack
    {
       public static void TerminateWith(this IResponse res, int errorcode, string message)
       {
           res.StatusCode = errorcode;
           res.AddHeader("WWW-Authenticate", message);
           res.AddHeader("Access-Control-Allow-Origin", "*");
           res.AddHeader("Access-Control-Allow-Methods", "POST,PUT, GET, OPTIONS");
           res.AddHeader("Access-Control-Allow-Headers", "content-type");
           res.End();
       }
    }
}
