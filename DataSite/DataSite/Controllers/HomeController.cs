using System.Web.Mvc;
using DataSite.Code.Manager;
using DataSite.Models.Home;

namespace DataSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("List", "Project");

            HomeIndexModel model = new HomeIndexModel();

            model.ProjectsCount = ProjectManager.GetCount();

            return View(model);
        }

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult Error404()
        {
            return View();
        }
    }
}
