using Autofac;
using My_awesome_character.Core.Game;
using My_awesome_character.Core.Game.Buildings.Build;
using My_awesome_character.Core.Game.Buildings.Build.Factories;
using My_awesome_character.Core.Game.Buildings.Requirements;
using My_awesome_character.Core.Game.Movement;
using My_awesome_character.Core.Game.Movement.Path;
using My_awesome_character.Core.Game.Movement.Path_1;
using My_awesome_character.Core.Game.Resources;
using My_awesome_character.Core.Game.TrafficLights;
using My_awesome_character.Core.Infrastructure.Events;
using My_awesome_character.Core.Systems;
using My_awesome_character.Core.Systems.Builidngs;
using My_awesome_character.Core.Systems.Character;
using My_awesome_character.Core.Systems.Common;
using My_awesome_character.Core.Systems.Homes;
using My_awesome_character.Core.Systems.Resources;
using My_awesome_character.Core.Systems.TrafficLights;
using My_awesome_character.Core.Ui;

namespace My_awesome_character
{
    internal class Module: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Storage>().As<IStorage>().SingleInstance();
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
            builder.RegisterType<SceneAccessor>().As<ISceneAccessor>().SingleInstance();
            builder.RegisterType<PathSearcherSettingsFactory>().As<IPathSearcherSettingsFactory>().SingleInstance();
            builder.RegisterType<PathSearcher>().As<IPathSearcher>().SingleInstance();
            builder.RegisterType<ResourceManager>().As<IResourceManager>().SingleInstance();

            //character
            builder.RegisterType<CharacterMovement>().As<ICharacterMovement>();

            //traffic light
            builder.RegisterType<Pointsman>().As<IPointsman>();
            builder.RegisterType<TrafficLightInteraction>().As<ITrafficLightInteraction>();
            builder.RegisterType<TrafficLightManager>().As<ITrafficLightManager>();

            //other
            builder.RegisterType<BuildRequirementProvider>().As<IBuildRequirementProvider>().SingleInstance();

            builder.RegisterType<GameInitSystem>().As<ISystem>();

            builder.RegisterType<BuildingFactoryProvider>().As<IBuildingFactoryProvider>().SingleInstance();
            builder.RegisterType<HomeFactory>().AsSelf();
            builder.RegisterType<RoadFactory>().AsSelf();
            builder.RegisterType<PowerStationFactory>().AsSelf();
            builder.RegisterType<UranusMineFactory>().AsSelf();

            builder.RegisterType<MovementSystem>().As<ISystem>().SingleInstance();
            builder.RegisterType<RandomPathGeneratorSystem>().As<ISystem>().SingleInstance();
            //builder.RegisterType<InitCharacterSystem>().As<ISystem>().SingleInstance();
            builder.RegisterType<HomeCreatingSystem>().As<ISystem>().SingleInstance();
            builder.RegisterType<PeriodicActionsSystem>().As<ISystem>().SingleInstance();
            builder.RegisterType<CharacterCreationSystem>().As<ISystem>().SingleInstance();
            builder.RegisterType<HomePreviewSystem>().As<ISystem>().SingleInstance();
            builder.RegisterType<HomeBuildingPreviewConditionSystem>().As<ISystem>().SingleInstance();
            builder.RegisterType<BuildingLoadingCreationSystem>().As<ISystem>().SingleInstance();
            builder.RegisterType<InitResourcesInfoSystem>().As<ISystem>().SingleInstance();
            builder.RegisterType<IncreaseSourceSystem>().As<ISystem>().SingleInstance();
            builder.RegisterType<CharacterDamageSystem>().As<ISystem>().SingleInstance();
            builder.RegisterType<CharacterInteractionSytem>().As<ISystem>().SingleInstance();

            builder.RegisterType<TrafficLightCreationSystem>().As<ISystem>().SingleInstance();
            builder.RegisterType<TrafficLightLogicSystem>().As<ISystem>().SingleInstance();
            builder.RegisterType<TrafficLightInputsHandlerSystem>().As<ISystem>().SingleInstance();


            builder.RegisterType<ListSystem>().As<ISystem>().SingleInstance();



            builder.RegisterType<GameObjectCreationSystem>().As<ISystem>();
        }
    }
}