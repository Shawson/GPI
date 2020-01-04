using GPI.Core;
using GPI.Core.Models.DTOs;
using GPI.Services.FileSystem;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using SteamKit2;
using GPI.Services.OS;

namespace GPI.Services.ContentHosts.Steam
{
    public class SteamHost : IBasicContentHost
    {
        private readonly IDirectoryShim _directoryShim;
        private readonly IFileShim _fileShim;
        private readonly IRegistryValueProvider _registry;
        private readonly ILogger<SteamHost> _logger;
        private string _installationPath;

        public Guid HosterIdentifier { get; } = GuidHelper.Hosters.Steam;
        public Guid DefaultPlatformIdentifier { get; } = GuidHelper.Platforms.PC;
        public SteamConfig Settings { get; set; } = null;

        public string Title => "Steam";

        public SteamHost(
            IDirectoryShim directoryShim,
            IFileShim fileShim,
            IRegistryValueProvider registry,
            ILogger<SteamHost> logger)
        {
            this._directoryShim = directoryShim;
            this._fileShim = fileShim;
            this._registry = registry;
            this._logger = logger;
        }

        public async Task<List<GameInfo>> ScanForGames(CancellationToken token)
        {
            var games = new List<GameInfo>();

            _installationPath = _registry
                    .GetValueForPath(Microsoft.Win32.RegistryView.Default, Microsoft.Win32.RegistryHive.CurrentUser, @"Software\Valve\Steam", "SteamPath")?
                    .Replace("/", @"\");

            if (string.IsNullOrEmpty(_installationPath))
            {
                return games;
            }

            foreach (var folder in GetLibraryFolders())
            {
                var libraryPath = Path.Combine(folder, "steamapps");

                if (!_directoryShim.Exists(libraryPath))
                {
                    _logger.LogWarning($"Library Folder Missing {libraryPath} ");
                }

                foreach(var manifest in GetGamesForPath(libraryPath))
                {
                    if (manifest.HosterContentIdentifier == "228980") // ignore steam common redistibutables (https://steamdb.info/app/228980/)
                    {
                        continue;
                    }

                    if (!games.Any(x => x.HosterContentIdentifier == manifest.HosterContentIdentifier))
                    {
                        games.Add(manifest);
                    }
                }
                
            }

            return games;
        }

        public Task LaunchGame(Guid gameId)
        {
            // @"steam://rungameid/" + gameId,
            throw new NotImplementedException();
        }

        public void LoadSettingsFromJson(string jsonSettings)
        {
            Settings = JsonConvert.DeserializeObject<SteamConfig>(jsonSettings);
        }


        private GameInfo GetGameFromManifest(string path)
        {
            var kv = new KeyValue();
            kv.ReadFileAsText(path);

            var name = string.IsNullOrEmpty(kv["name"].Value)
                ? kv["UserConfig"]["name"].Value ?? string.Empty
                : kv["name"].Value;

            var gameId = new GameID(kv["appID"].AsUnsignedInteger());
            var game = new GameInfo()
            {
                
                HosterContentIdentifier = gameId.ToString(),
                DisplayTitle = name,
                FileLocation = Path.Combine((new FileInfo(path)).Directory.FullName, "common", kv["installDir"].Value),
                PlatformId = DefaultPlatformIdentifier
            };

            return game;
        }

        private List<GameInfo> GetGamesForPath(string path)
        {
            var games = new List<GameInfo>();

            var files = _directoryShim.GetFiles(path, false);

            var manifestFileStart = Path.Combine(path, "appmanifest");

            foreach (var file in files.Where(x => x.StartsWith(manifestFileStart)))
            {
                try
                {
                    var game = GetGameFromManifest(Path.Combine(path, file));
                    games.Add(game);
                }
                catch (Exception e)
                {
                    _logger.LogError($"problem processing manifest {file} : {e}");
                }
            }

            return games;
        }

        private List<string> GetLibraryFolders()
        {
            var dbs = new List<string>() { _installationPath };
            var configPath = Path.Combine( _installationPath, "steamapps", "libraryfolders.vdf");
            if (!_fileShim.Exists(configPath))
            {
                return dbs;
            }

            var kv = new KeyValue();
            kv.ReadFileAsText(configPath);

            foreach (var child in kv.Children)
            {
                if (int.TryParse(child.Name, out int test))
                {
                    if (!string.IsNullOrEmpty(child.Value) && Directory.Exists(child.Value))
                    {
                        dbs.Add(child.Value);
                    }
                }
            }

            return dbs;
        }
    }

    public class SteamConfig
    {

    }
}