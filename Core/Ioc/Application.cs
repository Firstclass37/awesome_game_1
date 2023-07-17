using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Game.Server;

namespace My_awesome_character.Core.Ioc
{
    internal static class Application
    {
        private static IContainer _container;

        static Application()
        {
            var builder = new ContainerBuilder();
            var assemblies = Assembly.GetAssembly(typeof(Application));
            builder.RegisterAssemblyModules(assemblies);
            builder.RegisterModule(new GameServerModule());

            _container = builder.Build();
        }

        public static T Get<T>() => _container.Resolve<T>();

        public static T[] GetAll<T>() => _container.Resolve<IEnumerable<T>>().ToArray();
    }
}