using Game.Server.Events.Core;
using Game.Server.Events.List.Homes;
using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Game;
using My_awesome_character.Core.Ui;
using System;
using System.Collections.Generic;
using System.Linq;

namespace My_awesome_character.Core.Systems.Builidngs
{
    internal class GameObjectCreationSystem : ISystem
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ISceneAccessor _sceneAccessor;

        private readonly Dictionary<string, int> _resourcesToTileMapper = new Dictionary<string, int>()
        {
            { ResourceResourceTypes.Uranium, Tiles.ResourceUranus },
            { ResourceResourceTypes.Bauxite, Tiles.BauxiteResource },
            { ResourceResourceTypes.IronOre, Tiles.IronOreResource },
            { ResourceResourceTypes.Minerals, Tiles.MineralsResource },
            { ResourceResourceTypes.Coke, Tiles.CokeResource }
        };

        private readonly Dictionary<string, int> _buildingsToTileMapper = new Dictionary<string, int>()
        {
            { BuildingTypesTrue.Road, Tiles.RoadAshpalt },
            { BuildingTypesTrue.Home, Tiles.HomeType1 },
            { BuildingTypesTrue.UranusMine, Tiles.MineUranus },
            { BuildingTypesTrue.Block, Tiles.Block },
        };

        private readonly Dictionary<string, int> _groundToTileMapper = new Dictionary<string, int>
        {
            { GroundTypes.Ground, Tiles.Ground }
        };

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

            var root = new CoordianteUI(@event.Root.X, @event.Root.Y);
            var area = @event.Area.Select(c => new CoordianteUI(c.X, c.Y)).ToArray();

            if (_groundToTileMapper.ContainsKey(@event.ObjectType))
                map.SetCell(@event.Id, root, area, MapLayers.GroundLayer, _groundToTileMapper[@event.ObjectType]);
            else if (_buildingsToTileMapper.ContainsKey(@event.ObjectType))
                map.SetCell(@event.Id, root, area, MapLayers.RoadLayer, _buildingsToTileMapper[@event.ObjectType]);
            else if (_resourcesToTileMapper.ContainsKey(@event.ObjectType))
                map.SetCell(@event.Id, root, area, MapLayers.Resources, _resourcesToTileMapper[@event.ObjectType]);
        }

        public void Process(double gameTime)
        {
        }
    }
}