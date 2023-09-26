using Avalonia.Collections;
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
        private readonly IMapService _mapService;
        private Map _map;

        public AvaloniaList<Entity> Entities { get; }
        public ICommand AddNewEntityCommand { get; }
        public ICommand DeleteEntityCommand { get; }

        public static KeyValuePair<int, string>[] TypeIds { get; } =
            Enum.GetValues(typeof(TypeId))
            .Cast<int>()
            .Select(x => new KeyValuePair<int, string>(key: x, value: Enum.GetName(typeof(TypeId), x)))
            .ToArray();

        public EntitiesViewModel(IMapService mapService)
        {
            _mapService = mapService;

            if (Map.Instance is null)
            {
                _map = _mapService.CreateNewMap();
            }

            Entities = new AvaloniaList<Entity>();

            AddEntity(EntityTypes.I.Spawns[(int)Spawn.Flyer1]);

            AddNewEntityCommand = ReactiveCommand.Create(() =>
            {
                AddEntity(new EntityType(TypeId.None, 0, ""));
            });

            DeleteEntityCommand = ReactiveCommand.Create(() =>
            {
                Entities.RemoveAt(0);
            });
        }

        private void AddEntity(EntityType entityType)
        {
            var newEntity = new Entity()
            {
                Id = Entities.Count + 1,
                EntityType = entityType,
                Position = new Position(Entities.Count + 1, 0)
            };

            Entities.Add(newEntity);

            _mapService.AddEntity(_map, newEntity);
            var image = _mapService.GenerateBitmapAsync(_map).Result;
        }
    }
}
