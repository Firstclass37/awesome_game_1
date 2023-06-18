using My_awesome_character.Core.Game.Constants;
using My_awesome_character.Core.Game.Events.TrafficLights;
using My_awesome_character.Core.Game.Models;
using My_awesome_character.Core.Game.Resources;
using My_awesome_character.Core.Infrastructure.Events;
using System;
using System.Collections.Generic;

namespace My_awesome_character.Core.Game.TrafficLights
{
    internal class TrafficLightManager : ITrafficLightManager
    {
        private readonly static Dictionary<int, TrafficLightModel> _lights = new();

        private readonly IEventAggregator _eventAggregator;
        private readonly IResourceManager _resourceManager;

        private readonly int _cost = 1;

        public TrafficLightManager(IEventAggregator eventAggregator, IResourceManager resourceManager)
        {
            _eventAggregator = eventAggregator;
            _resourceManager = resourceManager;
        }

        public TrafficLightModel Get(int id) => _lights[id];

        public void AddTrafficLight(TrafficLightModel trafficLightModel)
        {
            _lights.Add(trafficLightModel.Id, trafficLightModel);
            _eventAggregator.GetEvent<GameEvent<TrafficLightCreatedEvent>>().Publish(new TrafficLightCreatedEvent { Id = trafficLightModel.Id });
        }

        public void IncreaseSize(TrafficLightModel trafficLight, Direction direction, int increment = 1)
        {
            if (_resourceManager.TrySpend(ResourceType.Microchip, _cost))
            {
                trafficLight.Sizes[direction] = trafficLight.Sizes[direction] + increment;
                PublishChangedEvent(trafficLight, direction);
            }
        }

        public void DecreaseSize(TrafficLightModel trafficLight, Direction direction, int decrement = 1)
        {
            if (_resourceManager.TrySpend(ResourceType.Microchip, _cost))
            {
                trafficLight.Sizes[direction] = trafficLight.Sizes[direction] - decrement;
                PublishChangedEvent(trafficLight, direction);
            }
        }

        public void UpdateValue(TrafficLightModel trafficLight, Direction direction, int value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            trafficLight.CurrentValues[direction] = value;
            PublishChangedEvent(trafficLight, direction);
        }

        private void PublishChangedEvent(TrafficLightModel trafficLight, Direction direction)
        {
            _eventAggregator.GetEvent<GameEvent<TrafficLightDirectionChangedEvent>>()
            .Publish(new TrafficLightDirectionChangedEvent { TrafficLightId = trafficLight.Id, Direction = direction, Value = trafficLight.CurrentValues[direction], Size = trafficLight.Sizes[direction] });
        }
    }
}