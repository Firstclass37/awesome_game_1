using Game.Server.Models.Constants;
using Game.Server.Models.Maps;
using Game.Server.Storage;

namespace Game.Server.Logic.Maps
{
    internal class MapGrid : IMapGrid
    {
        private readonly IStorage _storage;
        private Dictionary<Coordiante, Dictionary<Coordiante, Direction>> _neighbors = new();

        public MapGrid(IStorage storage)
        {
            _storage = storage;
        }

        public Direction GetDirectionOfNeightbor(Coordiante coordiante, Coordiante neighbor) => GetDictionary()[coordiante][neighbor];

        public IReadOnlyCollection<Coordiante> GetGrid() => GetDictionary().Keys;

        public IReadOnlyDictionary<Coordiante, Direction> GetNeightborsOf(Coordiante coordiante) => GetDictionary()[coordiante];

        private Dictionary<Coordiante, Dictionary<Coordiante, Direction>> GetDictionary()
        {
            if (_neighbors.Any() && _neighbors.Any(c => c.Value.Any()))
                return _neighbors;

            _neighbors = _storage.Find<IsometricMapCell>(i => true).ToDictionary(i => i.Coordiante, i => i.Neighbors.ToDictionary(n => n.Key.Coordiante, n => n.Value));
            return _neighbors;
        }
    }
}