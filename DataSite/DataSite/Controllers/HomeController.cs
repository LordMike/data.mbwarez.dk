using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using DataSite.Code.Manager;
using DataSite.Code.Models;
using DataSite.Models.Home;

namespace DataSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Demo()
        {
            ProjectItem pr = new ProjectItem();
            pr.Id = Guid.NewGuid();
            pr.Name = "Test project";
            pr.Files.Add(new ProjectFile { Id = Guid.NewGuid(), Name = "Testfile", Extension = ".torrent", Length = 87634 });
            pr.TimeLastUpdate = DateTime.UtcNow;

            ProjectManager.Save(pr.Id, pr);

            return RedirectToAction("Index");
        }

        public ActionResult Index()
        {
            HomeIndexModel model = new HomeIndexModel();

            model.ProjectsCount = ProjectManager.GetCount();

            return View(model);
        }
    }
}
