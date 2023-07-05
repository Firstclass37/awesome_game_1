using Game.Server.Models.GameObjects;
using Game.Server.Storage;

namespace Game.Server.DataAccess
{
    internal class GameObjectAgregatorRepository : IGameObjectAgregatorRepository
    {
        private readonly IStorage _storage;

        public GameObjectAgregatorRepository(IStorage storage)
        {
            _storage = storage;
        }

        public void Add(GameObjectAggregator gameObjectAggregator)
        {
            _storage.Add(gameObjectAggregator.GameObject);
            _storage.AddRange(gameObjectAggregator.Attributes);
            _storage.AddRange(gameObjectAggregator.Area);
            _storage.AddRange(gameObjectAggregator.PeriodicActions);
            _storage.AddRange(gameObjectAggregator.Interactions);
        }

        public void Remove(GameObjectAggregator gameObjectAggregator)
        {
            throw new NotImplementedException();
        }

        public void Update(GameObjectAggregator gameObjectAggregator)
        {
            throw new NotImplementedException();
        }
    }
}