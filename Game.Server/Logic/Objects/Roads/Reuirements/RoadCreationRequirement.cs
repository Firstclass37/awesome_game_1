using Game.Server.Logic.Maps;
using Game.Server.Logic.Objects._Requirements;
using Game.Server.Models.Constants;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;
using Game.Server.Storage;

namespace Game.Server.Logic.Objects.Roads.Reuirements
{
    internal class RoadCreationRequirement : ICreationRequirement
    {
        private readonly IMapGrid _mapGrid;
        private readonly IStorage _storage;
        private readonly IGameObjectAccessor _gameObjectAccessor;

        public RoadCreationRequirement(IMapGrid mapGrid, IStorage storage, IGameObjectAccessor gameObjectAccessor)
        {
            _mapGrid = mapGrid;
            _storage = storage;
            _gameObjectAccessor = gameObjectAccessor;
        }

        public bool Satisfy(Dictionary<Coordiante, GameObjectAggregator> area)
        {
            var root = area.First();
            if (root.Value == null || root.Value.GameObject.ObjectType != BuildingTypes.Ground)
                return false;

            var neigtborsCoordinates = _mapGrid.GetNeightborsOf(root.Key).Select(c => c.Key).ToArray();
            var roadNeigtbors = neigtborsCoordinates
                .Select(n => _gameObjectAccessor.Find(n))
                .Where(n => n.GameObject.ObjectType == BuildingTypes.Road)
                .ToArray();

            return roadNeigtbors.Length <= 2;
        }
    }
}