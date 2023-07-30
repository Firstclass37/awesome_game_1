using Game.Server.API.TrafficLight;
using Game.Server.Events.Core;
using Game.Server.Events.List.TrafficLights;
using Game.Server.Models.Constants;
using Godot;
using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Game;
using My_awesome_character.Core.Game.Constants;
using My_awesome_character.Core.Ui;
using System.Linq;
using static Godot.Node;

namespace My_awesome_character.Core.Systems.TrafficLights
{
    internal class TrafficLightCreationSystem : ISystem
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ISceneAccessor _sceneAccessor;
        private readonly ITrafficLightController _trafficLightController;

        public TrafficLightCreationSystem(IEventAggregator eventAggregator, ISceneAccessor sceneAccessor, ITrafficLightController trafficLightController)
        {
            _eventAggregator = eventAggregator;
            _sceneAccessor = sceneAccessor;
            _trafficLightController = trafficLightController;
        }

        public void OnStart()
        {
            _eventAggregator.GetEvent<GameEvent<TrafficLightChangedEvent>>().Subscribe(OnCreated);
        }

        public void Process(double gameTime)
        {
        }

        private void OnCreated(TrafficLightChangedEvent @event)
        {
            var map = _sceneAccessor.GetScene<Map>(SceneNames.Map);
            var trafficLight = _sceneAccessor.FindFirst<TrafficLight>(SceneNames.TrafficLight(@event.Id));
            if (trafficLight == null)
                trafficLight = Create(@event, map);

            AddDirections(trafficLight, @event);
        }

        private TrafficLight Create(TrafficLightChangedEvent @event, Map map)
        {
            var trafficLight = SceneFactory.Create<TrafficLight>(SceneNames.TrafficLight(@event.Id), ScenePaths.TrafficLight);
            map.AddChild(trafficLight, forceReadableName: true, InternalMode.Back);

            trafficLight.Id = @event.Id;
            trafficLight.MapPosition = new CoordianteUI(@event.Position.X, @event.Position.Y);
            trafficLight.Scale = new Vector2(0.2f, 0.2f);
            trafficLight.Position = map.GetLocalPosition(trafficLight.MapPosition);
            trafficLight.OnLeftClick += (d) => TrafficLight_OnLeftClick(d, trafficLight);
            trafficLight.OnRightClick += (d) => TrafficLight_OnRightClick(d, trafficLight);
            return trafficLight;
        }

        private void AddDirections(TrafficLight trafficLight, TrafficLightChangedEvent @event )
        {
            var values = @event.Values.ToDictionary(v => (DirectionUI)((int)v.Key), v => v.Value);
            var capacities = @event.Capasities.ToDictionary(v => (DirectionUI)((int)v.Key), v => v.Value);
            var disabledDirections = trafficLight.GetActiveDirections().Except(capacities.Keys).ToArray();

            foreach( var direction in disabledDirections)
                trafficLight.Deactivate(direction);

            foreach(var capacity in capacities)
            {
                if (!trafficLight.IsActiveDirection(capacity.Key))
                    trafficLight.Activate(capacity.Key, default);

                trafficLight.SetSize(capacity.Key, capacity.Value);
                trafficLight.SetValue(capacity.Key, 0);
            }

            foreach (var value in values)
                trafficLight.SetValue(value.Key, value.Value);
        }

        private void TrafficLight_OnLeftClick(DirectionUI direction, TrafficLight trafficLight)
        {
            _trafficLightController.Increase(trafficLight.Id, (Direction)((int)direction));
        }

        private void TrafficLight_OnRightClick(DirectionUI direction, TrafficLight trafficLight)
        {
            _trafficLightController.Decrease(trafficLight.Id, (Direction)((int)direction));
        }
    }
}