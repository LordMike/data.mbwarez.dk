using System;

namespace DataSite.Code.Models
{
    [Serializable]
    public class ProjectFile
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Extension { get; set; }

        public long Length { get; set; }
    }
}