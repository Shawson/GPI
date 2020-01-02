using System;

namespace GPI.Services.OS
{
    public interface IPathNormaliser : IDisposable
    {
        string Normalise(string path);
    }
}