using System;
using System.Security.Principal;
using System.Threading;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using DataSite.Code.Manager;

namespace DataSite
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            if (AdminManager.CanAdmin(Context))
            {
                //Obtain username and roles from application datastore and use them in the next line
                Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity("MikeKbh"),
                    new string[] { "Elmah" }
                );
            }
        }
    }
}