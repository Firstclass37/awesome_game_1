using Autofac;
using Game.Server.Logger;
using My_awesome_character.Core.Infrastructure.Events;
using My_awesome_character.Core.Systems;
using My_awesome_character.Core.Systems.Builidngs;
using My_awesome_character.Core.Systems.Character;
using My_awesome_character.Core.Systems.Homes;
using My_awesome_character.Core.Systems.Resources;
using My_awesome_character.Core.Systems.TrafficLights;
using My_awesome_character.Core.Ui;
using My_awesome_character.Logger;

namespace My_awesome_character
{
    internal class Module: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
            builder.RegisterType<SceneAccessor>().As<ISceneAccessor>().SingleInstance();

            builder.RegisterType<MovementSystem>().As<ISystem>().SingleInstance();
            builder.RegisterType<CharacterCreationSystem>().As<ISystem>().SingleInstance();
            builder.RegisterType<HomePreviewSystem>().As<ISystem>().SingleInstance();
            builder.RegisterType<BuildingLoadingCreationSystem>().As<ISystem>().SingleInstance();
            builder.RegisterType<InitResourcesInfoSystem>().As<ISystem>().SingleInstance();
            builder.RegisterType<IncreaseSourceSystem>().As<ISystem>().SingleInstance();
            builder.RegisterType<CharacterDeathSystem>().As<ISystem>().SingleInstance();

            builder.RegisterType<TrafficLightCreationSystem>().As<ISystem>().SingleInstance();

            builder.RegisterType<BuildingListSystem>().As<ISystem>().SingleInstance();

            builder.RegisterType<GameObjectCreationSystem>().As<ISystem>();

            builder.RegisterType<GodotLogger>().As<ILogger>();
        }
    }
}