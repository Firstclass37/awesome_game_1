using Game.Server.Events.Core;
using Game.Server.Events.Core.Extentions;
using Game.Server.Events.List.TrafficLights;
using Game.Server.Logic.Resources;
using Game.Server.Models.Buildings;
using Game.Server.Models.Constants;
using Game.Server.Models.GameObjects;
using Game.Server.Storage;

namespace Game.Server.Logic.Objects.TrafficLights.InnerLogic
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

        public void IncreaseSize(TrafficLight trafficLight, Direction direction, int increment = 1)
        {
            if (_resourceManager.TrySpend(ResourceType.Microchip, _cost))
            {
                var currentValues = trafficLight.Sizes.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
                currentValues[direction] = trafficLight.Sizes[direction] + increment;

                var attribute = trafficLight.GameObject.Attributes.First(a => a.AttributeType == AttributeType.TrafficLightSidesValues);
                var newAttribute = new GameObjectToAttribute(trafficLight.Id, AttributeType.TrafficLightSidesValues, currentValues);
                newAttribute.Id = attribute.Id;
                _storage.Update(newAttribute);

                trafficLight.GameObject.Attributes.Remove(attribute);
                trafficLight.GameObject.Attributes.Add(newAttribute);

                PublishChangedEvent(trafficLight);
            }
        }

        public void DecreaseSize(TrafficLight trafficLight, Direction direction, int decrement = 1)
        {
            if (_resourceManager.TrySpend(ResourceType.Microchip, _cost))
            {
                var currentValues = trafficLight.Sizes.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
                currentValues[direction] = trafficLight.Sizes[direction] - decrement;

                var attribute = trafficLight.GameObject.Attributes.First(a => a.AttributeType == AttributeType.TrafficLightSidesValues);
                var newAttribute = new GameObjectToAttribute(trafficLight.Id, AttributeType.TrafficLightSidesValues, currentValues);
                newAttribute.Id = attribute.Id;
                _storage.Update(newAttribute);

                trafficLight.GameObject.Attributes.Remove(attribute);
                trafficLight.GameObject.Attributes.Add(newAttribute);

                PublishChangedEvent(trafficLight);
            }
        }

        public void ActivateDirection(TrafficLight trafficLight, Direction direction)
        {
            if (trafficLight.Sizes.ContainsKey(direction))
                return;

            var currentValues = trafficLight.CurrentValues.ToDictionary(k => k.Key, k => k.Value);
            var currentCapacity = trafficLight.Sizes.ToDictionary(k => k.Key, k => k.Value);
            currentValues.Add(direction, 0);
            currentCapacity.Add(direction, 1);

            var capacityAttribute = trafficLight.GameObject.Attributes.First(a => a.AttributeType == AttributeType.TrafficLightSidesCapacity);
            var valuesAttribute = trafficLight.GameObject.Attributes.First(a => a.AttributeType == AttributeType.TrafficLightSidesValues);

            var newCapacityAttribute = capacityAttribute with { Value = currentCapacity };
            var newValuesAttribute = valuesAttribute with { Value = currentValues };

            _storage.Update(newCapacityAttribute);
            _storage.Update(newValuesAttribute);

            trafficLight.GameObject.Attributes.Remove(capacityAttribute);
            trafficLight.GameObject.Attributes.Remove(valuesAttribute);
            trafficLight.GameObject.Attributes.Add(newCapacityAttribute);
            trafficLight.GameObject.Attributes.Add(newValuesAttribute);

            PublishChangedEvent(trafficLight);
        }

        public void UpdateValue(TrafficLight trafficLight, Direction direction, int value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            var currentValues = trafficLight.Sizes.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            currentValues[direction] = value;

            var attribute = trafficLight.GameObject.Attributes.First(a => a.AttributeType == AttributeType.TrafficLightSidesCapacity);
            var newAttribute = new GameObjectToAttribute(trafficLight.Id, AttributeType.TrafficLightSidesCapacity, currentValues);
            newAttribute.Id = attribute.Id;
            _storage.Update(newAttribute);

            trafficLight.GameObject.Attributes.Remove(attribute);
            trafficLight.GameObject.Attributes.Add(newAttribute);

            PublishChangedEvent(trafficLight);
        }

        private void PublishChangedEvent(TrafficLight trafficLight)
        {
            _eventAggregator.PublishGameEvent(new TrafficLightChangedEvent
            {
                Id = trafficLight.Id,
                Position = trafficLight.RootCell,
                Capasities = trafficLight.Sizes,
                Values = trafficLight.CurrentValues
            });
        }
    }
}