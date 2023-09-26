using MCLevelEdit.DataModel;
using MCLevelEdit.Interfaces;
using Splat;

namespace MCLevelEdit.ViewModels;

public class MainViewModel : ViewModelBase
{
    public EntitiesViewModel EntitiesViewModel { get; }
    public MainViewModel(IMapService mapService) : base(mapService)
    {
        EntitiesViewModel = Locator.Current.GetService<EntitiesViewModel>();
    }
}
