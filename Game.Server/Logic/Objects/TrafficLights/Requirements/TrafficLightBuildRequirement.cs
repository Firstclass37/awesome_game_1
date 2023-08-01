using Game.Server.Logic.Maps;
using Game.Server.Logic.Objects._Requirements;
using Game.Server.Models.Constants;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;
using Game.Server.Storage;

namespace Game.Server.Logic.Objects.TrafficLights.Requirements
{
    internal class TrafficLightBuildRequirement : ICreationRequirement
    {
        private readonly IMapGrid _mapGrid;
        private readonly IStorage _storage;

        public TrafficLightBuildRequirement(IMapGrid mapGrid, IStorage storage)
        {
            _mapGrid = mapGrid;
            _storage = storage;
        }

        //todo: тут передавать еще root
        public bool Satisfy(Coordiante root, Dictionary<Coordiante, GameObjectAggregator> area)
        {
            var rootObj = area[root];
            if (rootObj == null || rootObj.GameObject.ObjectType != BuildingTypes.Road)
                return false;

            var neigtborsCoordinates = _mapGrid.GetNeightborsOf(root).Select(c => c.Key).ToArray();
            var neigtbors = _storage.Find<GameObjectPosition>(p => neigtborsCoordinates.Contains(p.Coordiante)).Select(e => e.EntityId).ToArray();

            var roadsAround = _storage.Find<GameObject>(o => o.ObjectType == BuildingTypes.Road).Select(r => r.Id)
                .Intersect(neigtbors)
                .Count();

            return roadsAround > 2;
        }
    }
}