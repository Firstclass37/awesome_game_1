using Game.Server.DataAccess;
using Game.Server.Events.Core;
using Game.Server.Events.Core.Extentions;
using Game.Server.Events.List.Movement;
using Game.Server.Logic.Maps;
using Game.Server.Models.Constants.Attributes;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;
using Game.Server.Storage;

namespace Game.Server.Logic.Systems
{
    internal class MovementSystem : ISystem
    {
        private readonly IStorage _storage;
        private readonly IEventAggregator _eventAggregator;
        private readonly IGameObjectAgregatorRepository _agregatorRepository;
        private readonly IStorageCacheDecorator _gameObjectPositionCacheDecorator;
        private readonly IGameObjectAccessor _gameObjectAccessor;

        public MovementSystem(IStorage storage, IEventAggregator eventAggregator, IStorageCacheDecorator gameObjectPositionCacheDecorator, IGameObjectAccessor gameObjectAccessor, IGameObjectAgregatorRepository agregatorRepository)
        {
            _storage = storage;
            _eventAggregator = eventAggregator;
            _gameObjectPositionCacheDecorator = gameObjectPositionCacheDecorator;
            _gameObjectAccessor = gameObjectAccessor;
            _agregatorRepository = agregatorRepository;
        }

        public void Process(double gameTime)
        {
            var moveableObjects = _gameObjectPositionCacheDecorator
                .GetObjectsWithAttributes(MovementAttributesTypes.Speed)
                .Select(id => _gameObjectAccessor.Get(id))
                .Where(o => o != null)
                .Where(o => o.AttributeExists(MovementAttributesTypes.MovementPath))
                .ToArray();

            foreach(var moveableObject in moveableObjects)
            {
                if (moveableObject.Area.Count() > 1)
                    throw new Exception($"movement system dosn't support huge object movement yet");

                var currentSinglePosition = moveableObject.Area.First();
                var lastMovementTime = moveableObject.AttributeExists(MovementAttributesTypes.LastMovementTime) 
                    ? moveableObject.GetAttributeValue(MovementAttributes.LastMovementTime)
                    : (double?)null;
                var speed = moveableObject.GetAttributeValue(MovementAttributes.Speed);
                var movingTo = moveableObject.GetAttributeValue(MovementAttributes.MovingTo);
                var path = moveableObject.GetAttributeValue(MovementAttributes.Movementpath);

                if (movingTo == null && path == null)
                    continue;

                if (speed == 0.0)
                    continue;

                if (movingTo == null)
                {
                    var nextPosition = FindNext(path, currentSinglePosition.Coordiante);
                    if (nextPosition == null) 
                    {
                        moveableObject.SetAttributeValue(MovementAttributes.Movementpath, null);
                        moveableObject.SetAttributeValue(MovementAttributes.Iniciator, null);
                    }
                    else
                        moveableObject.SetAttributeValue(MovementAttributes.MovingTo, nextPosition);

                    moveableObject.SetAttributeValue(MovementAttributes.LastMovementTime, gameTime);
                    _agregatorRepository.Update(moveableObject);

                    if (nextPosition != null)
                        _eventAggregator.PublishGameEvent(new GameObjectPositionChangingEvent
                        {
                            GameObjectId = moveableObject.GameObject.Id,
                            GameObjectType = moveableObject.GameObject.ObjectType,
                            CurrentPosition = currentSinglePosition.Coordiante,
                            TargetPosition = nextPosition,
                            Speed = speed,
                        });
                }
                else if (gameTime - lastMovementTime > speed)
                {
                    moveableObject.Area = new List<GameObjectPosition> { currentSinglePosition with { Coordiante = movingTo } };
                    moveableObject.SetAttributeValue(MovementAttributes.MovingTo, null);
                    moveableObject.SetAttributeValue(MovementAttributes.LastMovementTime, gameTime);
                    _agregatorRepository.Update(moveableObject);

                    _eventAggregator.PublishGameEvent(new GameObjectPositionChangedEvent
                    {
                        GameObjectId = moveableObject.GameObject.Id,
                        GameObjectType = moveableObject.GameObject.ObjectType,
                        PreviousPostion = currentSinglePosition.Coordiante,
                        NewPosition = movingTo
                    });
                }
            }
        }

        private Coordiante FindNext(Coordiante[] path, Coordiante currentPosition)
        {
            if (path == null)
                return default;

            var nextIndex = Array.IndexOf(path, currentPosition) + 1;
            if (nextIndex > path.Length - 1)
                return default;

            return path[nextIndex];
        }
    }
}