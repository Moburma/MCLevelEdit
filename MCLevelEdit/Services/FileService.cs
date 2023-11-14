using MCLevelEdit.DataModel;
using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace MCLevelEdit.Services
{
    public class FileService
    {
        public Task<Map> LoadMapFromFile(string fileName)
        {
            var levfile = File.ReadAllBytes(fileName);
            var counter = 0;
            var Wnumber = 1;

            if (levfile[0] == 82 && levfile[1] == 78 && levfile[2] == 67)
            {
                throw new Exception("Compressed level detected! You must uncompress this file first");
            }

            var manaTarget = levfile[38800];
            var numWizards = levfile[38802];
            var manaTotal = BitConverter.ToInt32(levfile, 0);

            TerrainGenerationParameters terrainGenerationParameters = new TerrainGenerationParameters()
            {
                Seed = BitConverter.ToUInt16(levfile, 4),
                Offset = BitConverter.ToUInt16(levfile, 8),
                Raise = (ushort)BitConverter.ToUInt32(levfile, 12),
                Gnarl = BitConverter.ToUInt16(levfile, 16),
                River = BitConverter.ToUInt16(levfile, 20),
                Source = BitConverter.ToUInt16(levfile, 24),
                SnLin = BitConverter.ToUInt16(levfile, 28),
                SnFlt = (byte)BitConverter.ToUInt16(levfile, 32),
                BhLin = (byte)BitConverter.ToUInt16(levfile, 36),
                BhFlt = BitConverter.ToUInt16(levfile, 40),
                RkSte = BitConverter.ToUInt16(levfile, 44)
            };

            Map map = new Map()
            {
                TerrainGenerationParameters = terrainGenerationParameters
            };

            return Task.FromResult(map);
        }
    }
}
