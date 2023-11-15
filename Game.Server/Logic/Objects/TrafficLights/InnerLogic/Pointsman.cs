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
            var currentValues = trafficLight.GameObject.GetAttributeValue(TrafficLightAttributes.TrafficLightSidesValues);
            var directions = currentValues
                .Where(d => d.Key != from)
                .Select(d => d.Key)
                .ToArray();

            Reset(trafficLight, directions);

            var selectedDirection = directions
                .OrderByDescending(d => currentValues[d])
                .First();

            _trafficLightManager.UpdateValue(trafficLight, selectedDirection, currentValues[selectedDirection] - 1);
            return selectedDirection;
        }

        private void Reset(TrafficLight trafficLigh, IReadOnlyCollection<Direction> directions)
        {
            var needReset = trafficLigh.GameObject.GetAttributeValue(TrafficLightAttributes.TrafficLightSidesValues)
                .Where(s => directions.Contains(s.Key))
                .All(s => s.Value == 0);

            if (!needReset)
                return;

            foreach (var direction in directions)
            {
                var maxSizeValue = trafficLigh.GameObject.GetAttributeValue(TrafficLightAttributes.TrafficLightSidesCapacity)[direction];
                _trafficLightManager.UpdateValue(trafficLigh, direction, maxSizeValue);
            }
        }
    }
}