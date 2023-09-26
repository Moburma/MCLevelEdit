using Splat;

namespace MCLevelEdit.ViewModels;

public class MainViewModel : ViewModelBase
{
    public EntitiesViewModel EntitiesViewModel { get; }

    public MainViewModel()
    {
        EntitiesViewModel = Locator.Current.GetService<EntitiesViewModel>();
    }
}
