using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Web;

namespace DataSite.Code
{
    public static class FileUtils
    {
        public static string GetRealPath(string path)
        {
            return HttpContext.Current.Server.MapPath(path);
        }

        public static void SaveFile(string path, byte[] data)
        {
            string realPath = GetRealPath(path);
            string dir = Path.GetDirectoryName(realPath);

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            File.WriteAllBytes(realPath, data);
        }

        public static void DeleteFile(string path)
        {
            string realPath = GetRealPath(path);
            try
            {
                if (File.Exists(realPath))
                {
                    File.Delete(realPath);

                    // Cleanup empty dirs
                    DeleteDirectory(Path.GetDirectoryName(realPath));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to delete path {0}, ex: {1}", realPath, ex.Message);
            }
        }

        public static void DeleteDirectory(string path, bool recursive = false)
        {
            string realPath = GetRealPath(path);
            try
            {
                if (Directory.Exists(realPath))
                    Directory.Delete(realPath, recursive);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to delete dir {0}, ex: {1}", realPath, ex.Message);
            }
        }

        public static string[] ListFiles(string path)
        {
            string realPath = GetRealPath(path);

            if (Directory.Exists(realPath))
                return Directory.GetFiles(realPath);

            return new string[0];
        }
    }
}