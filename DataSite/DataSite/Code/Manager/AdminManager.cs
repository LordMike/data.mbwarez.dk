using System.Linq;
using System.Web;

namespace DataSite.Code.Manager
{
    public static class AdminManager
    {
        const string CfIp = "CF-Connecting-IP";
        const string MikesIp = "87.73.127.4";

        public static bool CanAdmin(HttpContextBase context)
        {
            return context.Request.IsLocal ||
                context.Request.UserHostAddress == MikesIp || (context.Request.Headers.AllKeys.Contains(CfIp) && context.Request.Headers[CfIp] == MikesIp);
        }

        public static bool CanAdmin(HttpContext context)
        {
            return context.Request.IsLocal ||
                context.Request.UserHostAddress == MikesIp || (context.Request.Headers.AllKeys.Contains(CfIp) && context.Request.Headers[CfIp] == MikesIp);
        }
    }
}