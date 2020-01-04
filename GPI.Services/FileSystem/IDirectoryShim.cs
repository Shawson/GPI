namespace GPI.Services.FileSystem
{
    public interface IDirectoryShim
    {
        string[] GetFiles(string path, bool recursive);
        bool Exists(string path);
    }
}