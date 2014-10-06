using System;
using System.Web.Mvc;
using DataSite.Code;
using DataSite.Code.Manager;
using DataSite.Code.Models;
using DataSite.Models.Admin;

namespace DataSite.Controllers
{
    [AuthorizeAdmin]
    public class AdminController : Controller
    {
        public ActionResult Add()
        {
            AdminEditModel model = new AdminEditModel();

            model.IsNew = true;
            model.Project = new ProjectItem();

            return View("EditProject", model);
        }

        public ActionResult Edit(Guid id)
        {
            AdminEditModel model = new AdminEditModel();

            model.IsNew = false;
            model.Project = ProjectManager.Get(id);
            if (model.Project == null)
                return RedirectToAction("Index", "Home");

            return View("EditProject", model);
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "save")]
        [ValidateInput(false)]
        public ActionResult SaveEdit(Guid? id, AdminEditModel model)
        {
            if (!id.HasValue)
                id = Guid.NewGuid();

            // Get old project
            ProjectItem oldProject = ProjectManager.Get(id.Value) ?? new ProjectItem();
            oldProject.Id = id.Value;

            // Transfer values
            oldProject.Name = model.Project.Name;
            oldProject.Abstract = model.Project.Abstract;
            oldProject.Text = model.Project.Text;

            // Save
            ProjectManager.Save(id.Value, oldProject);

            model.IsNew = false;
            model.Project = oldProject;

            return View("EditProject", model);
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "delete")]
        [ValidateInput(false)]
        public ActionResult Delete(AdminEditModel model)
        {
            return View("EditProject", model);
        }
    }
}