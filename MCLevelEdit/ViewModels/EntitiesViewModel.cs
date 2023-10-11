using Avalonia.Media;
using DynamicData;
using MCLevelEdit.DataModel;
using MCLevelEdit.Interfaces;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Input;

namespace MCLevelEdit.ViewModels
{
    public class EntitiesViewModel : ViewModelBase
    {
        public ICommand AddNewEntityCommand { get; }
        public ICommand DeleteEntityCommand { get; }

        public static KeyValuePair<int, string>[] TypeIds { get; } =
            Enum.GetValues(typeof(TypeId))
            .Cast<int>()
            .Select(x => new KeyValuePair<int, string>(key: x, value: Enum.GetName(typeof(TypeId), x)))
            .ToArray();

        public EntitiesViewModel(IMapService mapService) : base(mapService)
        {
            foreach(var entity in Map.Entities)
            {
                entity.PropertyChanged += Entity_PropertyChanged;
                entity.Position.PropertyChanged += Entity_PropertyChanged;
                entity.EntityType.PropertyChanged += Entity_PropertyChanged;
                entity.EntityType.Child.PropertyChanged += Entity_PropertyChanged;
            }

            AddNewEntityCommand = ReactiveCommand.Create(() =>
            {
                var entity = AddEntity(new EntityType(TypeId.None, Color.FromRgb(0, 0, 0), 0, ""), new Position(0,0));
                entity.PropertyChanged += Entity_PropertyChanged;
                entity.Position.PropertyChanged += Entity_PropertyChanged;
                entity.EntityType.PropertyChanged += Entity_PropertyChanged;
                entity.EntityType.Child.PropertyChanged += Entity_PropertyChanged;
                Entities.Add(entity);
            });

            DeleteEntityCommand = ReactiveCommand.Create(() =>
            {
                Entities.RemoveAt(0);
            });
        }

        private void Entity_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            RefreshPreviewAsync();
        }
    }
}
