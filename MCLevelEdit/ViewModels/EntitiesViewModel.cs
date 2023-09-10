using MCLevelEdit.DataModel;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MCLevelEdit.ViewModels
{
    public class EntitiesViewModel : ViewModelBase
    {
        public ObservableCollection<Entity> Entities { get; }
        public ICommand AddNewEntityCommand { get; }

        public EntitiesViewModel()
        {
            Entities = new ObservableCollection<Entity>();
            Entities.Add(new Entity(0, DataModel.EntityTypes.I.Spawns[0], new Position(0, 0)));

            AddNewEntityCommand = ReactiveCommand.Create(() =>
            {
                // Code here will be executed when the button is clicked.
            });
        }
    }
}
