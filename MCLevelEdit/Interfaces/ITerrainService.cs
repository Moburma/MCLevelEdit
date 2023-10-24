using Avalonia.Media.Imaging;
using MCLevelEdit.DataModel;
using System.Threading.Tasks;

namespace MCLevelEdit.Interfaces;

public interface ITerrainService
{
    public Task<byte[]> CalculateTerrain(TerrainGenerationParameters genParams);
    Task<WriteableBitmap> GenerateBitmapAsync(byte[] heightMap);
    Task<WriteableBitmap> DrawBitmapAsync(byte[] heightMap, WriteableBitmap bitmap);
}