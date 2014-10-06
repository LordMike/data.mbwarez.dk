using System.Web;

namespace DataSite.Code.Manager
{
    public static class AdminManager
    {
        public static bool CanAdmin(HttpContextBase context)
        {
            return context.Request.IsLocal;
        }
    }
}