using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;

namespace MCLevelEdit.DataModel
{
    public class Map : ObservableObject
    {
        private Square[,] _squares;

        public static Map Instance { get; set; }

        public Square[,] Squares
        {
            get { return _squares; }
            set
            {
                SetProperty(ref _squares, value);
            }
        }

        public IEnumerable<Entity> Entities 
        { 
            get 
            {
                var entities = new List<Entity>();

                var width = Math.Sqrt(this.Squares.Length);
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < width; y++)
                    {
                        if (this.Squares[x, y]?.Entities?.Count > 0)
                            entities.AddRange(this.Squares[x, y].Entities);
                    }
                }
                return entities;
            } 
        }

        public Map(Square[, ] squares)
        {
            _squares = squares;
        }

        public void AddEntity(Entity entity)
        {
            //TODO: Validation needed, this assumes for the moment that more than 1 entity can be on one square
            this.Squares[entity.Position.X, entity.Position.Y]?.Entities?.Add(entity);
        }

        public void RemoveEntity(Entity entity)
        {
            this.Squares[entity.Position.X, entity.Position.Y]?.Entities?.Remove(entity);
        }
    }
}
