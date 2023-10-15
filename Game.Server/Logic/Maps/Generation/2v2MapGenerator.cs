using Game.Server.Logger;
using Game.Server.Logic.Objects._Buidling;
using Game.Server.Map;
using Game.Server.Models.Constants;
using Game.Server.Models.GameObjects;
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

        private readonly int BaseWidht = 30;
        private readonly int BaseHeight = 50;

        public Map2x2Generator(IPhantomNeighboursAccessor phantomNeighboursAccessor, IGameObjectCreator gameObjectCreator, IStorage storage, ILogger logger)
        {
            _phantomNeighboursAccessor = phantomNeighboursAccessor;
            _gameObjectCreator = gameObjectCreator;
            _storage = storage;
            _logger = logger;
        }

        public int[] Generate()
        {
            var center = new Coordiante(0, 0);
            CreateGroundGrid(center);
            return new int[] { 1, 2 };
        }

        private void CreateGroundGrid(Coordiante center)
        {
            var up = MoveTo(center, Direction.Top, BaseHeight / 2).ToArray();
            var down = MoveTo(center, Direction.Bottom, BaseHeight / 2).ToArray();
            var centers = up.Union(down).Union(new[] { center }).ToArray();

            var centerLeft = MoveTo(center, Direction.Left, BaseWidht / 2).ToArray();
            var centerRight = MoveTo(center, Direction.Right, BaseWidht / 2).ToArray();
            var leftRoadIndex = centerLeft.Length / 2;
            var rightRoadIndex = centerRight.Length / 2;
            var roadStartPoints = new Coordiante[]
            {
                center,
                centerLeft[leftRoadIndex],
                centerRight[rightRoadIndex]
            };

            foreach (var rowCenter in centers)
            {
                var rowCoordinates = CreateRow(rowCenter, BaseWidht / 2).ToArray();
                foreach (var coordinate in rowCoordinates)
                {
                    AddCoordinateInfo(coordinate);
                    
                    _gameObjectCreator.Create(BuildingTypes.Ground, coordinate);

                    var playerNumber = up.Contains(rowCenter) ? 1 : 2;
                    _storage.Add(new PlayerToPosition { Coordinate = coordinate, PlayerNumber = playerNumber });
                }
            }

            foreach (var coordinate in CreateRow(center, BaseWidht/2).Except(roadStartPoints))
                _gameObjectCreator.Create(BuildingTypes.Block, coordinate);
             
            var roadLenght = (int)((BaseHeight / 2) * 0.6);
            foreach(var roadStartPoint in roadStartPoints)
            {
                var upRoad = MoveTo(roadStartPoint, Direction.Top, roadLenght).ToArray();
                var downRoad = MoveTo(roadStartPoint, Direction.Bottom, roadLenght).ToArray();

                _gameObjectCreator.Create(BuildingTypes.Road, roadStartPoint);
                foreach (var coordinate in upRoad.Union(downRoad))
                    _gameObjectCreator.Create(BuildingTypes.Road, coordinate);
            }

            var baseHeight = 3;
            var topBase = MoveTo(center, Direction.Bottom).Skip(roadLenght).Take(baseHeight);
            var bottomBase = MoveTo(center, Direction.Top).Skip(roadLenght).Take(baseHeight);
            foreach (var point in topBase.Union(bottomBase))
            {
                var leftPart = MoveTo(point, Direction.Left, leftRoadIndex + 1);
                var rightPart = MoveTo(point, Direction.Right, rightRoadIndex + 1);

                _gameObjectCreator.Create(BuildingTypes.Road, point);
                foreach (var coordinate in leftPart.Union(rightPart))
                    _gameObjectCreator.Create(BuildingTypes.Road, coordinate);
            }


            InitNeigbours();
        }

        private IEnumerable<Coordiante> CreateRow(Coordiante coordiante, int count)
        {
            var left = MoveTo(coordiante, Direction.Left, count);
            var right = MoveTo(coordiante, Direction.Right, count);
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

        private IEnumerable<Coordiante> MoveTo(Coordiante coordiante, Direction direction)
        {
            var current = coordiante;
            var i = BaseWidht + BaseHeight;
            while (i-- > 0)
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