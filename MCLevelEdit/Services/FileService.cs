using MCLevelEdit.DataModel;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MCLevelEdit.Services
{
    public class FileService
    {
        public Task<Map> LoadMapFromFile(string fileName)
        {
            var levfile = File.ReadAllBytes(fileName);
            var thingcount = 1999;
            var counter = 0;
            var Wnumber = 1;

            if (levfile[0] == 82 && levfile[1] == 78 && levfile[2] == 67)
            {
                throw new Exception("Compressed level detected! You must uncompress this file first");
            }

            var manaTarget = levfile[38800];
            var numWizards = levfile[38802];
            var manaTotal = levfile[0] + levfile[1] + levfile[2] + levfile[3];

            TerrainGenerationParameters terrainGenerationParameters = new TerrainGenerationParameters()
            {
                Seed = (ushort)(levfile[4] + levfile[5]),
                Offset = (ushort)(levfile[8] + levfile[9]),
                Raise = (ushort)(levfile[12] + levfile[13] + levfile[14] + levfile[15]),
                Gnarl = (ushort)(levfile[16] + levfile[17]),
                River = (ushort)(levfile[20] + levfile[21]),
                Source = (ushort)(levfile[24] + levfile[25]),
                SnLin = (ushort)(levfile[28] + levfile[29]),
                SnFlt = (byte)(levfile[32] + levfile[33]),
                BhLin = (byte)(levfile[36] + levfile[37]),
                BhFlt = (ushort)(levfile[40] + levfile[41]),
                RkSte = (ushort)(levfile[44] + levfile[45])
            };

            Map map = new Map()
            {
                TerrainGenerationParameters = terrainGenerationParameters
            };

            return Task.FromResult(map);
        }
    }
}
