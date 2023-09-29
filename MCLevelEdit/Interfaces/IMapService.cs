using Avalonia.Media.Imaging;
using MCLevelEdit.DataModel;
using System.Threading.Tasks;

namespace MCLevelEdit.Interfaces
{
    public interface IMapService
    {
        Task<WriteableBitmap> GenerateBitmapAsync(Map map);
        Task<WriteableBitmap> DrawBitmapAsync(Map map, WriteableBitmap bitmap);
        Task<bool> SaveBitmap(WriteableBitmap bitmap);
        void DrawEntity(Entity entity, WriteableBitmap bitmap);
        Map CreateNewMap(ushort size = 256);
    }
}
