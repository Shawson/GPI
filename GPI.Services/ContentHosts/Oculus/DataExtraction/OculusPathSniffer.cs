using GPI.Services.OS;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPI.Services.ContentHosts.Oculus.DataExtraction
{
    public class OculusPathSniffer : IOculusPathSniffer
    {
        private readonly IRegistryValueProvider registryValueProvider;
        private readonly IPathNormaliser pathNormaliser;
        private readonly ILogger<OculusPathSniffer> logger;

        public OculusPathSniffer(
            IRegistryValueProvider registryValueProvider,
            IPathNormaliser pathNormaliser,
            ILogger<OculusPathSniffer> logger)
        {
            this.registryValueProvider = registryValueProvider;
            this.pathNormaliser = pathNormaliser;
            this.logger = logger;
        }

        private List<string> GetOculusLibraryLocations(RegistryView platformView)
        {
            var libraryPaths = new List<string>();

            logger.LogDebug($"Getting Oculus library locations from registry ({platformView})");

            try
            {
                var libraryKeyTitles = registryValueProvider.GetSubKeysForPath(platformView,
                                                                                RegistryHive.CurrentUser,
                                                                                @"Software\Oculus VR, LLC\Oculus\Libraries\");

                if (libraryKeyTitles == null || !libraryKeyTitles.Any())
                {
                    logger.LogError("No libraries found");
                    return null;
                }

                foreach (var libraryKeyTitle in libraryKeyTitles)
                {
                    var libraryPath = registryValueProvider.GetValueForPath(platformView,
                                                                            RegistryHive.CurrentUser,
                                                                            $@"Software\Oculus VR, LLC\Oculus\Libraries\{libraryKeyTitle}",
                                                                            "Path");

                    if (!string.IsNullOrWhiteSpace(libraryPath))
                    {
                        libraryPath = pathNormaliser.Normalise(libraryPath);
                        libraryPaths.Add(libraryPath);
                        logger.LogDebug($"Found library: {libraryPath}");
                    }
                }

                logger.LogDebug($"Libraries located: {libraryPaths.Count}");

                return libraryPaths;
            }
            catch (Exception ex)
            {
                logger.LogError($"Exception opening registry keys: {ex}");
                return null;
            }
        }

        public List<string> GetOculusLibraryLocations()
        {
            logger.LogDebug("Trying to get Oculus base path (REG64)");

            var libraryLocations = GetOculusLibraryLocations(RegistryView.Registry64);

            if (libraryLocations == null)
            {
                logger.LogDebug("Trying to get Oculus base path (REG32)");
                libraryLocations = GetOculusLibraryLocations(RegistryView.Registry32);
            }

            return libraryLocations;
        }
    }
}
