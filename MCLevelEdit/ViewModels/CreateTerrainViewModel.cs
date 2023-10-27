using MCLevelEdit.DataModel;
using MCLevelEdit.Interfaces;
using ReactiveUI;
using System.Windows.Input;

namespace MCLevelEdit.ViewModels
{
    public class CreateTerrainViewModel : ViewModelBase
    {
        private readonly ITerrainService _terrainService;
        public ICommand GenerateTerrainCommand { get; }
        public bool GenerateTerrainButtonEnable { get; set; }
        public TerrainGenerationParameters TerrainGenerationParameters { get; private set; }

        public CreateTerrainViewModel(IMapService mapService, ITerrainService terrainService) : base(mapService, terrainService)
        {
            _terrainService = terrainService;
            GenerateTerrainButtonEnable = true;
            TerrainGenerationParameters = new TerrainGenerationParameters();

            GenerateTerrainCommand = ReactiveCommand.Create(async () =>
            {
                GenerateTerrainButtonEnable = false;
                Map.Instance.HeightMap = await _terrainService.CalculateTerrain(TerrainGenerationParameters);
                GenerateTerrainButtonEnable = true;
                RefreshPreviewAsync();
            });
        }
    }
}
