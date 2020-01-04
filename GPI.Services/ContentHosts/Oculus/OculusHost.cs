using GPI.Core;
using GPI.Core.Models.DTOs;
using GPI.Services.ContentHosts.Oculus.DataExtraction;
using GPI.Services.OS;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GPI.Services.ContentHosts.Oculus
{
    public class OculusHost : IBasicContentHost
    {
        public Guid HosterIdentifier => GuidHelper.Hosters.Oculus;

        public Guid DefaultPlatformIdentifier => GuidHelper.Platforms.PC;

        public string Title => "Oculus";

        private readonly IOculusWebsiteScraper _oculusScraper;
        private readonly IOculusPathSniffer _oculusPathSniffer;
        private readonly ILogger<OculusHost> _logger;

        public OculusHost(ILogger<OculusHost> logger, IOculusWebsiteScraper oculusWebsiteScraper, IOculusPathSniffer oculusPathSniffer) 
        {
            _logger = logger;
            _oculusPathSniffer = oculusPathSniffer;
            _oculusScraper = oculusWebsiteScraper;
        }

        public OculusHost(ILogger<OculusHost> logger)
        {
            _logger = logger;
            _oculusPathSniffer = new OculusPathSniffer(new RegistryValueProvider(), new PathNormaliser(new WMODriveQueryProvider()), logger);
            _oculusScraper = new OculusWebsiteScraper(logger);
        }

        private List<OculusManifest> GetOculusAppManifests(string oculusBasePath)
        {
            _logger.LogDebug($"Listing Oculus manifests");

            string[] fileEntries = Directory.GetFiles($@"{oculusBasePath}\Manifests\");

            if (!fileEntries.Any())
            {
                _logger.LogInformation($"No Oculus game manifests found");
            }

            var manifests = new List<OculusManifest>();

            foreach (string fileName in fileEntries.Where(x => x.EndsWith(".json")))
            {
                try
                {
                    if (fileName.EndsWith("_assets.json"))
                    {
                        // not interested in the asset json files
                        continue;
                    }

                    var json = File.ReadAllText(fileName);

                    if (string.IsNullOrWhiteSpace(json))
                    {
                        _logger.LogError($"JSON file is empty ({fileName})");
                        continue;
                    }

                    var manifest = JsonConvert.DeserializeObject<OculusManifest>(json);

                    if (manifest == null)
                    {
                        _logger.LogError($"Could not deserialise json ({fileName})");
                    }

                    manifest.LaunchFile = manifest?.LaunchFile?.Replace("/", @"\");

                    manifests.Add(manifest);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Exception while processing manifest ({fileName}) : {ex}");
                }
            }

            return manifests;
        }

        public async Task<List<GameInfo>> ScanForGames(CancellationToken token)
        {
            _logger.LogInformation($"Executing Oculus GetGames");

            var gameInfos = new List<GameInfo>();

            var oculusLibraryLocations = _oculusPathSniffer.GetOculusLibraryLocations();

            if (oculusLibraryLocations == null || !oculusLibraryLocations.Any())
            {
                _logger.LogError($"Cannot ascertain Oculus library locations");
                return await Task.FromResult(gameInfos);
            }

            //using (var view = PlayniteApi.WebViews.CreateOffscreenView())
            {
                foreach (var oculusBasePath in oculusLibraryLocations)
                {
                    _logger.LogInformation($"Processing Oculus library location {oculusBasePath}");

                    foreach (var manifest in GetOculusAppManifests(oculusBasePath))
                    {
                        _logger.LogInformation($"Processing manifest {manifest.CanonicalName} {manifest.AppId}");

                        try
                        {
                            var executableFullPath = $@"{oculusBasePath}\Software\{manifest.CanonicalName}\{manifest.LaunchFile}";

                            // set a default name
                            var executableName = Path.GetFileNameWithoutExtension(executableFullPath);

                            var icon = $@"{oculusBasePath}\..\CoreData\Software\StoreAssets\{manifest.CanonicalName}_assets\icon_image.jpg";

                            if (!File.Exists(icon))
                            {
                                _logger.LogDebug($"Oculus store icon missing from file system- reverting to executable icon");
                                icon = executableFullPath;
                            }

                            var backgroundImage = $@"{oculusBasePath}\..\CoreData\Software\StoreAssets\{manifest.CanonicalName}_assets\cover_landscape_image_large.png";

                            if (!File.Exists(backgroundImage))
                            {
                                _logger.LogDebug($"Oculus store background missing from file system- selecting no background");
                                backgroundImage = string.Empty;
                            }

                            OculusWebsiteJson scrapedData = null;// _oculusScraper.ScrapeDataForApplicationId(view, manifest.AppId);

                            if (scrapedData == null)
                            {
                                _logger.LogDebug($"Failed to retrieve scraped data for game");
                            }

                            _logger.LogInformation($"Executable {executableFullPath}");

                            gameInfos.Add(new GameInfo
                            {
                                FileLocation = executableFullPath,
                                PlatformId = DefaultPlatformIdentifier,
                                DisplayTitle = scrapedData?.Name ?? Path.GetFileNameWithoutExtension(executableName),
                                HosterContentIdentifier = manifest.AppId
                            });

                            _logger.LogInformation($"Completed manifest {manifest.CanonicalName} {manifest.AppId}");
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"Exception while adding game for manifest {manifest.AppId} : {ex}");
                        }
                    }
                }
            }

            _logger.LogInformation($"Oculus GetGames Completing");

            return gameInfos;
        }

        public Task LaunchGame(Guid gameId)
        {
            throw new NotImplementedException();
        }

        public void LoadSettingsFromJson(string jsonSettings)
        {
            return;
        }
    }
}