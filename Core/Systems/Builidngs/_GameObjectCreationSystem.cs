﻿using Game.Server.Events.Core;
using Game.Server.Events.List.Homes;
using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Game;
using My_awesome_character.Core.Ui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_awesome_character.Core.Systems.Builidngs
{
    internal class GameObjectCreationSystem : ISystem
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ISceneAccessor _sceneAccessor;

        public GameObjectCreationSystem(IEventAggregator eventAggregator, ISceneAccessor sceneAccessor)
        {
            _eventAggregator = eventAggregator;
            _sceneAccessor = sceneAccessor;
        }

        public void OnStart()
        {
            _eventAggregator.GetEvent<GameEvent<ObjectCreatedEvent>>().Subscribe(OnCreated);
        }

        private void OnCreated(ObjectCreatedEvent @event)
        {
            var map = _sceneAccessor.GetScene<Map>(SceneNames.Map);

            if (@event.ObjectType == GroundTypes.Ground)
            {
                var root = new CoordianteUI(@event.Root.X, @event.Root.Y);
                var area = @event.Area.Select(c => new CoordianteUI(c.X, c.Y)).ToArray();

                map.SetCell(@event.Id, root, area, MapLayers.GroundLayer, Tiles.Ground);
            }
            if (@event.ObjectType == BuildingTypesTrue.Road)
            {
                var root = new CoordianteUI(@event.Root.X, @event.Root.Y);
                var area = @event.Area.Select(c => new CoordianteUI(c.X, c.Y)).ToArray();

                map.SetCell(@event.Id, root, area, MapLayers.RoadLayer, Tiles.RoadAshpalt);
            }
            if (@event.ObjectType == BuildingTypesTrue.Home)
            {
                var root = new CoordianteUI(@event.Root.X, @event.Root.Y);
                var area = @event.Area.Select(c => new CoordianteUI(c.X, c.Y)).ToArray();

                map.SetCell(@event.Id, root, area, MapLayers.Buildings, Tiles.HomeType1);
            }
            if (@event.ObjectType == BuildingTypesTrue.UranusMine)
            {
                var root = new CoordianteUI(@event.Root.X, @event.Root.Y);
                var area = @event.Area.Select(c => new CoordianteUI(c.X, c.Y)).ToArray();

                map.SetCell(@event.Id, root, area, MapLayers.Buildings, Tiles.MineUranus);
            }
            if (@event.ObjectType == ResourceResourceTypes.Uranium)
            {
                var root = new CoordianteUI(@event.Root.X, @event.Root.Y);
                var area = @event.Area.Select(c => new CoordianteUI(c.X, c.Y)).ToArray();

                map.SetCell(@event.Id, root, area, MapLayers.Resources, Tiles.ResourceUranus);
            }
            if (@event.ObjectType == BuildingTypesTrue.Block)
            {
                var root = new CoordianteUI(@event.Root.X, @event.Root.Y);
                var area = @event.Area.Select(c => new CoordianteUI(c.X, c.Y)).ToArray();

                map.SetCell(@event.Id, root, area, MapLayers.Buildings, Tiles.Block);
            }
        }

        public void Process(double gameTime)
        {
        }
    }
}