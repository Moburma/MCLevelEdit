using MCLevelEdit.DataModel;
using MCLevelEdit.Interfaces;
using MCLevelEdit.Services;
using ReactiveUI;
using System.Windows.Input;

namespace MCLevelEdit.ViewModels
{
    public class CreateTerrainViewModel : ReactiveObject
    {
        private ITerrainService _terrainService;
        public ICommand GenerateTerrainCommand { get; }
        public bool GenerateTerrainButtonEnable { get; set; }
        public TerrainGenerationParameters TerrainGenerationParameters { get; private set; }

        public CreateTerrainViewModel(ITerrainService terrainService)
        {
            _terrainService = terrainService;
            GenerateTerrainButtonEnable = true;
            TerrainGenerationParameters  = new TerrainGenerationParameters();

            GenerateTerrainCommand = ReactiveCommand.Create(async () =>
            {
                GenerateTerrainButtonEnable = false;
                await _terrainService.CalculateTerrain(TerrainGenerationParameters);
                GenerateTerrainButtonEnable = true;
            });
        }
    }
}
