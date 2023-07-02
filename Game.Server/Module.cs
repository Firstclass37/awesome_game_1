using Autofac;
using Game.Server.Storage;

namespace Game.Server
{
    internal class Module: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var types = GetType().Assembly.GetTypes();

            builder.RegisterType<Storage.Storage>().As<IStorage>().SingleInstance();

            builder.RegisterTypes(types).AsImplementedInterfaces();
        }
    }
}