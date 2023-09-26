using Avalonia.Media.Imaging;
using MCLevelEdit.DataModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MCLevelEdit.Interfaces
{
    public interface IMapService
    {
        Task<WriteableBitmap> GenerateBitmapAsync(Map map);
        Task<bool> SaveBitmap(WriteableBitmap bitmap);
        void DrawEntity(Entity entity, WriteableBitmap bitmap);
        Map CreateNewMap(ushort size = 256);
        IEnumerable<Entity> GetEntities(Map map);
        void AddEntity(Map map, Entity entity);
        void RemoveEntity(Map map, Entity entity);
    }
}
