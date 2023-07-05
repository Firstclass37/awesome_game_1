using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;
using Game.Server.Storage;

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
            var positionInfo = _storage.Find<GameObjectPosition>(p => p.Coordiante.Equals(position)).OrderByDescending(p => p.CreatedDate).First();
            return Get(positionInfo.EntityId);
        }

        public GameObjectAggregator Get(Guid id)
        {
            var gameObject = _storage.Get<GameObject>(id);

            var agregator = new GameObjectAggregator();
            agregator.GameObject = gameObject;
            agregator.Attributes = _storage.Find<GameObjectToAttribute>(a => a.GameObjectId == gameObject.Id).ToList();
            agregator.Area = _storage.Find<GameObjectPosition>(p => p.EntityId == gameObject.Id).ToList();

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