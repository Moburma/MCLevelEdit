using Avalonia.Collections;
using MCLevelEdit.DataModel;
using MCLevelEdit.Interfaces;
using ReactiveUI;
using System.Linq;
using System.Threading.Tasks;

namespace MCLevelEdit.ViewModels;

public class ViewModelBase : ReactiveObject
{
    protected readonly IMapService _mapService;

    public AvaloniaList<Entity> Entities { get; }
    public Map Map { get; set; }

    public ViewModelBase(IMapService mapService)
    {
        _mapService = mapService;

        if (Map.Instance is null)
        {
            Map.Instance = _mapService.CreateNewMap();
        }
        Map = Map.Instance;

        Entities = new AvaloniaList<Entity>();
    }

    protected async Task RefreshPreviewAsync()
    {
        Map.Preview = await _mapService.GenerateBitmapAsync(Map);
    }

    protected Entity AddEntity(EntityType entityType, Position position)
    {
        var newEntity = new Entity()
        {
            Id = Map.Entities.Count(),
            EntityType = entityType,
            Position = position
        };
        Map.AddEntity(newEntity);
        RefreshPreviewAsync();
        return newEntity;
    }
}
