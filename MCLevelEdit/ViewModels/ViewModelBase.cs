using MCLevelEdit.DataModel;
using MCLevelEdit.Interfaces;
using ReactiveUI;
using System.Linq;

namespace MCLevelEdit.ViewModels;

public class ViewModelBase : ReactiveObject
{
    protected readonly IMapService _mapService;
    protected Map _map;

    public ObservableImage Preview { get; }

    public ViewModelBase(IMapService mapService)
    {
        _mapService = mapService;

        if (Map.Instance is null)
        {
            Map.Instance = _mapService.CreateNewMap();
        }
        _map = Map.Instance;
        Preview = new ObservableImage()
        {
            Image = _mapService.GenerateBitmapAsync(_map).Result
        };
    }

    protected async void RefreshPreviewAsync()
    {
        await _mapService.DrawBitmapAsync(_map, Preview.Image);
        //{
        //    PreviewImage = image.Result;
        //});
    }

    protected Entity AddEntity(EntityType entityType, Position position)
    {
        var newEntity = new Entity()
        {
            Id = _map.Entities.Count(),
            EntityType = entityType,
            Position = position
        };
        _map.AddEntity(newEntity);
        RefreshPreviewAsync();
        return newEntity;
    }
}
