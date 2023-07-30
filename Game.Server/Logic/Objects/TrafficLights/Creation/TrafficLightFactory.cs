using Game.Server.DataBuilding;
using Game.Server.Logic.Maps;
using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects.TrafficLights.Interaction;
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

            return new GameObjectAggregatorBuilder(BuildingTypes.TrafficLigh)
                .AddArea(root, area)
                .AddAttribute(AttrituteTypes.Interactable)
                .AddAttribute(AttributeType.TrafficLightTrackingCells, readNeigbors)
                .AddAttribute(AttributeType.TrafficLightSidesCapacity, neighbors.Select(d => d.Value).ToDictionary(d => d, d => 1))
                .AddAttribute(AttributeType.TrafficLightSidesValues, neighbors.Select(d => d.Value).ToDictionary(d => d, d => 0))
                .AddInteraction<TrafficLightInteraction>()
                .Build();
        }
    }
}