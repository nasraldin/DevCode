using System.IO;
using System.Text;

namespace DevCode.Extensions.IO
{
    public static class StreamExtensions
    {
        public static byte[] GetAllBytes(this Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// Write File in UTF8 from MemoryStream
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="path"></param>
        public static void WriteToFileUtf8(this MemoryStream stream, string path)
        {
            using (var writer = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
            {
                Encoding enc = new UTF8Encoding(false, false);
                var chars = enc.GetString(stream.ToArray());
                var bytes = enc.GetBytes(chars.ToCharArray());
                writer.Write(bytes, 0, bytes.Length);
            }
        }
    }
}
