using Game.Server.Events.Core;
using Game.Server.Events.Core.Extentions;
using Game.Server.Events.List.Homes;
using Game.Server.Events.List.TrafficLights;
using Game.Server.Logic.Maps;
using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects.TrafficLights.InnerLogic;
using Game.Server.Logic.Systems;
using Game.Server.Models.Buildings;
using Game.Server.Models.Constants;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects.TrafficLights.Systems
{
    internal class AutoCreationSystem : ISystem
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IGameObjectCreator _gameObjectCreator;
        private readonly ITrafficLightExtender _trafficLightExtender;
        private readonly IPlayerGrid _playerGrid;
        private readonly IMapGrid _mapGrid;

        public AutoCreationSystem(IEventAggregator eventAggregator, IGameObjectCreator gameObjectCreator, ITrafficLightExtender trafficLightExtender, IMapGrid mapGrid, IPlayerGrid playerGrid)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<GameEvent<ObjectCreatedEvent>>().Subscribe(OnCreated);

            _gameObjectCreator = gameObjectCreator;
            _trafficLightExtender = trafficLightExtender;
            _mapGrid = mapGrid;
            _playerGrid = playerGrid;
        }

        public void Process(double gameTime)
        {
            
        }

        private void OnCreated(ObjectCreatedEvent @event)
        {
            if (@event.ObjectType != BuildingTypes.Road)
                return;

            var candidates = _mapGrid.GetNeightborsOf(@event.Root)
                .Select(n => n.Key)
                .Union(new[] { @event.Root })
                .ToArray();

            foreach(var candidate in candidates)
            {
                var created = TryCreate(candidate);
                if (created != null)
                    PublishEvent(new TrafficLight(created));
            }

            _trafficLightExtender.TryExtend(@event.Root);
        }

        private GameObjectAggregator TryCreate(Coordiante coordiante)
        {
            var creationParams = new CreationParams(BuildingTypes.TrafficLigh, coordiante, _playerGrid.GetPlayerOf(coordiante));
            if (_gameObjectCreator.CanCreate(creationParams))
                return _gameObjectCreator.Create(creationParams);

            return default;
        }

        private void PublishEvent(TrafficLight trafficLight)
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