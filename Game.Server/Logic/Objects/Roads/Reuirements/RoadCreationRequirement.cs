using Game.Server.Logic.Maps;
using Game.Server.Logic.Objects._Requirements;
using Game.Server.Models.Constants;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects.Roads.Reuirements
{
    internal class RoadCreationRequirement : ICreationRequirement
    {
        private readonly IMapGrid _mapGrid;
        private readonly IGameObjectAccessor _gameObjectAccessor;

        private readonly HashSet<string> _occupingBuldingTypes = new HashSet<string>() 
        {
            BuildingTypes.Road,
            BuildingTypes.TrafficLigh // Добавлен, т.к. у светофора площадь включает в себя трекаемые клетки
        };

        public RoadCreationRequirement(IMapGrid mapGrid, IGameObjectAccessor gameObjectAccessor)
        {
            _mapGrid = mapGrid;
            _gameObjectAccessor = gameObjectAccessor;
        }

        public bool Satisfy(Coordiante coordiante, Dictionary<Coordiante, GameObjectAggregator> area)
        {
            var root = area.First();
            if (root.Value == null || root.Value.GameObject.ObjectType != BuildingTypes.Ground)
                return false;

            var roadNeigtbors = _mapGrid.GetNeightborsOf(root.Key)
                .Where(n => _occupingBuldingTypes.Contains(_gameObjectAccessor.Find(n.Key).GameObject.ObjectType))
                .ToDictionary(n => n.Key, n => n.Value);

            return WillBeInAngle(coordiante, roadNeigtbors) == false;
        }

        /// <summary>
        /// 0 0  1 0
        /// 1 0  0 0 
        /// </summary>
        
        private bool WillBeInAngle(Coordiante coordiante, Dictionary<Coordiante, Direction> roadNeighbours)
        {
            if (roadNeighbours.Count <= 1)
                return false;

            var pairs = new List<Direction[]>()
            {
                new Direction[]{ Direction.Left, Direction.Top },
                new Direction[]{ Direction.Left, Direction.Bottom },
                new Direction[]{ Direction.Right, Direction.Top },
                new Direction[]{ Direction.Right, Direction.Bottom }
            };

            foreach(var pair in pairs)
            {
                if (!pair.All(d => roadNeighbours.Values.Contains(d)))
                    continue;

                var firstStepDirection = pair[0];
                var secondStepDirection = pair[1];

                var firstNeightbour = _mapGrid.GetNeightborsOf(coordiante).Where(n => n.Value == firstStepDirection).FirstOrDefault().Key;
                if (firstNeightbour == null)
                    continue;

                var secondNeightbour = _mapGrid.GetNeightborsOf(firstNeightbour).Where(n => n.Value == secondStepDirection).FirstOrDefault().Key;
                if (secondNeightbour == null)
                    continue;

                var diagonalNeigbour = _gameObjectAccessor.Find(secondNeightbour);
                if (diagonalNeigbour != null && _occupingBuldingTypes.Contains(diagonalNeigbour.GameObject.ObjectType))
                    return true;
            }

            return false;
        }
    }
}