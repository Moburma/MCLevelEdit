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
            Entities = new ObservableCollection<Entity>
            {
                new Entity(0, DataModel.EntityTypes.I.Spawns[(int)Spawn.Flyer1], new Position(0, 0))
            };

            AddNewEntityCommand = ReactiveCommand.Create(() =>
            {
                int i = 0;
                // Code here will be executed when the button is clicked.
            });
        }
    }
}
