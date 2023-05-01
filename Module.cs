using Autofac;
using My_awesome_character.Core.Game;

namespace My_awesome_character
{
    internal class Module: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Storage>().AsImplementedInterfaces().SingleInstance();
        }
    }
}