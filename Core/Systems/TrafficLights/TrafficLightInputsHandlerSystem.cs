using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Game.Constants;
using My_awesome_character.Core.Game.Events;
using My_awesome_character.Core.Game.Resources;
using My_awesome_character.Core.Infrastructure.Events;
using My_awesome_character.Core.Ui;

namespace My_awesome_character.Core.Systems.TrafficLights
{
    internal class TrafficLightInputsHandlerSystem : ISystem
    {
        private readonly ISceneAccessor _sceneAccessor;
        private readonly IEventAggregator _eventAggregator;
        private readonly IResourceManager _resourceManager;

        public TrafficLightInputsHandlerSystem(ISceneAccessor sceneAccessor, IEventAggregator eventAggregator, IResourceManager resourceManager)
        {
            _sceneAccessor = sceneAccessor;
            _eventAggregator = eventAggregator;
            _resourceManager = resourceManager;
        }

        public void OnStart()
        {
            _eventAggregator.GetEvent<GameEvent<TrafficLightsCreatedEvent>>().Subscribe(OnCreated);
        }

        public void Process(double gameTime)
        {
        }

        private void OnCreated(TrafficLightsCreatedEvent @event)
        {
            var trafficLight = _sceneAccessor.FindFirst<TrafficLight>(SceneNames.TrafficLight(@event.Id));
            trafficLight.OnLeftClick += (d) => TrafficLight_OnLeftClick(d, trafficLight);
            trafficLight.OnRightClick += (d) => TrafficLight_OnRightClick(d, trafficLight);
        }

        private readonly int _cost = 1;

        private void TrafficLight_OnLeftClick(Direction direction, TrafficLight trafficLight)
        {
            if (_resourceManager.TrySpend(ResourceType.Microchip, _cost))
                trafficLight.SetSize(direction, trafficLight.GetSize(direction) + 1);
        }

        private void TrafficLight_OnRightClick(Direction direction, TrafficLight trafficLight)
        {
            var currentSize = trafficLight.GetSize(direction);
            if (currentSize <= 0)
                return;

            if (_resourceManager.TrySpend(ResourceType.Microchip, _cost))
            {
                trafficLight.SetSize(direction, trafficLight.GetSize(direction) - 1);
                if (trafficLight.GetSize(direction) < trafficLight.GetValue(direction))
                    trafficLight.SetValue(direction, trafficLight.GetSize(direction));
            }
        }
    }
}