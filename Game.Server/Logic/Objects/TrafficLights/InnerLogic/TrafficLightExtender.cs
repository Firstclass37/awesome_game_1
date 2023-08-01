using Game.Server.Logic.Maps;
using Game.Server.Models.Buildings;
using Game.Server.Models.Constants;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects.TrafficLights.InnerLogic
{
    internal class TrafficLightExtender : ITrafficLightExtender
    {
        private readonly IGameObjectAccessor _gameObjectAccessor;
        private readonly ITrafficLightManager _trafficLightManager;
        private readonly IMapGrid _mapGrid;

        public TrafficLightExtender(IGameObjectAccessor gameObjectAccessor, ITrafficLightManager trafficLightManager, IMapGrid mapGrid)
        {
            _gameObjectAccessor = gameObjectAccessor;
            _trafficLightManager = trafficLightManager;
            _mapGrid = mapGrid;
        }

        public bool TryExtend(Coordiante coordiante)
        {
            var neighbours = _mapGrid.GetNeightborsOf(coordiante);
            var objectHere = _gameObjectAccessor.Find(coordiante);
            if (objectHere.GameObject.ObjectType != BuildingTypes.Road)
                return false;

            foreach(var neighbor in neighbours) 
            {
                var gameObjectHere = _gameObjectAccessor.Find(neighbor.Key);
                if (gameObjectHere.GameObject.ObjectType != BuildingTypes.TrafficLigh)
                    continue;

                if (gameObjectHere != null)
                {
                    _trafficLightManager.ActivateDirection(new TrafficLight(gameObjectHere), _mapGrid.GetDirectionOfNeightbor(neighbor.Key, coordiante));
                    return true;
                }
            }

            return false;
        }
    }
}