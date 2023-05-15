﻿using Autofac;
using My_awesome_character.Core.Game;
using My_awesome_character.Core.Game.Movement.Path;
using My_awesome_character.Core.Game.Movement.Path_1;
using My_awesome_character.Core.Infrastructure.Events;
using My_awesome_character.Core.Systems;
using My_awesome_character.Core.Systems.Character;
using My_awesome_character.Core.Systems.Homes;
using My_awesome_character.Core.Systems.Resources;
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

            builder.RegisterType<MovementSystem>().As<ISystem>().SingleInstance();
            builder.RegisterType<RandomPathGeneratorSystem>().As<ISystem>().SingleInstance();
            builder.RegisterType<InitCharacterSystem>().As<ISystem>().SingleInstance();
            builder.RegisterType<HomeCreatingSystem>().As<ISystem>().SingleInstance();
            builder.RegisterType<HomeCharacterSpawnSystem>().As<ISystem>().SingleInstance();
            builder.RegisterType<CharacterCreationSystem>().As<ISystem>().SingleInstance();
            builder.RegisterType<HomePreviewSystem>().As<ISystem>().SingleInstance();
            builder.RegisterType<HomeBuildingPreviewConditionSystem>().As<ISystem>().SingleInstance();
            builder.RegisterType<InitResourcesInfoSystem>().As<ISystem>().SingleInstance();
        }
    }
}