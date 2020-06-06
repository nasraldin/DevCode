using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DevCode.Extensions.IO
{
    /// <summary>
    /// A helper class for Directory operations.
    /// </summary>
    public static class DirectoryHelper
    {
        /// <summary>
        /// Creates a new directory if it does not exists.
        /// </summary>
        /// <param name="directory">Directory to create</param>
        public static void CreateIfNotExists(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        /// <summary>
        /// Recursively create directory
        /// </summary>
        /// <param name="dirInfo">Folder path to create.</param>
        public static void CreateDirectory(this DirectoryInfo dirInfo)
        {
            if (dirInfo.Parent != null) CreateDirectory(dirInfo.Parent);
            if (!dirInfo.Exists) dirInfo.Create();
        }

        /// <summary>
        /// Gets the total size of a specified folder. It can also check sizes of subdirectory under it as a parameter.
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="bIncludeSub"></param>
        /// <returns></returns>
        public static long FolderSize(this DirectoryInfo dir, bool bIncludeSub)
        {
            long totalFolderSize = 0;

            if (!dir.Exists) return 0;

            var files = from f in dir.GetFiles()
                        select f;
            foreach (var file in files) totalFolderSize += file.Length;

            if (bIncludeSub)
            {
                var subDirs = from d in dir.GetDirectories()
                              select d;
                foreach (var subDir in subDirs) totalFolderSize += FolderSize(subDir, true);
            }

            return totalFolderSize;
        }

        /// <summary>
        /// Get all files in a specified directory using. Doesn't include sub-directory files.
        /// </summary>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        public static List<string> ListFiles(this string folderPath)
        {
            if (!Directory.Exists(folderPath)) return null;
            return (from f in Directory.GetFiles(folderPath) select Path.GetFileName(f)).ToList();
        }

        /// <summary>
        /// Delete all files found on the specified folder with a given file extension.
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="ext"></param>
        public static void DeleteFiles(this string folderPath, string ext)
        {
            string mask = "*." + ext;

            try
            {
                string[] fileList = Directory.GetFiles(folderPath, mask);

                foreach (string file in fileList)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    fileInfo.Delete();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Deleting file. Reason: {0}", ex);
            }
        }
    }
}