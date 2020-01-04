namespace GPI.Services.FileSystem
{
    public interface IFileShim
    {
        bool Exists(string path);
    }
}