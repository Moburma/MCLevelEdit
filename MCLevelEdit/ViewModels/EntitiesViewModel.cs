using MCLevelEdit.DataModel;
using System.Collections.ObjectModel;

namespace MCLevelEdit.ViewModels
{
    public class EntitiesViewModel : ViewModelBase
    {
        public EntitiesViewModel()
        {
            Entities = new ObservableCollection<Entity>();
            Entities.Add(new Entity(0, DataModel.EntityTypes.I.Spawns[0], new Position(0, 0)));
        }

        public ObservableCollection<Entity> Entities { get; }
    }
}
