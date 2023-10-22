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

        private void Reset(TrafficLight trafficLigh, IEnumerable<Direction> directions)
        {
            if (trafficLigh.GameObject.GetAttributeValue(TrafficLightAttributes.TrafficLightSidesValues).Values.All(v => v == 0))
                foreach (var direction in directions)
                {
                    var maxSizeValue = trafficLigh.GameObject.GetAttributeValue(TrafficLightAttributes.TrafficLightSidesCapacity)[direction];
                    _trafficLightManager.UpdateValue(trafficLigh, direction, maxSizeValue);
                }
                    
        }
    }
}