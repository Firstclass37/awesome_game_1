using Game.Server.Models.Buildings;
using Game.Server.Models.Constants;

namespace Game.Server.Logic.TrafficLights
{
    internal class Pointsman : IPointsman
    {
        private readonly ITrafficLightManager _trafficLightManager;

        public Pointsman(ITrafficLightManager trafficLightManager)
        {
            _trafficLightManager = trafficLightManager;
        }

        public Direction SelectDirection(TrafficLight trafficLight, Direction from)
        {
            Reset(trafficLight, trafficLight.Tracking.Keys.Where(d => d != from));

            var selectedDirection = trafficLight.Tracking
                .OrderByDescending(d => trafficLight.CurrentValues[d.Key])
                .Select(d => d.Key)
                .First();

            _trafficLightManager.UpdateValue(trafficLight, selectedDirection, trafficLight.CurrentValues[selectedDirection] - 1);
            return selectedDirection;
        }

        private void Reset(TrafficLight trafficLigh, IEnumerable<Direction> directions)
        {
            if (directions.All(d => trafficLigh.CurrentValues[d] == 0))
                foreach (var direction in directions)
                    _trafficLightManager.UpdateValue(trafficLigh, direction, trafficLigh.Sizes[direction]);
        }
    }
}