using MCLevelEdit.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MCLevelEdit.Services
{
    internal class MapService
    {
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
