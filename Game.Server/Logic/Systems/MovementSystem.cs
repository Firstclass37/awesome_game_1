using Game.Server.DataAccess;
using Game.Server.Events.Core;
using Game.Server.Events.List.Character;
using Game.Server.Models;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;
using Game.Server.Storage;

namespace Game.Server.Logic.Systems
{
    internal class MovementSystem : ISystem
    {
        private readonly IStorage _storage;
        private readonly IEventAggregator _eventAggregator;
        private readonly IGameObjectPositionCacheDecorator _gameObjectPositionCacheDecorator;
        private readonly float _speed = 1.5f;

        public MovementSystem(IStorage storage, IEventAggregator eventAggregator, IGameObjectPositionCacheDecorator gameObjectPositionCacheDecorator)
        {
            _storage = storage;
            _eventAggregator = eventAggregator;
            _gameObjectPositionCacheDecorator = gameObjectPositionCacheDecorator;
        }

        public void Process(double gameTime)
        {
            var movements = _storage.Find<Movement>(m => m.Active);
            foreach (var movement in movements) 
            {
                var currentPosition = _gameObjectPositionCacheDecorator.GetPositionsFor(movement.GameObjectId).ToArray();
                if (currentPosition.Length > 1)
                    throw new Exception($"movement system dosn't support huge object movement yet");


                var currentSinglePosition = currentPosition.First();
                var nextPosition = FindNext(movement, currentSinglePosition.Coordiante);
                if (nextPosition == null)
                {
                    _storage.Remove(movement);
                    continue;
                }

                if (movement.CurrentMoveToPosition != nextPosition)
                {
                    _eventAggregator.GetEvent<GameEvent<CharacterMoveEvent>>().Publish(new CharacterMoveEvent
                    {
                        CharacterId = movement.GameObjectId,
                        NewPosition = nextPosition,
                        Speed = _speed
                    });
                    _storage.Update(movement with { CurrentMoveToPosition = nextPosition, LastMovementTime = gameTime }); // Костыль для LastMovementTime == 0
                }

                var updatedMovement = _storage.Get<Movement>(movement.Id);
                if ((gameTime - updatedMovement.LastMovementTime) > _speed)
                {
                    var active = movement.Path.Last() != nextPosition;
                    _storage.Update(currentSinglePosition with { Coordiante = nextPosition });
                    _storage.Update(updatedMovement with { LastMovementTime = gameTime, Active = movement.Path.Last() != nextPosition });
                    if (active == false)
                        _storage.Remove(updatedMovement);

                    _eventAggregator.GetEvent<GameEvent<CharacterPositionChanged>>()
                        .Publish(new CharacterPositionChanged { CharacterId = movement.GameObjectId, Position = nextPosition });
                }
            }
        }

        private Coordiante FindNext(Movement movement, Coordiante currentPosition)
        {
            var nextIndex = Array.IndexOf(movement.Path, currentPosition) + 1;
            if (nextIndex > movement.Path.Length - 1)
                return default;

            return movement.Path[nextIndex];
        }
    }
}