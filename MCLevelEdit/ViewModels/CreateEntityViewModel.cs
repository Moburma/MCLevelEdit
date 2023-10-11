using MCLevelEdit.DataModel;
using MCLevelEdit.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MCLevelEdit.ViewModels
{
    public class CreateEntityViewModel : ViewModelBase
    { 
        public Entity Entity { get; set; }

        public static KeyValuePair<int, string>[] TypeIds { get; } =
            Enum.GetValues(typeof(TypeId))
            .Cast<int>()
            .Select(x => new KeyValuePair<int, string>(key: x, value: Enum.GetName(typeof(TypeId), x)))
            .ToArray();

        public CreateEntityViewModel(IMapService mapService) : base(mapService)
        {

        }
    }
}
