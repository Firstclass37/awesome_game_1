﻿using Autofac;
using Game.Server.DataAccess;
using Game.Server.Events.Core;
using Game.Server.Logic;
using Game.Server.Logic.Maps;
using Game.Server.Logic.Resources;
using Game.Server.Storage;

namespace Game.Server
{
    public class GameServerModule: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var types = GetType().Assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && !t.IsGenericType)
                .Where(t => t != typeof(MyStorage) && t != typeof(GameObjectPositionCacheDecorator))
                .ToArray();


            builder.RegisterType<MapGrid>().As<IMapGrid>().SingleInstance();
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
            builder.RegisterType<GameCycle>().As<IGameCycle>().SingleInstance();
            builder.RegisterType<ResourceManager>().As<IResourceManager>().SingleInstance();

            //builder.RegisterDecorator<GameObjectPositionCacheDecorator, IStorage>();

            builder.RegisterInstance(new GameObjectPositionCacheDecorator(new MyStorage()))
                .As<IStorage>()
                .As<IGameObjectPositionCacheDecorator>()
                .SingleInstance();

            builder.RegisterTypes(types).AsSelf().AsImplementedInterfaces();
        }
    }
}