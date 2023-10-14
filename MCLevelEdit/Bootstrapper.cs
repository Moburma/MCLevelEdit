using MCLevelEdit.DataModel;
using MCLevelEdit.Interfaces;
using MCLevelEdit.Services;
using MCLevelEdit.ViewModels;
using Splat;

namespace MCLevelEdit
{
    public static class Bootstrapper
    {
        public static void Register(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
        {
            services.RegisterLazySingleton<IMapService>(() => new MapService());
            services.RegisterLazySingleton(() => new MainViewModel(resolver.GetService<IMapService>()));
            services.RegisterLazySingleton(() => new EntitiesViewModel(resolver.GetService<IMapService>()));
            services.RegisterLazySingleton(() => new MapViewModel(resolver.GetService<IMapService>()));
            services.RegisterLazySingleton(() => new CreateEntityViewModel(resolver.GetService<IMapService>()));
            services.RegisterLazySingleton(() => new CreateTerrainViewModel(resolver.GetService<IMapService>()));
        }
    }
}