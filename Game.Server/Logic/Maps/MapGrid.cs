using Game.Server.Models.Constants;
using Game.Server.Models.Maps;
using Game.Server.Storage;

namespace Game.Server.Logic.Maps
{
    internal class MapGrid : IMapGrid
    {
        private readonly IStorage _storage;

        public MapGrid(IStorage storage)
        {
            _storage = storage;
        }

        public Direction GetDirectionOfNeightbor(Coordiante coordiante, Coordiante neighbor)
        {
            var neighbors = GetNeightborsOf(coordiante);
            return neighbors[neighbor];
        }

        public IReadOnlyDictionary<Coordiante, Direction> GetNeightborsOf(Coordiante coordiante) => 
            _storage.Find<IsometricMapCell>(c => c.Equals(coordiante)).First().Neighbors.ToDictionary(k => k.Key.Coordiante, k => k.Value);
    }
}