using System.IO;

namespace GPI.Services.FileSystem
{
    public class DirectoryShim : IDirectoryShim
    {
        public string[] GetFiles(string path, bool recursive)
        {
            return Directory.GetFiles(path, string.Empty, recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
        }
    }
}
