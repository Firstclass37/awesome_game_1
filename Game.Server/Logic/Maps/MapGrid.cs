using Game.Server.Models.Constants;
using Game.Server.Models.Maps;
using Game.Server.Storage;

namespace Game.Server.Logic.Maps
{
    internal class MapGrid : IMapGrid
    {
        private readonly IStorage _storage;
        private readonly Lazy<IReadOnlyCollection<Coordiante>> _mapGrid;
        private readonly Lazy<Dictionary<Coordiante, Guid>> _isometricCellIds;

        public MapGrid(IStorage storage)
        {
            _storage = storage;
            _mapGrid = new Lazy<IReadOnlyCollection<Coordiante>>(() => _storage.Find<IsometricMapCell>(c => true).Select(c => c.Coordiante).ToArray());
            _isometricCellIds = new Lazy<Dictionary<Coordiante, Guid>>(() => _storage.Find<IsometricMapCell>(i => true).ToDictionary(i => i.Coordiante, i => i.Id));
        }

        public Direction GetDirectionOfNeightbor(Coordiante coordiante, Coordiante neighbor)
        {
            var neighbors = GetNeightborsOf(coordiante);
            return neighbors[neighbor];
        }

        public IReadOnlyCollection<Coordiante> GetGrid() => _mapGrid.Value;

        public IReadOnlyDictionary<Coordiante, Direction> GetNeightborsOf(Coordiante coordiante) => 
            _storage.Get<IsometricMapCell>(_isometricCellIds.Value[coordiante]).Neighbors.ToDictionary(n => n.Key.Coordiante, n => n.Value);
    }
}