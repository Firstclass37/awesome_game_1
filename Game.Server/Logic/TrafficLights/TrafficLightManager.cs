using Game.Server.Events.Core;
using Game.Server.Events.List.TrafficLights;
using Game.Server.Logic.Resources;
using Game.Server.Models.Buildings;
using Game.Server.Models.Constants;
using Game.Server.Storage;

namespace Game.Server.Logic.TrafficLights
{
    internal class TrafficLightManager : ITrafficLightManager
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IResourceManager _resourceManager;
        private readonly IStorage _storage;

        private readonly int _cost = 1;

        public TrafficLightManager(IEventAggregator eventAggregator, IResourceManager resourceManager, IStorage storage)
        {
            _eventAggregator = eventAggregator;
            _resourceManager = resourceManager;
            _storage = storage;
        }

        public TrafficLight Get(int id) => _storage.Get<TrafficLight>(id);

        public void AddTrafficLight(TrafficLight trafficLight)
        {
            trafficLight.Id = new Random().Next(0, 10000);
            _storage.Add(trafficLight);
            _eventAggregator.GetEvent<GameEvent<TrafficLightCreatedEvent>>().Publish(new TrafficLightCreatedEvent { Id = trafficLight.Id });
        }

        public void IncreaseSize(TrafficLight trafficLight, Direction direction, int increment = 1)
        {
            if (_resourceManager.TrySpend(ResourceType.Microchip, _cost))
            {
                trafficLight.Sizes[direction] = trafficLight.Sizes[direction] + increment;
                PublishChangedEvent(trafficLight, direction);
            }
        }

        public void DecreaseSize(TrafficLight trafficLight, Direction direction, int decrement = 1)
        {
            if (_resourceManager.TrySpend(ResourceType.Microchip, _cost))
            {
                trafficLight.Sizes[direction] = trafficLight.Sizes[direction] - decrement;
                PublishChangedEvent(trafficLight, direction);
            }
        }

        public void UpdateValue(TrafficLight trafficLight, Direction direction, int value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            trafficLight.CurrentValues[direction] = value;
            PublishChangedEvent(trafficLight, direction);
        }

        private void PublishChangedEvent(TrafficLight trafficLight, Direction direction)
        {
            _eventAggregator.GetEvent<GameEvent<TrafficLightDirectionChangedEvent>>()
            .Publish(new TrafficLightDirectionChangedEvent { TrafficLightId = trafficLight.Id, Direction = direction, Value = trafficLight.CurrentValues[direction], Size = trafficLight.Sizes[direction] });
        }
    }
}