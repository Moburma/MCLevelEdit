using MCLevelEdit.Interfaces;

namespace MCLevelEdit.ViewModels
{
    public class MapViewModel : ViewModelBase
    {
        public MapViewModel(IMapService mapService) : base(mapService)
        {

        }
    }
}
