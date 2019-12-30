using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GPI.Core.Models.DTOs
{
    public class GameInfo
    {
        /// <summary>
        /// Identifier used by hoster application Eg; SteamId
        /// </summary>
        public string HosterContentIdentifier { get; set; }
        public string DisplayTitle { get; set; }

        /// <summary>
        /// Used to identify the platform for this game Eg; PC/ SNES/ OCULUS
        /// </summary>
        public Guid PlatformId { get; set; }
        public string FileLocation { get; set; }
        public string FileName
        {
            get
            {
                return Path.GetFileName(FileLocation);
            }
        }

        public string FileExtension
        {
            get
            {
                return Path.GetExtension(FileLocation);
            }
        }
    }
}
