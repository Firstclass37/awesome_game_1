﻿using Game.Server.DataBuilding;
using Game.Server.Logic.Maps;
using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects.TrafficLights.Interaction;
using Game.Server.Models.Buildings;
using Game.Server.Models.Constants;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects.TrafficLights.Creation
{
    internal class TrafficLightFactory : IGameObjectFactory
    {
        private readonly IMapGrid _mapGrid;
        private readonly IGameObjectAccessor _gameObjectAccessor;

        public TrafficLightFactory(IMapGrid mapGrid, IGameObjectAccessor gameObjectAccessor)
        {
            _mapGrid = mapGrid;
            _gameObjectAccessor = gameObjectAccessor;
        }

        public GameObjectAggregator CreateNew(Coordiante root, Coordiante[] area)
        {
            var neighbors = _mapGrid.GetNeightborsOf(root);
            var roadNeigbors = neighbors.Where(n => _gameObjectAccessor.Find(n.Key)?.GameObject.ObjectType == BuildingTypes.Road)
                .ToDictionary(n => n.Value, n => n.Key);
            var trafficLightArea = roadNeigbors.Select(r => r.Value).Union(new[] { root }).ToArray();

            return new GameObjectAggregatorBuilder(BuildingTypes.TrafficLigh)
                .AddArea(root, trafficLightArea)
                .AddAttribute(AttrituteTypes.Interactable)
                .AddAttribute(TrafficLightAttributes.TrafficLightTrackingCells, roadNeigbors)
                .AddAttribute(TrafficLightAttributes.TrafficLightSidesCapacity, roadNeigbors.Select(d => d.Key).ToDictionary(d => d, d => 1))
                .AddAttribute(TrafficLightAttributes.TrafficLightSidesValues, roadNeigbors.Select(d => d.Key).ToDictionary(d => d, d => 0))
                .AddInteraction<TrafficLightInteraction>()
                .Build();
        }
    }
}