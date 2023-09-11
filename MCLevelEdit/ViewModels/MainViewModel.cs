namespace MCLevelEdit.ViewModels;

public class MainViewModel : ViewModelBase
{
    public string Greeting => "Welcome to Avalonia!";
    public EntitiesViewModel EntitiesViewModel { get; }

    public MainViewModel()
    {
        EntitiesViewModel = new EntitiesViewModel();
    }
}
