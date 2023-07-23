using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Game;
using My_awesome_character.Core.Game.Events.Character;
using My_awesome_character.Core.Infrastructure.Events;
using My_awesome_character.Core.Ui;
using System;
using System.Linq;

namespace My_awesome_character.Core.Systems.TrafficLights
{
    internal class TrafficLightLogicSystem : ISystem
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ISceneAccessor _sceneAccessor;

        private static object _lock = new object();

        public TrafficLightLogicSystem(IEventAggregator eventAggregator, ISceneAccessor sceneAccessor)
        {
            _eventAggregator = eventAggregator;
            _sceneAccessor = sceneAccessor;
        }

        public void OnStart()
        {
            _eventAggregator.GetEvent<GameEvent<CharacterPositionChangedEvent>>().Subscribe(OnCharacterMove);
            _eventAggregator.GetEvent<GameEvent<CharacterPositionChangedEvent>>().Subscribe(CheckSkip);
        }

        private void CheckSkip(CharacterPositionChangedEvent @event)
        {
            var skippedTrafficLight = _sceneAccessor
                .FindAll<TrafficLight>(t => t.WasSkipped(@event.CharacterId))
                .FirstOrDefault();

            if (skippedTrafficLight == null)
                return;

            var characterPosition = new CoordianteUI(@event.NewPosition.X, @event.NewPosition.Y);
            if (!skippedTrafficLight.GetTrackingCells().Contains(@event.NewPosition) && !characterPosition.Equals(skippedTrafficLight.MapPosition))
                skippedTrafficLight.ClearSkip(@event.CharacterId);
        }

        public void Process(double gameTime)
        {
        }

        private void OnCharacterMove(CharacterPositionChangedEvent @event)
        {
            var traficLighs = _sceneAccessor
                .FindAll<TrafficLight>(t => t.GetTrackingCells()
                .Contains(@event.NewPosition))
                .ToArray();

            if (!traficLighs.Any())
                return;

            var map = _sceneAccessor.GetScene<Map>(SceneNames.Map);

            foreach(var traficLight in traficLighs)
            {
                if (traficLight.WasSkipped(@event.CharacterId))
                    continue;

                var character = _sceneAccessor.FindFirst<character>(SceneNames.Character(@event.CharacterId));
                character.StopMoving();

                lock (_lock)
                {
                    var characterDirection = traficLight.GetDirectionFor(@event.NewPosition);
                    var availableDirections = traficLight
                        .GetActiveDirections()
                        .Where(d => d != characterDirection)
                        .ToDictionary(d => d, d => traficLight.GetValue(d));

                    if (availableDirections.All(d => d.Value == 0))
                        foreach (var direction in availableDirections)
                            traficLight.Reset(direction.Key);

                    var targetDirection = availableDirections
                        .Select(d => new { Direction = d.Key, Value = traficLight.GetValue(d.Key) })
                        .Where(d => d.Value > 0)
                        .OrderBy(d => d.Value)
                        .First()
                        .Direction;
                    var targetCell = traficLight.GetTrackingCell(targetDirection);

                    var wantMoveTo = map.GetDirectedNeighboursOf(targetCell)
                        .Where(d => d.Value == targetDirection)
                        .Select(d => d.Key)
                        .FirstOrDefault();

                    if (wantMoveTo == default)
                        continue;

                    _eventAggregator.GetEvent<GameEvent<MoveToRequestEvent>>().Publish(new MoveToRequestEvent { CharacterId = character.Id, TargetCell = wantMoveTo });
                    traficLight.DecreaseValue(targetDirection);
                    traficLight.SkipCharacter(character.Id);
                }
            };
        }
    }
}