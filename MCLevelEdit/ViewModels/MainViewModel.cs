using MCLevelEdit.Interfaces;
using ReactiveUI;
using Splat;
using System.Windows.Input;

namespace MCLevelEdit.ViewModels;

public class MainViewModel : ViewModelBase
{
    public ICommand EditEntitiesCommand { get; }
    public EntitiesViewModel EntitiesViewModel { get; }

    public MainViewModel(IMapService mapService) : base(mapService)
    {
        EntitiesViewModel = Locator.Current.GetService<EntitiesViewModel>();

        EditEntitiesCommand = ReactiveCommand.CreateFromTask(async () =>
        {

        });
    }
}
