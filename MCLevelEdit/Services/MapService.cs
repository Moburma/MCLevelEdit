using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using MCLevelEdit.Avalonia;
using MCLevelEdit.DataModel;
using MCLevelEdit.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MCLevelEdit.Services
{
    internal class MapService : IMapService
    {
        public static int index = 0;
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

                //Random rnd = new Random();
                //SetBackground(new Rect(0, 0, Globals.MAX_MAP_SIZE, Globals.MAX_MAP_SIZE), new Color(255, (byte)rnd.Next(0, 128), (byte)rnd.Next(0, 128), (byte)rnd.Next(0, 128)), bitmap);

                SetBackground(new Rect(0, 0, Globals.MAX_MAP_SIZE, Globals.MAX_MAP_SIZE), new Color(255, 0, 0, 0), bitmap);

                foreach (var entity in Entities)
                {
                    DrawEntity(entity, bitmap);
                }

                //var result = SaveBitmap(bitmap).Result;

                return bitmap;
            });
        }

        public Task<bool> SaveBitmap(WriteableBitmap bitmap)
        {
            return Task.Run(() =>
            {
                try
                {
                    string pathTempDir = Path.Combine(Path.GetTempPath(), Globals.APP_DIRECTORY);
                    Directory.CreateDirectory(pathTempDir);

                    using (var file = new FileStream(Path.Combine(pathTempDir, $"Temp{index}.png"), FileMode.Create))
                    {
                        bitmap.Save(file);
                    }
                    index++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception saving png: {ex.Message}");
                    return false;
                }

                return true;
            });
        }

        public void DrawEntity(Entity entity, WriteableBitmap bitmap)
        {
            using (var fb = bitmap.Lock())
            {
                fb.SetPixel(entity.Position.X, entity.Position.Y, entity.EntityType.Colour);
            }
        }

        private void SetBackground(Rect rect, Color colour, WriteableBitmap bitmap)
        {
            using (var fb = bitmap.Lock())
            {
                for (int x = (int)rect.X; x < rect.Width; x++)
                {
                    for (int y = (int)rect.Y; y < rect.Height; y++)
                    {
                        fb.SetPixel(x, y, colour);
                    }
                }
            }
        }

        public Map CreateNewMap(ushort size = 256)
        {
            var map = new Map();
            return map;
        }
    }
}
