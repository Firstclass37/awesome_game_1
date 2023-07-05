using Game.Server.Logic.Maps;
using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects.TrafficLights.Interaction;
using Game.Server.Models;
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
            var readNeigbors = neighbors.Where(n => _gameObjectAccessor.Find(n.Key)?.GameObject.ObjectType == BuildingTypes.Road)
                .ToDictionary(n => n.Value, n => n.Key);

            var gameObject = new GameObject(BuildingTypes.TrafficLigh);
            var positions = area.Select(a => new GameObjectPosition(gameObject.Id, root, a.Equals(root))).ToList();
            var interactions = new List<GameObjectInteraction>()
            {
                new GameObjectInteraction(gameObject.Id, typeof(TrafficLightInteraction).FullName)
            };

            var attributes = new List<GameObjectToAttribute>()
            {
                new GameObjectToAttribute(gameObject.Id, AttributeType.TrafficLightTrackingCells, readNeigbors),
                new GameObjectToAttribute(gameObject.Id, AttributeType.TrafficLightSidesCapacity, neighbors.Select(d => d.Value).ToDictionary(d => d, d => 1)),
                new GameObjectToAttribute(gameObject.Id, AttributeType.TrafficLightSidesValues, neighbors.Select(d => d.Value).ToDictionary(d => d, d => 0))
            };

            return new GameObjectAggregator
            {
                GameObject = gameObject,
                Area = positions,
                Attributes = attributes,
                Interactions = interactions,
            };
        }
    }
}