using Avalonia.Media.Imaging;
using MCLevelEdit.DataModel;
using System.Threading.Tasks;

namespace MCLevelEdit.Interfaces
{
    public interface IMapService
    {
        void DrawEntity(Entity entity, WriteableBitmap bitmap);
        Map CreateNewMap(ushort size = Globals.MAX_MAP_SIZE);
        Task<WriteableBitmap> GenerateBitmapAsync(Map map);
        Task<WriteableBitmap> DrawBitmapAsync(Map map, WriteableBitmap bitmap);
    }
}
