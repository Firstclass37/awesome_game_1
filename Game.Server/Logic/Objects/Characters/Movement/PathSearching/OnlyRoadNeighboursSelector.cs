using Game.Server.Logic._Extentions;
using Game.Server.Logic.Maps;
using Game.Server.Logic.Objects.Characters.Movement.PathSearching.AStar;
using Game.Server.Models.Constants;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects.Characters.Movement.PathSearching
{
    internal interface IOnlyRoadNeighboursSelector : INieighborsSearchStrategy<Coordiante> { };

    internal class OnlyRoadNeighboursSelector : IOnlyRoadNeighboursSelector
    {
        private readonly IMapGrid _mapGrid;
        private readonly IGameObjectAccessor _gameObjectAccessor;

        public OnlyRoadNeighboursSelector(IMapGrid mapGrid, IGameObjectAccessor gameObjectAccessor)
        {
            _mapGrid = mapGrid;
            _gameObjectAccessor = gameObjectAccessor;
        }

        public Coordiante[] Search(Coordiante element)
        {
            var neighbours = _mapGrid.GetNeightborsOf(element).Keys.ToArray();
            return neighbours
                .Select(n => new { Coordinate = n, Object = _gameObjectAccessor.Find(n) })
                .Where(n => n.Object != null)
                .Where(c =>  c.Object.GameObject.ObjectType == BuildingTypes.Road || c.Object.Interactable())
                .Select(n => n.Coordinate)
                .ToArray();
        }
    }
}