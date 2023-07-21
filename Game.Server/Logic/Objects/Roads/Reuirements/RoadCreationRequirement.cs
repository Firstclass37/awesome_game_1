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
            var root = area.First().Key;
            var rootObject = _gameObjectAccessor.Find(root);
            if (rootObject.GameObject.ObjectType != BuildingTypes.Ground)
                return false;

            var neigtborsCoordinates = _mapGrid.GetNeightborsOf(root).Select(c => c.Key).ToArray();
            var roadNeigtbors = _storage.Find<GameObjectPosition>(p => neigtborsCoordinates.Contains(p.Coordiante))
                .Select(n => _gameObjectAccessor.Find(n.Coordiante))
                .Where(n => n.GameObject.ObjectType == BuildingTypes.Road)
                .ToArray();
            var roadNeigtborsDirection = roadNeigtbors.Select(n => _mapGrid.GetDirectionOfNeightbor(root, n.Area.First().Coordiante)).ToArray();

            return roadNeigtbors.Length <= 1 || 
                roadNeigtbors.Length == 2 && roadNeigtborsDirection.All(d => d == Direction.Left || d == Direction.Right) ||
                roadNeigtbors.Length == 2 && roadNeigtborsDirection.All(d => d == Direction.Top || d == Direction.Bottom);
        }
    }
}