using CommunityToolkit.Mvvm.ComponentModel;

namespace MCLevelEdit.DataModel
{
    public class Square : ObservableObject
    {
        private Position _position;
        private Entity[] _entities;

        public Position Position
        {
            get { return _position; }
            set { SetProperty(ref _position, value); }
        }

        public Entity[] Entities
        {
            get { return _entities; }
            set { SetProperty(ref _entities, value); }
        }

        public Square(Position position, Entity[] entities)
        {
            _position = position;
            _entities = entities;
        }
    }
}
