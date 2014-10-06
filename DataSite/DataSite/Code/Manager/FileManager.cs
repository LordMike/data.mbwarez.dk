using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DataSite.Code.Models;

namespace DataSite.Code.Manager
{
    public static class FileManager
    {
        private const string FileBaseDir = "~/App_Data/Files";

        private static string GetProjectDir(Guid id)
        {
            return FileUtils.GetRealPath(Path.Combine(FileBaseDir, id.ToString()));
        }

        public static string GetFilePath(Guid projectId, Guid fileId)
        {
            return Path.Combine(GetProjectDir(projectId), fileId.ToString());
        }

        public static List<ProjectFile> GetFiles(Guid projectId)
        {
            List<ProjectFile> result = new List<ProjectFile>();

            ProjectItem model = ProjectManager.Get(projectId);

            string path = GetProjectDir(projectId);
            if (Directory.Exists(path))
            {
                // Enumerate
                string[] files = Directory.GetFiles(path);
                foreach (string file in files)
                {
                    Guid tmpGuid;
                    if (Guid.TryParse(Path.GetFileNameWithoutExtension(file), out tmpGuid))
                    {
                        ProjectFile fileItem = model.Files.FirstOrDefault(s => s.Id == tmpGuid);
                        if (fileItem != null)
                            result.Add(fileItem);
                    }
                }
            }

            return result;
        }

        public static ProjectFile GetFile(Guid projectId, Guid fileId)
        {
            ProjectItem model = ProjectManager.Get(projectId);

            if (model == null)
                return null;

            string path = GetFilePath(projectId, fileId);
            if (!File.Exists(path))
                return null;

            return model.Files.FirstOrDefault(s => s.Id == fileId);
        }

        public static void RemoveFile(Guid id, Guid fileId)
        {
            string path = GetFilePath(id, fileId);

            ProjectItem model = ProjectManager.Get(id);
            model.Files.RemoveAll(s => s.Id == fileId);

            ProjectManager.Save(id, model);

            FileUtils.DeleteFile(path);
        }

        public static void RemoveAllFiles(Guid id)
        {
            string dir = GetProjectDir(id);

            FileUtils.DeleteDirectory(dir, true);
        }

        public static void AddFile(Guid projectId, string name, string extension, byte[] data)
        {
            Guid fileId = Guid.NewGuid();

            string path = GetFilePath(projectId, fileId);

            name = Path.ChangeExtension(name, extension);

            ProjectItem model = ProjectManager.Get(projectId);
            model.Files.Add(new ProjectFile { Id = fileId, Name = name, Length = data.Length });

            ProjectManager.Save(projectId, model);

            FileUtils.SaveFile(path, data);
        }
    }
}