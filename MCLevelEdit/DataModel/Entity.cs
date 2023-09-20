using Avalonia.Collections;
using CommunityToolkit.Mvvm.ComponentModel;
using MCLevelEdit.ViewModels;

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
            set {
                if (EntitiesViewModel.Types is null)
                {
                    EntitiesViewModel.Types = new AvaloniaList<EntityChildType>(EntityTypeExtensions.GetChildTypesFromTypeId(value.TypeId));
                }
                else
                {
                    EntitiesViewModel.Types.Clear();
                    EntitiesViewModel.Types.AddRange(EntityTypeExtensions.GetChildTypesFromTypeId(value.TypeId));
                }
                value.Child = EntitiesViewModel.Types[0];
                SetProperty(ref _entityType, value); 
            }
        }
    };
}
