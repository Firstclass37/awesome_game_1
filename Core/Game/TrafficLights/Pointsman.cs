using My_awesome_character.Core.Game.Constants;
using My_awesome_character.Core.Game.Models;
using System.Collections.Generic;
using System.Linq;

namespace My_awesome_character.Core.Game.TrafficLights
{
    internal class Pointsman : IPointsman
    {
        private readonly ITrafficLightManager _trafficLightManager;

        public Pointsman(ITrafficLightManager trafficLightManager)
        {
            _trafficLightManager = trafficLightManager;
        }

        public Direction SelectDirection(TrafficLightModel trafficLight, Direction from)
        {
            Reset(trafficLight, trafficLight.Tracking.Keys.Where(d => d != from));

            var selectedDirection = trafficLight.Tracking
                .OrderByDescending(d => trafficLight.CurrentValues[d.Key])
                .Select(d => d.Key)
                .First();

            _trafficLightManager.UpdateValue(trafficLight, selectedDirection, trafficLight.CurrentValues[selectedDirection] - 1);
            return selectedDirection;
        }

        private void Reset(TrafficLightModel trafficLigh, IEnumerable<Direction> directions)
        {
            if (directions.All(d => trafficLigh.CurrentValues[d] == 0))
                foreach (var direction in directions)
                    _trafficLightManager.UpdateValue(trafficLigh, direction, trafficLigh.Sizes[direction]);
        }
    }
}