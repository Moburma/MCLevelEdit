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

    public AvaloniaList<Entity> Entities => new AvaloniaList<Entity>(Map.Instance.Entities);
    public Map Map { get; set; }

    public ViewModelBase(IMapService mapService)
    {
        _mapService = mapService;

        if (Map.Instance is null)
        {
            Map.Instance = _mapService.CreateNewMap();
        }
        Map = Map.Instance;
    }

    protected async Task RefreshPreviewAsync()
    {
        Map.Preview = await _mapService.GenerateBitmapAsync(Map);
    }

    protected Entity AddEntity(EntityType entityType, Position position, ushort parent = 0, ushort child = 0)
    {
        var newEntity = new Entity()
        {
            Id = Map.Entities.Count(),
            EntityType = entityType,
            Position = position,
            Parent = parent,
            Child = child
        };
        return AddEntity(newEntity);
    }

    protected Entity AddEntity(Entity entity)
    {
        var newEntity = entity.Copy();

        newEntity.PropertyChanged += Entity_PropertyChanged;
        newEntity.Position.PropertyChanged += Entity_PropertyChanged;
        newEntity.EntityType.PropertyChanged += Entity_PropertyChanged;
        newEntity.EntityType.Child.PropertyChanged += Entity_PropertyChanged;

        Map.AddEntity(newEntity);
        this.RaisePropertyChanged(nameof(Entities));
        RefreshPreviewAsync();
        return newEntity;
    }

    protected void DeleteEntity(Entity entity)
    {
        Map.RemoveEntity(entity);
        this.RaisePropertyChanged(nameof(Entities));
        RefreshPreviewAsync();
    }

    protected void Entity_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        RefreshPreviewAsync();
    }
}
