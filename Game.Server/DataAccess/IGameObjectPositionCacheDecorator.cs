using Game.Server.Models;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;
using Game.Server.Storage;
using System.Collections.Concurrent;

namespace Game.Server.DataAccess
{
    internal interface IGameObjectPositionCacheDecorator: IStorage
    {
        IEnumerable<Guid> GetObjectsOn(Coordiante coordiante);
        IEnumerable<GameObjectPosition> GetPositionsFor(Guid objectId);
    }

    internal class GameObjectPositionCacheDecorator : IGameObjectPositionCacheDecorator
    {
        private readonly IStorage _storage;

        private readonly ConcurrentDictionary<Guid, ConcurrentDictionary<Guid, GameObjectPosition>> _objectToPositions = new();
        private readonly ConcurrentDictionary<Coordiante, ConcurrentDictionary<Guid, bool>> _positionToObjects = new();

        public GameObjectPositionCacheDecorator(IStorage storage)
        {
            _storage = storage;
        }

        public IEnumerable<GameObjectPosition> GetPositionsFor(Guid objectId)
        {
            return _objectToPositions.ContainsKey(objectId) ? _objectToPositions[objectId].Values : Array.Empty<GameObjectPosition>();
        }

        public IEnumerable<Guid> GetObjectsOn(Coordiante coordiante)
        {
            return _positionToObjects.ContainsKey(coordiante) ? _positionToObjects[coordiante].Keys : Array.Empty<Guid>();
        }


        public void Add<T>(T obj) where T : IEntityObject
        {
            _storage.Add(obj);

            if (obj is GameObjectPosition position)
            {
                GetPositions(position.EntityId).TryAdd(position.Id, position);
                GetPositionsToObject(position.Coordiante).TryAdd(position.EntityId, true);
            }
        }

        public void AddRange<T>(IEnumerable<T> obj) where T : IEntityObject
        {
            foreach(var item in obj)
                Add(item);
        }

        public bool Exists<T>(Func<T, bool> predicate) where T : IEntityObject
        {
            return _storage.Exists<T>(predicate);
        }

        public IEnumerable<T> Find<T>(Func<T, bool> predicate) where T : IEntityObject
        {
            return _storage.Find<T>(predicate);
        }

        public T Get<T>(Guid id) where T : IEntityObject
        {
            return _storage.Get<T>(id);
        }

        public void Remove<T>(T obj) where T : IEntityObject
        {
            _storage.Remove<T>(obj);

            if (obj is GameObjectPosition position)
            {
                GetPositions(position.EntityId).TryRemove(position.Id, out _);
                GetPositionsToObject(position.Coordiante).TryRemove(position.EntityId, out _);
            }
        }

        public void RemoveRange<T>(IEnumerable<T> entities) where T : IEntityObject
        {
            foreach(var entity in entities)
                Remove(entity);
        }

        public void Update<T>(T obj) where T : IEntityObject
        {
            var prevValue = _storage.Get<T>(obj.Id);

            _storage.Update<T>(obj);

            if (obj is GameObjectPosition position)
            {
                var positions = GetPositions(position.EntityId);
                positions[position.Id] = position;

                var prevPosition = prevValue as GameObjectPosition;
                GetPositionsToObject(prevPosition.Coordiante).TryRemove(prevPosition.EntityId, out _);
                GetPositionsToObject(position.Coordiante).TryAdd(position.EntityId, true);
            }
        }


        private ConcurrentDictionary<Guid, GameObjectPosition> GetPositions(Guid objectId)
        {
            if (_objectToPositions.ContainsKey(objectId))
                return _objectToPositions[objectId];

            _objectToPositions.TryAdd(objectId, new ConcurrentDictionary<Guid, GameObjectPosition>());
            return _objectToPositions[objectId];
        }

        private ConcurrentDictionary<Guid, bool> GetPositionsToObject(Coordiante coordinate)
        {
            if (_positionToObjects.ContainsKey(coordinate))
                return _positionToObjects[coordinate];

            _positionToObjects.TryAdd(coordinate, new ConcurrentDictionary<Guid, bool>());
            return _positionToObjects[coordinate];
        }
    }
}