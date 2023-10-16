using Avalonia;
using MCLevelEdit.DataModel;
using MCLevelEdit.Interfaces;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MCLevelEdit.ViewModels
{
    public class CreateEntityViewModel : ViewModelBase
    {
        private Entity _entity;

        public Entity Entity
        {
            get => _entity;
            set => this.RaiseAndSetIfChanged(ref _entity, value);
        }

        public static KeyValuePair<int, string>[] TypeIds { get; } =
            Enum.GetValues(typeof(TypeId))
            .Cast<int>()
            .Select(x => new KeyValuePair<int, string>(key: x, value: Enum.GetName(typeof(TypeId), x)))
            .ToArray();

        public CreateEntityViewModel(IMapService mapService) : base(mapService)
        {
            Entity = new Entity()
            {
                Id = Map.Entities.Count(),
                EntityType = new SpawnType(Spawn.Flyer1),
                Position = new Position(0, 0)
            };
        }
    }
}
