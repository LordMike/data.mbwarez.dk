using DataSite.Code.Models;

namespace DataSite.Models.Admin
{
    public class AdminEditModel
    {
        public bool IsNew { get; set; }

        public ProjectItem Project { get; set; }
    }
}