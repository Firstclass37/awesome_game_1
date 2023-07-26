using Game.Server.Models;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;
using Game.Server.Storage;
using System.Collections.Immutable;

namespace Game.Server.Logic.Maps
{
    internal class GameObjectAccessor : IGameObjectAccessor
    {
        private readonly IStorage _storage;

        public GameObjectAccessor(IStorage storage)
        {
            _storage = storage;
        }

        public GameObjectAggregator Find(Coordiante position)
        {
            var positionInfo = _storage.Find<GameObjectPosition>(p => p.Coordiante == position).OrderByDescending(p => p.CreatedDate).FirstOrDefault();
            return positionInfo != null ? Get(positionInfo.EntityId) : null;
        }

        public IEnumerable<GameObjectAggregator> FindAll(Coordiante position)
        {
            return _storage.Find<GameObjectPosition>(p => p.Coordiante == position)
                .OrderByDescending(p => p.CreatedDate)
                .Select(o => Get(o.EntityId));
        }

        public GameObjectAggregator Get(Guid id)
        {
            var gameObject = _storage.Get<GameObject>(id);

            var agregator = new GameObjectAggregator();
            agregator.GameObject = gameObject;
            agregator.Attributes = _storage.Find<GameObjectToAttribute>(a => a.GameObjectId == gameObject.Id).ToList();
            agregator.Area = _storage.Find<GameObjectPosition>(p => p.EntityId == gameObject.Id).ToList();
            agregator.Interactions = _storage.Find<GameObjectInteraction>(i => i.GameObjectId == id).ToList();

            return agregator;
        }

        public IReadOnlyCollection<GameObjectAggregator> FindAll(string gameObjectType)
        {
            var collection = new List<GameObjectAggregator>();
            var gameObjects = _storage.Find<GameObject>(o => o.ObjectType == gameObjectType);
            foreach ( var gameObject in gameObjects) 
                collection.Add(Get(gameObject.Id));
            return collection;
        }
    }
}