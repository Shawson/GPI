using System.Collections.Generic;

namespace GPI.Services.ContentHosts.Oculus.DataExtraction
{
    public interface IOculusPathSniffer
    {
        List<string> GetOculusLibraryLocations();
    }
}