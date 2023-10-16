using CommunityToolkit.Mvvm.ComponentModel;

namespace MCLevelEdit.DataModel
{
    public class Entity : ObservableObject
    {
        private int _id;
        private Position _position;
        private EntityType _entityType;
        private int _parent;
        private int _child;

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

        public int Parent
        {
            get { return _parent; }
            set { SetProperty(ref _parent, value); }
        }

        public int Child
        {
            get { return _child; }
            set { SetProperty(ref _child, value); }
        }
    };
}
