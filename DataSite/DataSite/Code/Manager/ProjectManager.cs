using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml;
using DataSite.Code.Models;

namespace DataSite.Code.Manager
{
    public static class ProjectManager
    {
        private const string RootPath = "~/App_Data/Projects";

        public static int GetCount()
        {
            return FileUtils.ListFiles(RootPath).Length;
        }

        private static string GetPath(Guid projectId)
        {
            return Path.Combine(RootPath, projectId.ToString());
        }

        public static List<ProjectItem> List()
        {
            string[] files = FileUtils.ListFiles(RootPath);

            List<ProjectItem> res = new List<ProjectItem>();
            foreach (string file in files)
            {
                // Try loading file
                try
                {
                    Guid tmpId;
                    if (Guid.TryParse(Path.GetFileName(file), out tmpId))
                    {
                        ProjectItem item = Get(tmpId);

                        if (item != null)
                            res.Add(item);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Unable to load {0}: {1}", file, ex.Message);
                }
            }

            return res;
        }

        public static ProjectItem Get(Guid id)
        {
            string path = FileUtils.GetRealPath(GetPath(id));

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);

                return Serializer.Deserialize<ProjectItem>(doc);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static void Save(Guid id, ProjectItem project)
        {
            string path = GetPath(id);

            using (MemoryStream ms = new MemoryStream())
            {
                Serializer.Serialize(project).Save(ms);

                byte[] data = ms.ToArray();
                FileUtils.SaveFile(path, data);
            }
        }
    }
}