using Game.Server.Models.GameObjects;
using Game.Server.Storage;
using Game.Server.Storage.Extentions;

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
            _storage.Remove(gameObjectAggregator.GameObject);
            _storage.RemoveRange(gameObjectAggregator.Attributes);
            _storage.RemoveRange(gameObjectAggregator.Area);
            _storage.RemoveRange(gameObjectAggregator.PeriodicActions);
            _storage.RemoveRange(gameObjectAggregator.Interactions);
        }

        public void Update(GameObjectAggregator gameObjectAggregator)
        {
            _storage.Update(gameObjectAggregator.GameObject);

            foreach(var attribute in gameObjectAggregator.Attributes)
                _storage.Upsert(attribute);

            foreach(var area in gameObjectAggregator.Area)
                _storage.Upsert(area);

            foreach(var periodicAction in gameObjectAggregator.PeriodicActions)
                _storage.Upsert(periodicAction);

            foreach(var interaction in gameObjectAggregator.Interactions)
                _storage.Upsert(interaction);
        }
    }
}