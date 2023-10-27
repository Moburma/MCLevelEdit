using MCLevelEdit.Interfaces;
using ReactiveUI;
using Splat;
using System.Reactive.Linq;
using System.Windows.Input;

namespace MCLevelEdit.ViewModels;

public class MainViewModel : ViewModelBase
{
    public ICommand EditEntitiesCommand { get; }
    public EntitiesViewModel EntitiesViewModel { get; }
    public MapViewModel MapViewModel { get; }

    public Interaction<EntitiesViewModel, EntitiesViewModel?> ShowDialog { get; }

    public MainViewModel(IMapService mapService, ITerrainService terrainService) : base(mapService, terrainService)
    {
        EntitiesViewModel = Locator.Current.GetService<EntitiesViewModel>();
        MapViewModel = Locator.Current.GetService<MapViewModel>();

        ShowDialog = new Interaction<EntitiesViewModel, EntitiesViewModel?>();

        EditEntitiesCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var result = await ShowDialog.Handle(EntitiesViewModel);
        });
    }
}
