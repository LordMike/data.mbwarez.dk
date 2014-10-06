using System;
using System.Web.Mvc;
using DataSite.Code;
using DataSite.Code.Manager;
using DataSite.Code.Models;
using DataSite.Models.Admin;
using DataSite.Models.Project;

namespace DataSite.Controllers
{
    [AuthorizeAdmin]
    public class AdminController : Controller
    {
        public ActionResult Add()
        {
            AdminEditModel model = new AdminEditModel();

            model.Project = new ProjectItem();

            return View("EditProject", model);
        }

        public ActionResult Edit(Guid id)
        {
            AdminEditModel model = new AdminEditModel();

            model.Project = ProjectManager.Get(id);
            if (model.Project == null)
                return RedirectToAction("Index", "Home");

            return View("EditProject", model);
        }
    }
}