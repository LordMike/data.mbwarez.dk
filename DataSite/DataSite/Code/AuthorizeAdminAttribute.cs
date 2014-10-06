using System.Web;
using System.Web.Mvc;
using DataSite.Code.Manager;

namespace DataSite.Code
{
    public class AuthorizeAdminAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool isAuthorized = base.AuthorizeCore(httpContext);
            if (!isAuthorized)
                return false;

            return AdminManager.CanAdmin(httpContext);
        }
    }
}