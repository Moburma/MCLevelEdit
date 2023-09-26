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
        public Task<WriteableBitmap> GenerateBitmapAsync(Map map)
        {
            return Task.Run(() =>
            {
                WriteableBitmap bitmap = new WriteableBitmap(
                    new PixelSize(Globals.MAX_MAP_SIZE, Globals.MAX_MAP_SIZE),
                    new Vector(96, 96), // DPI (dots per inch)
                    PixelFormat.Rgba8888);

                var Entities = map.Entities;

                SetBackground(new Rect(0, 0, Globals.MAX_MAP_SIZE, Globals.MAX_MAP_SIZE), new Color(255,0,0,0), bitmap);

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

                    using (var file = new FileStream(Path.Combine(pathTempDir, "Temp.png"), FileMode.Create))
                    {
                        bitmap.Save(file);
                    }
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
                //TODO: All entities are red atm
                fb.SetPixel(entity.Position.X, entity.Position.Y, Color.FromArgb(255,255,0,0));
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
            var squares = new Square[size, size];

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    squares[x, y] = new Square(new Position(x, y), new Entity[] { });
                }
            }

            var map = new Map(squares);
            var newEntity = new Entity()
            {
                Id = 0,
                EntityType = EntityTypes.I.Spawns[(int)Spawn.Flyer1],
                Position = new Position(128, 128)
            };
            map.AddEntity(newEntity);
            return map;
        }
    }
}
