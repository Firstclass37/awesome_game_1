using Game.Server.Models;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;
using Game.Server.Storage;
using System.Collections.Concurrent;

namespace Game.Server.DataAccess
{
    internal interface IStorageCacheDecorator: IStorage
    {
        IEnumerable<Guid> GetObjectsOn(Coordiante coordiante);
        IEnumerable<GameObjectPosition> GetPositionsFor(Guid objectId);

        IEnumerable<GameObjectToAttribute> GetAttributesFor(Guid entityId);

        IEnumerable<Guid> GetObjectsWithAttributes(string attributeType);
    }

    internal class StorageCacheDecorator : IStorageCacheDecorator
    {
        private readonly IStorage _storage;

        private readonly ConcurrentDictionary<Guid, ConcurrentDictionary<Guid, GameObjectPosition>> _objectToPositions = new();
        private readonly ConcurrentDictionary<Coordiante, ConcurrentDictionary<Guid, bool>> _positionToObjects = new();
        private readonly ConcurrentDictionary<Guid, ConcurrentDictionary<Guid, GameObjectToAttribute>> _attributes = new();
        private readonly ConcurrentDictionary<string, ConcurrentDictionary<Guid, bool>> _attributesToObjects = new();

        public StorageCacheDecorator(IStorage storage)
        {
            _storage = storage;
        }

        public IEnumerable<GameObjectToAttribute> GetAttributesFor(Guid entityId)
        {
            if (_attributes.TryGetValue(entityId, out var attributes))
                return attributes.Values;

            return Enumerable.Empty<GameObjectToAttribute>();
        }

        public IEnumerable<Guid> GetObjectsWithAttributes(string attributeType) 
        {
            if (_attributesToObjects.TryGetValue(attributeType, out var objects))
                return objects.Keys;

            return Enumerable.Empty<Guid>();
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

            if (obj is GameObjectToAttribute attribute)
            {
                GetAttributes(attribute.GameObjectId).TryAdd(attribute.Id, attribute);
                GetAttributesToObjects(attribute.AttributeType).TryAdd(attribute.GameObjectId, true);
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

            if (obj is GameObjectToAttribute attribute)
            {
                GetAttributes(attribute.GameObjectId).TryRemove(attribute.Id, out _);
                GetAttributesToObjects(attribute.AttributeType).TryRemove(attribute.GameObjectId, out _);
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

            if (obj is GameObjectToAttribute attribute)
                GetAttributes(attribute.GameObjectId)[attribute.Id] = attribute;
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

        private ConcurrentDictionary<Guid, GameObjectToAttribute> GetAttributes(Guid objectId)
        {
            if (_attributes.ContainsKey(objectId))
                return _attributes[objectId];

            _attributes.TryAdd(objectId, new ConcurrentDictionary<Guid, GameObjectToAttribute>());
            return _attributes[objectId];
        }

        private ConcurrentDictionary<Guid, bool> GetAttributesToObjects(string attributeType)
        {
            if (_attributesToObjects.ContainsKey(attributeType))
                return _attributesToObjects[attributeType];

            _attributesToObjects.TryAdd(attributeType, new ConcurrentDictionary<Guid, bool>());
            return _attributesToObjects[attributeType];
        }

        public bool Exists<T>(Guid id) where T: IEntityObject
        {
            return _storage.Exists<T>(id);
        }
    }
}