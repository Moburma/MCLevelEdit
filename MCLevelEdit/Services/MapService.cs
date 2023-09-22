using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using MCLevelEdit.Avalonia;
using MCLevelEdit.DataModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MCLevelEdit.Services
{
    internal class MapService
    {
        public Task<WriteableBitmap> GenerateBitmapAsync(Map map)
        {
            return Task.Run(() =>
            {
                WriteableBitmap bitmap = new WriteableBitmap(
                    new PixelSize(Globals.MAX_MAP_SIZE, Globals.MAX_MAP_SIZE),
                    new Vector(96, 96), // DPI (dots per inch)
                    PixelFormat.Rgba8888);

                var Entities = GetEntities(map);

                foreach(var entity in Entities)
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
                fb.SetPixel(entity.Position.X, entity.Position.Y, Color.FromArgb(128,255,0,0));
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

            return new Map(squares);
        }

        public IEnumerable<Entity> GetEntities(Map map)
        {   
            var entities = new List<Entity>();

            var width = Math.Sqrt(map.Squares.Length);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    if (map.Squares[x, y]?.Entities?.Count() > 0)
                        entities.AddRange(map.Squares[x, y].Entities);
                }
            }
            return entities;
        }

        public void AddEntity(Map map, Entity entity)
        {
            //TODO: Validation needed, this assumes for the moment that more than 1 entity can be on one square
            map.Squares[entity.Position.X, entity.Position.Y]?.Entities?.Add(entity);
        }

        public void RemoveEntity(Map map, Entity entity)
        {
            map.Squares[entity.Position.X, entity.Position.Y]?.Entities?.Remove(entity);
        }
    }
}
