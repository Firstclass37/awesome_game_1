using Game.Server.Common.Extentions;
using Game.Server.DataAccess;
using Game.Server.Events.Core;
using Game.Server.Events.Core.Extentions;
using Game.Server.Events.List.TrafficLights;
using Game.Server.Logic.Maps;
using Game.Server.Logic.Resources;
using Game.Server.Models.Buildings;
using Game.Server.Models.Constants;

namespace Game.Server.Logic.Objects.TrafficLights.InnerLogic
{
    internal class TrafficLightManager : ITrafficLightManager
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IResourceManager _resourceManager;
        private readonly IMapGrid _mapGrid;
        private readonly IGameObjectAgregatorRepository _gameObjectAgregatorRepository;

        private readonly int _cost = 1;

        public TrafficLightManager(IEventAggregator eventAggregator, IResourceManager resourceManager, IMapGrid mapGrid, IGameObjectAgregatorRepository gameObjectAgregatorRepository)
        {
            _eventAggregator = eventAggregator;
            _resourceManager = resourceManager;
            _mapGrid = mapGrid;
            _gameObjectAgregatorRepository = gameObjectAgregatorRepository;
        }

        public void IncreaseSize(TrafficLight trafficLight, Direction direction, int increment = 1)
        {
            if (_resourceManager.TrySpend(ResourceType.Microchip, _cost))
            {
                trafficLight.GameObject.ModifyAttribute(TrafficLightAttributes.TrafficLightSidesCapacity, v => v.Update(direction, v[direction] + increment));

                _gameObjectAgregatorRepository.Update(trafficLight.GameObject);

                PublishChangedEvent(trafficLight);
            }
        }

        public void DecreaseSize(TrafficLight trafficLight, Direction direction, int decrement = 1)
        {
            var currentCapacities = trafficLight.GameObject.GetAttributeValue(TrafficLightAttributes.TrafficLightSidesCapacity);
            var newCapacity = currentCapacities[direction] - decrement;
            if (newCapacity < 0)
                return;

            if (_resourceManager.TrySpend(ResourceType.Microchip, _cost))
            {
                var currentValue = trafficLight.GameObject.GetAttributeValue(TrafficLightAttributes.TrafficLightSidesValues)[direction];
                if (currentValue > newCapacity)
                    trafficLight.GameObject.ModifyAttribute(TrafficLightAttributes.TrafficLightSidesValues, v => v.Update(direction, newCapacity));

                trafficLight.GameObject.ModifyAttribute(TrafficLightAttributes.TrafficLightSidesCapacity, v => v.Update(direction, newCapacity));

                _gameObjectAgregatorRepository.Update(trafficLight.GameObject);
                PublishChangedEvent(trafficLight);
            }
        }

        public void ActivateDirection(TrafficLight trafficLight, Direction direction)
        {
            if (trafficLight.GameObject.GetAttributeValue(TrafficLightAttributes.TrafficLightSidesValues).ContainsKey(direction))
                return;

            var newAreaPoint = _mapGrid.GetNeightborsOf(trafficLight.GameObject.RootCell).First(n => n.Value == direction).Key;

            trafficLight.GameObject.ModifyAttribute(TrafficLightAttributes.TrafficLightSidesCapacity, v => v.AddNew(direction, 1));
            trafficLight.GameObject.ModifyAttribute(TrafficLightAttributes.TrafficLightSidesValues, v => v.AddNew(direction, 1));
            trafficLight.GameObject.ModifyAttribute(TrafficLightAttributes.TrafficLightTrackingCells, v => v.AddNew(direction, newAreaPoint));
            trafficLight.GameObject.ExpandArea(newAreaPoint);

            _gameObjectAgregatorRepository.Update(trafficLight.GameObject);

            PublishChangedEvent(trafficLight);
        }

        public void UpdateValue(TrafficLight trafficLight, Direction direction, int value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            trafficLight.GameObject.ModifyAttribute(TrafficLightAttributes.TrafficLightSidesValues, v => v.Update(direction, value));
            _gameObjectAgregatorRepository.Update(trafficLight.GameObject);

            PublishChangedEvent(trafficLight);
        }

        private void PublishChangedEvent(TrafficLight trafficLight)
        {
            _eventAggregator.PublishGameEvent(new TrafficLightChangedEvent
            {
                Id = trafficLight.Id,
                Position = trafficLight.GameObject.RootCell,
                Capasities = trafficLight.GameObject.GetAttributeValue(TrafficLightAttributes.TrafficLightSidesCapacity),
                Values = trafficLight.GameObject.GetAttributeValue(TrafficLightAttributes.TrafficLightSidesValues)
            });
        }
    }
}