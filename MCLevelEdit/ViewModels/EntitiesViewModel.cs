using MCLevelEdit.DataModel;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Input;

namespace MCLevelEdit.ViewModels
{
    public class EntitiesViewModel : ViewModelBase
    {
        public ObservableCollection<Entity> Entities { get; }
        public ObservableCollection<KeyValuePair<int, string>> TypeIds { get; }

        public ICommand AddNewEntityCommand { get; }

        public EntitiesViewModel()
        {
            Entities = new ObservableCollection<Entity>
            {
                new Entity(0, DataModel.EntityTypes.I.Spawns[(int)Spawn.Flyer1], new Position(0, 0))
            };
            
            TypeIds = new ObservableCollection<KeyValuePair<int, string>>(Enum.GetValues(typeof(TypeId))
                .Cast<int>()
                .Select(x => new KeyValuePair<int, string>(key: x, value: Enum.GetName(typeof(TypeId), x))));

            AddNewEntityCommand = ReactiveCommand.Create(() =>
            {
                int i = 0;
                // Code here will be executed when the button is clicked.
            });
        }
    }
}
