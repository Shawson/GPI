﻿using System.Collections.Generic;
using Microsoft.Win32;

namespace GPI.Services.OS
{
    public interface IRegistryValueProvider
    {
        List<string> GetSubKeysForPath(RegistryView platform, RegistryHive hive, string path);
        string GetValueForPath(RegistryView platform, RegistryHive hive, string path, string keyName);
    }
}