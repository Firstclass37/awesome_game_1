using Game.Server.Models;
using Game.Server.Models.GameObjects;
using Game.Server.Storage;
using System.Collections.Concurrent;

namespace Game.Server.DataAccess
{
    internal interface IGameObjectPositionCacheDecorator: IStorage
    {
        IEnumerable<GameObjectPosition> GetPositionsFor(Guid objectId);
    }

    internal class GameObjectPositionCacheDecorator : IGameObjectPositionCacheDecorator
    {
        private readonly IStorage _storage;

        private readonly ConcurrentDictionary<Guid, ConcurrentDictionary<Guid, GameObjectPosition>> _objectToPositions = new();

        public GameObjectPositionCacheDecorator(IStorage storage)
        {
            _storage = storage;
        }

        public IEnumerable<GameObjectPosition> GetPositionsFor(Guid objectId)
        {
            return _objectToPositions.ContainsKey(objectId) ? _objectToPositions[objectId].Values : Array.Empty<GameObjectPosition>();
        }

        public void Add<T>(T obj) where T : IEntityObject
        {
            _storage.Add(obj);

            if (obj is GameObjectPosition position)
            {
                var positions = GetPositions(position.EntityId);
                positions.TryAdd(position.Id, position);
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
                var positions = GetPositions(position.EntityId);
                positions.TryRemove(position.Id, out _);
            }
        }

        public void RemoveRange<T>(IEnumerable<T> entities) where T : IEntityObject
        {
            foreach(var entity in entities)
                Remove(entity);
        }

        public void Update<T>(T obj) where T : IEntityObject
        {
            _storage.Update<T>(obj);

            if (obj is GameObjectPosition position)
            {
                var positions = GetPositions(position.EntityId);
                positions[position.Id] = position;
            }
        }


        private ConcurrentDictionary<Guid, GameObjectPosition> GetPositions(Guid objectId)
        {
            if (_objectToPositions.ContainsKey(objectId))
                return _objectToPositions[objectId];

            _objectToPositions.TryAdd(objectId, new ConcurrentDictionary<Guid, GameObjectPosition>());
            return _objectToPositions[objectId];
        }
    }
}