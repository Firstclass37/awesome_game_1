using Game.Server.Models.GameObjects;
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

        public IReadOnlyCollection<GameObjectAggregator> FindAll(string gameObjectType)
        {
            var collection = new List<GameObjectAggregator>();
            var gameObjects = _storage.Find<GameObject>(o => o.ObjectType == gameObjectType);
            foreach ( var gameObject in gameObjects) 
            {
                var agregator = new GameObjectAggregator();
                agregator.GameObject = gameObject;
                agregator.Attributes = _storage.Find<GameObjectToAttribute>(a => a.GameObjectId == gameObject.Id).ToArray();
                agregator.Area = _storage.Find<GameObjectPosition>(p => p.EntityId == gameObject.Id).ToArray();
            }
            return collection;
        }
    }
}