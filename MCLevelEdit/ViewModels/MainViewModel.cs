namespace MCLevelEdit.ViewModels;

public class MainViewModel : ViewModelBase
{
    public EntitiesViewModel EntitiesViewModel { get; }

    public MainViewModel()
    {
        EntitiesViewModel = new EntitiesViewModel();
    }
}
