using System.IO;

namespace GPI.Services.FileSystem
{
    public class FileShim : IFileShim
    {
        public bool Exists(string path)
        {
            return File.Exists(path);
        }
    }
}
