using ServiceStack.Web;

namespace iParty.Services.Infrastructure
{
   public static class ExtendServiceStack
    {
      public static void TerminateWith(this IResponse res, int errorcode, string message)
       {
           res.StatusCode = errorcode;
           res.AddHeader("WWW-Authenticate", message);
           res.End();
       }
    }
}
