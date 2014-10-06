using System;
using System.Web;
using System.Web.Mvc;
using DataSite.Code.Manager;

namespace DataSite.Code
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AuthorizeAdminAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return AdminManager.CanAdmin(httpContext);
        }
    }
}