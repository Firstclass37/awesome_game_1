using Game.Server.Events.Core;
using Game.Server.Events.Core.Extentions;
using Game.Server.Events.List;
using Game.Server.Events.List.Homes;
using Game.Server.Models.GameObjects;
using Game.Server.Storage;
using Game.Server.Storage.Extentions;

namespace Game.Server.DataAccess
{
    internal class GameObjectAgregatorRepository : IGameObjectAgregatorRepository
    {
        private readonly IStorage _storage;
        private readonly IEventAggregator _eventAggregator;

        public GameObjectAgregatorRepository(IStorage storage, IEventAggregator eventAggregator)
        {
            _storage = storage;
            _eventAggregator = eventAggregator;
        }

        public void Add(GameObjectAggregator gameObjectAggregator)
        {
            _storage.Add(gameObjectAggregator.GameObject);
            _storage.AddRange(gameObjectAggregator.Attributes);
            _storage.AddRange(gameObjectAggregator.Area);
            _storage.AddRange(gameObjectAggregator.PeriodicActions);
            _storage.AddRange(gameObjectAggregator.Interactions);

            _eventAggregator.PublishGameEvent(new ObjectCreatedEvent
            {
                Id = gameObjectAggregator.GameObject.Id,
                Area = gameObjectAggregator.Area.Select(a => a.Coordiante).ToArray(),
                ObjectType = gameObjectAggregator.GameObject.ObjectType,
                Root = gameObjectAggregator.RootCell
            });
        }

        public void Remove(GameObjectAggregator gameObjectAggregator)
        {
            _storage.Remove(gameObjectAggregator.GameObject);
            _storage.RemoveRange(gameObjectAggregator.Attributes);
            _storage.RemoveRange(gameObjectAggregator.Area);
            _storage.RemoveRange(gameObjectAggregator.PeriodicActions);
            _storage.RemoveRange(gameObjectAggregator.Interactions);

            _eventAggregator.PublishGameEvent(new GameObjectDestroiedEvent 
            { 
                ObjectId = gameObjectAggregator.GameObject.Id, 
                ObjectType = gameObjectAggregator.GameObject.ObjectType 
            });
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