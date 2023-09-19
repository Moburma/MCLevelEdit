using CommunityToolkit.Mvvm.ComponentModel;

namespace MCLevelEdit.DataModel
{
    public class Entity : ObservableObject
    {
        private int _id;
        private Position _position;
        private EntityType _entityType;

        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        public Position Position
        {
            get { return _position; }
            set { SetProperty(ref _position, value); }
        }

        public EntityType EntityType
        {
            get { return _entityType; }
            set { SetProperty(ref _entityType, value); }
        }
    };
}
