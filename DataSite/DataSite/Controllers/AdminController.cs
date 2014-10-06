using System;
using System.IO;
using System.Web;
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
            AdminEditProjectModel model = new AdminEditProjectModel();

            model.IsNew = true;
            model.Project = new ProjectItem();

            return View("EditProject", model);
        }

        public ActionResult EditProject(Guid id)
        {
            AdminEditProjectModel model = new AdminEditProjectModel();

            model.IsNew = false;
            model.Project = ProjectManager.Get(id);
            if (model.Project == null)
                return RedirectToAction("Index", "Home");

            return View("EditProject", model);
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "save")]
        [ValidateInput(false)]
        public ActionResult EditProjectSave(Guid? id, AdminEditProjectModel model)
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
        public ActionResult EditProjectDelete(Guid id)
        {
            ProjectManager.Delete(id);

            return RedirectToAction("List", "Project");
        }

        public ActionResult EditFiles(Guid id)
        {
            AdminEditFilesModel model = new AdminEditFilesModel();

            model.Project = ProjectManager.Get(id);
            if (model.Project == null)
                return RedirectToAction("Index", "Home");

            return View(model);
        }

        public ActionResult DeleteFile(Guid id, Guid projectid)
        {
            throw new NotImplementedException();
        }

        public ActionResult AddFile(Guid id)
        {
            if (Request.Files.Count > 0)
            {
                // Add file
                HttpPostedFileBase file = Request.Files[0];

                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);

                    FileManager.AddFile(id, file.FileName, Path.GetExtension(file.FileName), ms.ToArray());
                }
            }

            return RedirectToAction("EditFiles", new { id });
        }
    }
}