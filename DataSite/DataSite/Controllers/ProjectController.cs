using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using DataSite.Code.Manager;
using DataSite.Code.Models;
using DataSite.Models.Project;

namespace DataSite.Controllers
{
    public class ProjectController : Controller
    {
        public ActionResult List()
        {
            ProjectListModel model = new ProjectListModel();

            model.Projects = ProjectManager.List().OrderByDescending(s => s.TimeLastUpdate).ToList();

            return View(model);
        }

        public ActionResult View(Guid id)
        {
            ProjectViewModel model = new ProjectViewModel();

            model.Project = ProjectManager.Get(id);
            if (model.Project == null)
                return RedirectToAction("List");

            return View(model);
        }

        public ActionResult DownloadFile(Guid id, Guid projectId)
        {
            ProjectFile file = FileManager.GetFile(projectId, id);

            if (file == null)
                return HttpNotFound();

            FileStream fs = System.IO.File.OpenRead(FileManager.GetFilePath(projectId, id));
            return new FileStreamResult(fs, "application/octect-stream") { FileDownloadName = file.Name };

        }
    }
}