using Game.Server.Models.Constants;
using Game.Server.Models.Maps;
using Game.Server.Storage;

namespace Game.Server.Logic.Maps
{
    internal class MapGrid : IMapGrid
    {
        private readonly IStorage _storage;
        private readonly Lazy<IReadOnlyCollection<Coordiante>> _mapGrid;
        private readonly Lazy<Dictionary<Coordiante, Dictionary<Coordiante, Direction>>> _neighbors;

        public MapGrid(IStorage storage)
        {
            _storage = storage;
            _mapGrid = new Lazy<IReadOnlyCollection<Coordiante>>(() => _storage.Find<IsometricMapCell>(c => true).Select(c => c.Coordiante).ToArray());
            _neighbors = new Lazy<Dictionary<Coordiante, Dictionary<Coordiante, Direction>>>(
                () => _storage.Find<IsometricMapCell>(i => true).ToDictionary(i => i.Coordiante, i => i.Neighbors.ToDictionary(n => n.Key.Coordiante, n => n.Value)));
        }

        public Direction GetDirectionOfNeightbor(Coordiante coordiante, Coordiante neighbor) => _neighbors.Value[coordiante][neighbor];

        public IReadOnlyCollection<Coordiante> GetGrid() => _mapGrid.Value;

        public IReadOnlyDictionary<Coordiante, Direction> GetNeightborsOf(Coordiante coordiante) => _neighbors.Value[coordiante];
    }
}