using Autofac;
using Game.Server.Storage;

namespace Game.Server
{
    public class GameServerModule: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var types = GetType().Assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && !t.IsGenericType).ToArray();

            builder.RegisterType<Storage.Storage>().As<IStorage>().SingleInstance();

            builder.RegisterTypes(types).AsSelf().AsImplementedInterfaces();
        }
    }
}