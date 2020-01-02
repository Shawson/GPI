using System.Collections.Generic;
using System.Management;

namespace GPI.Services.OS
{
    public interface IWMODriveQueryProvider
    {
        List<WMODrive> GetDriveData();
    }
}