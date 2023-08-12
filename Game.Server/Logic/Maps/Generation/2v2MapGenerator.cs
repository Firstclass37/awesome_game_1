using Game.Server.Logger;
using Game.Server.Logic.Objects._Buidling;
using Game.Server.Map;
using Game.Server.Models.Constants;
using Game.Server.Models.Maps;
using Game.Server.Storage;

namespace Game.Server.Logic.Maps.Generation
{
    internal class Map2x2Generator : IMapGenerator
    {
        private readonly IPhantomNeighboursAccessor _phantomNeighboursAccessor;
        private readonly IGameObjectCreator _gameObjectCreator;
        private readonly IStorage _storage;

        private readonly ILogger _logger;

        private readonly int Widht = 20;
        private readonly int Height = 40;

        public Map2x2Generator(IPhantomNeighboursAccessor phantomNeighboursAccessor, IGameObjectCreator gameObjectCreator, IStorage storage, ILogger logger)
        {
            _phantomNeighboursAccessor = phantomNeighboursAccessor;
            _gameObjectCreator = gameObjectCreator;
            _storage = storage;
            _logger = logger;
        }

        public void Generate()
        {
            var center = new Coordiante(0, 0);
            CreateGroundGrid(center);
        }

        private void CreateGroundGrid(Coordiante center)
        {
            var centers = GetRowsCenters(center, Height).ToArray();
            _logger.Info($"centers count: {centers.Length}");

            foreach (var rowCenter in centers)
            {
                var rowCoordinates = CreateRow(rowCenter, Widht).ToArray();
                foreach (var coordinate in rowCoordinates)
                {
                    AddCoordinateInfo(coordinate);
                    _gameObjectCreator.Create(BuildingTypes.Ground, coordinate);
                }
            }

            InitNeigbours();
        }

        private IEnumerable<Coordiante> GetRowsCenters(Coordiante center, int height)
        {
            var up = MoveTo(center, Direction.Top, height / 2);
            var down = MoveTo(center, Direction.Bottom, height / 2);
            return up.Union(down).Union(new[] { center} );
        }

        private IEnumerable<Coordiante> CreateRow(Coordiante coordiante, int widht)
        {
            var left = MoveTo(coordiante, Direction.Left, widht / 2);
            var right = MoveTo(coordiante, Direction.Right, widht / 2);
            return left.Union(right).Union(new[] { coordiante });
        }

        private IEnumerable<Coordiante> MoveTo(Coordiante coordiante, Direction direction, int count)
        {
            var current = coordiante;
            var i = count;
            while(i-- > 0)
            {
                current = _phantomNeighboursAccessor.GetNeightborsOf(current).First(v => v.Value == direction).Key;
                yield return current;
            }
        }

        private void AddCoordinateInfo(Coordiante coordiante)
        {
            var existing = _storage.Find<IsometricMapCell>(c => c.Coordiante == coordiante).FirstOrDefault();
            if (existing == null)
                _storage.Add(new IsometricMapCell(coordiante));
        }

        private void InitNeigbours()
        {
            foreach (var cell in _storage.Find<IsometricMapCell>(t => true))
            {
                var neightbours = _phantomNeighboursAccessor.GetNeightborsOf(cell.Coordiante);
                var isometricNeightbours = neightbours
                    .Select(n => _storage.Find<IsometricMapCell>(p => p.Coordiante == n.Key).FirstOrDefault())
                    .Where(n => n != null)
                    .ToDictionary(n => n, n => neightbours[n.Coordiante]);

                var newCell = cell with { Neighbors = isometricNeightbours };
                _storage.Update(newCell);
            }
        }
    }
}