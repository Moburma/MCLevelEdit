using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using MCLevelEdit.Avalonia;
using MCLevelEdit.DataModel;
using MCLevelEdit.Interfaces;
using MCLevelEdit.Utils;
using System.Threading.Tasks;

namespace MCLevelEdit.Services
{
    public class MapService : IMapService
    {
        public Task<WriteableBitmap> GenerateBitmapAsync(Map map)
        {
            return Task.Run(() =>
            {
                WriteableBitmap bitmap = new WriteableBitmap(
                    new PixelSize(Globals.MAX_MAP_SIZE, Globals.MAX_MAP_SIZE),
                    new Vector(96, 96), // DPI (dots per inch)
                    PixelFormat.Rgba8888);

                return DrawBitmapAsync(map, bitmap);
            });
        }

        public Task<WriteableBitmap> DrawBitmapAsync(Map map, WriteableBitmap bitmap)
        {
            return Task.Run(() =>
            {
                var Entities = map.Entities;

                BitmapUtils.SetBackground(new Rect(0, 0, Globals.MAX_MAP_SIZE, Globals.MAX_MAP_SIZE), new Color(0, 0, 0, 0), bitmap);

                foreach (var entity in Entities)
                {
                    DrawEntity(entity, bitmap);
                }

                //var result = SaveBitmap(bitmap).Result;

                return bitmap;
            });
        }

        public void DrawEntity(Entity entity, WriteableBitmap bitmap)
        {
            using (var fb = bitmap.Lock())
            {
                fb.SetPixel(entity.Position.X, entity.Position.Y, entity.EntityType.Colour);
            }
        }

        public Map CreateNewMap(ushort size = Globals.MAX_MAP_SIZE)
        {
            var map = new Map();
            return map;
        }
    }
}
