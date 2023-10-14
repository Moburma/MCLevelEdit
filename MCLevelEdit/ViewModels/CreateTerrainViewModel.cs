using MCLevelEdit.DataModel;
using MCLevelEdit.Interfaces;
using ReactiveUI;

namespace MCLevelEdit.ViewModels
{
    public class CreateTerrainViewModel : ReactiveObject
    {
        public TerrainGenerationParameters TerrainGenerationParameters { get; set; }

        public CreateTerrainViewModel(IMapService mapService)
        {

        }
    }
}
