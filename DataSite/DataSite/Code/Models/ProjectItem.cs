using System;
using System.Collections.Generic;

namespace DataSite.Code.Models
{
    [Serializable]
    public class ProjectItem
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime TimeCreated { get; set; }

        public DateTime TimeLastUpdate { get; set; }

        public string Abstract { get; set; }

        public string Text { get; set; }

        public List<ProjectFile> Files { get; set; }

        public ProjectItem()
        {
            Files = new List<ProjectFile>();
            TimeCreated = DateTime.UtcNow;
        }
    }
}