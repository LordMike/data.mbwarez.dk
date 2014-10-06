using DataSite.Code.Models;

namespace DataSite.Models.Admin
{
    public class AdminEditProjectModel
    {
        public bool IsNew { get; set; }

        public ProjectItem Project { get; set; }
    }
}