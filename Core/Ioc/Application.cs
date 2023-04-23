using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;


namespace My_awesome_character.Core.Ioc
{
    internal static class Application
    {
        private static IContainer _container;

        static Application()
        {
            var builder = new ContainerBuilder();
            var assemblies = Assembly.GetAssembly(typeof(Application));
            builder.RegisterAssemblyTypes(assemblies).AsImplementedInterfaces();
            builder.RegisterAssemblyModules(assemblies);
            _container = builder.Build();
        }

        public static T Get<T>() => _container.Resolve<T>();

        public static T[] GetAll<T>() => _container.Resolve<IEnumerable<T>>().ToArray();
    }
}