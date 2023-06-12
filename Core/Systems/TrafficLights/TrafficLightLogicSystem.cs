using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Game.Events.Character;
using My_awesome_character.Core.Infrastructure.Events;
using My_awesome_character.Core.Ui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_awesome_character.Core.Systems.TrafficLights
{
    internal class TrafficLightLogicSystem : ISystem
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ISceneAccessor _sceneAccessor;

        public TrafficLightLogicSystem(IEventAggregator eventAggregator, ISceneAccessor sceneAccessor)
        {
            _eventAggregator = eventAggregator;
            _sceneAccessor = sceneAccessor;
        }

        public void OnStart()
        {
            _eventAggregator.GetEvent<GameEvent<CharacterPositionChangedEvent>>().Subscribe(OnCharacterMove);
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
                var character = _sceneAccessor.FindFirst<character>(SceneNames.Character(@event.CharacterId));
                character.StopMoving();


                var characterDirection = traficLight.GetDirectionFor(@event.NewPosition);
                var directionsValus = traficLight
                    .GetActiveDirections()
                    .Where(d => d != characterDirection)
                    .ToDictionary(d => d, d => traficLight.GetValue(d));

                if (directionsValus.Any(d => d.Value == 0))
                    foreach (var direction in directionsValus)
                        traficLight.Reset(direction.Key);

                var targetDirection = directionsValus.Where(d => d.Value > 0).OrderBy(d => d.Value).First().Key;
                var targetCell = traficLight.GetTrackingCell(targetDirection);

                var wantMoveTo = map.GetDirectedNeighboursOf(targetCell)
                    .Where(d => d.Value == targetDirection)
                    .Select(d => d.Key)
                    .FirstOrDefault();

                if (wantMoveTo == default)
                    continue;

                _eventAggregator.GetEvent<GameEvent<MoveToRequestEvent>>().Publish(new MoveToRequestEvent { CharacterId = character.Id, TargetCell = wantMoveTo });
                traficLight.DecreaseValue(targetDirection);
            };
        }
    }
}