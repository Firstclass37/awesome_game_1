using Autofac;
using Game.Server.Events.Core;
using Game.Server.Logic;
using Game.Server.Logic.Maps;
using Game.Server.Storage;

namespace Game.Server
{
    public class GameServerModule: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var types = GetType().Assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && !t.IsGenericType).ToArray();

            builder.RegisterType<Storage.Storage>().As<IStorage>().SingleInstance();
            builder.RegisterType<MapGrid>().As<IMapGrid>().SingleInstance();
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
            builder.RegisterType<GameCycle>().As<IGameCycle>().SingleInstance();

            builder.RegisterTypes(types).AsSelf().AsImplementedInterfaces();
        }
    }
}