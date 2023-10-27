using Avalonia.ReactiveUI;
using MCLevelEdit.ViewModels;
using ReactiveUI;
using System.Threading.Tasks;

namespace MCLevelEdit.Views;

public partial class MainWindow : ReactiveWindow<MainViewModel>
{
    public MainWindow()
    {
        InitializeComponent();

        this.WhenActivated(action => action(ViewModel!.ShowDialog.RegisterHandler(DoShowDialogAsync)));
    }

    private async Task DoShowDialogAsync(InteractionContext<MapViewModel,
                                    MapViewModel?> interaction)
    {
        var dialog = new EntitiesWindow();
        dialog.DataContext = interaction.Input;

        var result = await dialog.ShowDialog<MapViewModel?>(this);
        interaction.SetOutput(result);
    }
}
