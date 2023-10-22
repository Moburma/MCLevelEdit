using MCLevelEdit.DataModel;
using System.Threading.Tasks;

namespace MCLevelEdit.Interfaces;

public interface ITerrainService
{
    public Task<bool> CalculateTerrain(TerrainGenerationParameters genParams);
    public void sub_B5E70_decompress_terrain_map_level(short[] mapEntityIndex_15B4E0, ushort seed, ushort offset, ushort raise, ushort gnarl);
}