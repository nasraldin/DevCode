using System;
using System.IO;
using System.Security.Cryptography;

namespace DevCode.Extensions.IO
{
    /// <summary>
    /// A helper class for File operations.
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// Checks and deletes given file if it does exists.
        /// </summary>
        /// <param name="filePath">Path of the file</param>
        public static void DeleteIfExists(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        /// <summary>
        /// Ensures given file name will return a unique file name, using the format Filename - Copy, or Filename - Copy (n) where n > 1
        /// </summary>
        /// <param name="intendedName"></param>
        /// <returns></returns>
        public static string EnsureFileNameIsUnique(this string intendedName)
        {
            if (!File.Exists(intendedName)) return intendedName;

            FileInfo file = new FileInfo(intendedName);
            string extension = file.Extension;
            string basePath = intendedName.Substring(0, intendedName.Length - extension.Length);

            int counter = 1;

            string newPath;
            do
            {
                newPath = string.Format("{0} - Copy{1}{2}", basePath, counter == 1 ? "" : string.Format(" ({0})", counter), extension);

                counter += 1;
            }
            while (File.Exists(newPath));

            return newPath;
        }
        /// <summary>
        /// Read and get MD5 hash value of any given filename.
        /// </summary>
        /// <param name="filename">full path and filename</param>
        /// <returns>lowercase MD5 hash value</returns>
        public static string GetMD5(this string filename)
        {
            string hashData;

            FileStream fileStream;
            MD5CryptoServiceProvider md5Provider = new MD5CryptoServiceProvider();

            string result;
            try
            {
                fileStream = GetFileStream(filename);
                byte[] arrByteHashValue = md5Provider.ComputeHash(fileStream);
                fileStream.Close();

                hashData = BitConverter.ToString(arrByteHashValue).Replace("-", "");
                result = hashData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error get MD5. Reason: {0}", ex);
            }

            return (result.ToLower());
        }

        private static FileStream GetFileStream(string pathName)
        {
            return (new FileStream(pathName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
        }
    }
}
