using Game.Server.Logic.Maps;
using Game.Server.Logic.Objects.TrafficLights.InnerLogic;
using Game.Server.Models.Constants;

namespace Game.Server.API.TrafficLight
{
    internal class TrafficLightController : ITrafficLightController
    {
        private readonly ITrafficLightManager _trafficLightManager;
        private readonly IGameObjectAccessor _gameObjectAccessor;

        public TrafficLightController(ITrafficLightManager trafficLightManager, IGameObjectAccessor gameObjectAccessor)
        {
            _trafficLightManager = trafficLightManager;
            _gameObjectAccessor = gameObjectAccessor;
        }

        public void Increase(Guid trafficLightId, Direction direction)
        {
            var gameObject = _gameObjectAccessor.Get(trafficLightId);
            _trafficLightManager.IncreaseSize(new Models.Buildings.TrafficLight(gameObject), direction);
        }

        public void Decrease(Guid trafficLightId, Direction direction)
        {
            var gameObject = _gameObjectAccessor.Get(trafficLightId);
            _trafficLightManager.DecreaseSize(new Models.Buildings.TrafficLight(gameObject), direction);
        }
    }
}