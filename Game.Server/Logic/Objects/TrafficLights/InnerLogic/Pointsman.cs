using Game.Server.Models.Buildings;
using Game.Server.Models.Constants;

namespace Game.Server.Logic.Objects.TrafficLights.InnerLogic
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
            var directions = trafficLight.CurrentValues.Where(d => d.Key != from).Select(d => d.Key).ToArray();
            Reset(trafficLight, directions);

            var selectedDirection = directions
                .OrderByDescending(d => trafficLight.CurrentValues[d])
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