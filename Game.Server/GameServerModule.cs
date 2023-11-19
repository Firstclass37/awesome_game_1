using Autofac;
using Game.Server.Common;
using Game.Server.DataAccess;
using Game.Server.Events.Core;
using Game.Server.Logic;
using Game.Server.Logic.Maps;
using Game.Server.Logic.Objects._Core;
using Game.Server.Logic.Resources;
using Game.Server.Logic.Weapons;
using Game.Server.Storage;

namespace Game.Server
{
    public class GameServerModule: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var excludeRegistrationsTypes = new Type[]
            {
                typeof(MyStorage),
                typeof(StorageCacheDecorator),
                typeof(GameDiceGroup)
            };
            
            var types = GetType().Assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && !t.IsGenericType)
                .Where(t => excludeRegistrationsTypes.Contains(t) == false)
                .ToArray();


            builder.RegisterType<MapGrid>().As<IMapGrid>().SingleInstance();
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
            builder.RegisterType<GameCycle>().As<IGameCycle>().SingleInstance();
            builder.RegisterType<ResourceManager>().As<IResourceManager>().SingleInstance();
            builder.RegisterType<MatrixGrid>().As<IMatrixGrid>().SingleInstance();
            builder.RegisterType<Arsenal>().As<IArsenal>().SingleInstance();

            builder
                .RegisterGeneric(typeof(TypesCollection<>))
                .As(typeof(ITypesCollection<>))
                .SingleInstance();

            //builder.RegisterDecorator<GameObjectPositionCacheDecorator, IStorage>();

            builder.RegisterInstance(new StorageCacheDecorator(new MyStorage()))
                .As<IStorage>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterTypes(types).AsSelf().AsImplementedInterfaces();
        }
    }
}