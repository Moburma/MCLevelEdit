using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;

namespace MCLevelEdit.DataModel
{
    public class Square : ObservableObject
    {
        private Position _position;
        private List<Entity> _entities;

        public Position Position
        {
            get { return _position; }
            set { SetProperty(ref _position, value); }
        }

        public List<Entity> Entities
        {
            get { return _entities; }
            set { SetProperty(ref _entities, value); }
        }

        public Square(Position position, Entity[] entities)
        {
            _position = position;
            _entities = new List<Entity>(entities);
        }
    }
}
