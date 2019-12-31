using System;

namespace GPI.Core
{
    public static class GuidHelper
    {
        public static class Platforms
        {
            public static Guid None = Guid.Empty;
            public static Guid PC = Guid.Parse("E90C3B68-73A4-44B8-9DB2-78045F63E32B");
            public static Guid NES = Guid.Parse("FAE38978-6C54-499D-B1AE-DA5FCD86AC73");
            public static Guid SNES = Guid.Parse("ECA5E331-CE7E-42F7-AA58-0FC676D05C6A");
            public static Guid N64 = Guid.Parse("FEE90143-5AFC-4CDD-93E8-E93795B679E7");
            public static Guid SegaGenesis = Guid.Parse("14DA5DC8-E1C5-440A-8694-5DA21EAA8C32");
            public static Guid PSX = Guid.Parse("EA012B25-2895-42ED-9B80-369A83FE387C");
        }
        public static class Hosters
        {
            public static Guid None = Guid.Empty;
            public static Guid Steam = Guid.Parse("E6836EFD-3A8A-4F1E-8D9A-EF0B582268B3");
            public static Guid Uplay = Guid.Parse("CBBB5D72-646E-4968-8504-055CF2257E82");
            public static Guid Origin = Guid.Parse("7D523BBB-00F0-4FFA-859B-A865A4CBB315");
            public static Guid BattleNet = Guid.Parse("0373094A-5EAD-497D-B773-F45BF82FA7D2");
            public static Guid GOG = Guid.Parse("42A64980-E52F-48FB-9243-B48398B3EBD2");
            public static Guid Oculus = Guid.Parse("2C5AF03D-7E1A-49B0-933C-79C69BFFC35D");
            public static Guid VivePort = Guid.Parse("EE0D7222-9750-455E-8F49-960204FDFF52");
            public static Guid RockStarLauncher = Guid.Parse("03C42D17-16B1-4E41-8FF2-F96A1CA238EF");
        }
    }
}
