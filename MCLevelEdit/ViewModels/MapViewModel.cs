using MCLevelEdit.Interfaces;
using Splat;

namespace MCLevelEdit.ViewModels
{
    public class MapViewModel : ViewModelBase
    {
        public CreateEntityViewModel CreateEntityViewModel { get; }
        public CreateTerrainViewModel CreateTerrainViewModel { get; }

        public MapViewModel(IMapService mapService) : base(mapService)
        {
            CreateEntityViewModel = Locator.Current.GetService<CreateEntityViewModel>();
            CreateTerrainViewModel = Locator.Current.GetService<CreateTerrainViewModel>();
        }
    }
}
