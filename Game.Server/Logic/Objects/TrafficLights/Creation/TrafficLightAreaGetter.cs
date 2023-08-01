using Game.Server.Logic.Maps;
using Game.Server.Logic.Objects._Buidling;
using Game.Server.Models.Constants;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects.TrafficLights.Creation
{
    internal interface ITrafficLightAreaGetter : IAreaGetter { }

    internal class TrafficLightAreaGetter : ITrafficLightAreaGetter
    {
        private readonly IGameObjectAccessor _gameObjectAccessor;
        private readonly IMapGrid _mapGrid;

        public TrafficLightAreaGetter(IGameObjectAccessor gameObjectAccessor, IMapGrid mapGrid)
        {
            _gameObjectAccessor = gameObjectAccessor;
            _mapGrid = mapGrid;
        }

        public Coordiante[] GetArea(Coordiante root)
        {
            return _mapGrid
                .GetNeightborsOf(root)
                .Select(r => r.Key)
                .Where(r => _gameObjectAccessor.Find(r)?.GameObject.ObjectType == BuildingTypes.Road)
                .Union(new[] { root})
                .ToArray();
        }
    }
}
