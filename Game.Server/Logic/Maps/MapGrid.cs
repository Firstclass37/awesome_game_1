using Game.Server.Models.Constants;
using Game.Server.Models.Maps;
using Game.Server.Storage;

namespace Game.Server.Logic.Maps
{
    internal class MapGrid : IMapGrid
    {
        private readonly IStorage _storage;
        private readonly Lazy<IReadOnlyCollection<Coordiante>> _mapGrid;


        public MapGrid(IStorage storage)
        {
            _storage = storage;
            _mapGrid = new Lazy<IReadOnlyCollection<Coordiante>>(() => _storage.Find<IsometricMapCell>(c => true).Select(c => c.Coordiante).ToArray());
        }

        public Direction GetDirectionOfNeightbor(Coordiante coordiante, Coordiante neighbor)
        {
            var neighbors = GetNeightborsOf(coordiante);
            return neighbors[neighbor];
        }

        public IReadOnlyCollection<Coordiante> GetGrid() => _mapGrid.Value;

        public IReadOnlyDictionary<Coordiante, Direction> GetNeightborsOf(Coordiante coordiante) => 
            _storage.Find<IsometricMapCell>(c => c.Coordiante.Equals(coordiante)).First().Neighbors.ToDictionary(k => k.Key.Coordiante, k => k.Value);
    }
}