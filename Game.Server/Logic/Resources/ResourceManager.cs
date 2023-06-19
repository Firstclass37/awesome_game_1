using Game.Server.Events.Core;
using Game.Server.Events.List.Resource;
using Game.Server.Models.Resources;
using Game.Server.Storage;

namespace Game.Server.Logic.Resources
{
    internal class ResourceManager : IResourceManager
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IStorage _storage;

        public ResourceManager(IEventAggregator eventAggregator, IStorage storage)
        {
            _eventAggregator = eventAggregator;
            _storage = storage;
        }

        public int GetAmount(int resourceId) => _storage.Get<Resource>(resourceId).Value;

        public Resource Get(int resourceId) => _storage.Get<Resource>(resourceId);

        public bool TrySpend(int resourceId, int count)
        {
            var resource = _storage.Get<Resource>(resourceId);
            if (resource == null)
                throw new ArgumentOutOfRangeException($"unknown resource with id {resourceId}");

            if (resource.Value < count)
                return false;

            resource.Value = resource.Value - count;
            _storage.Update(resource);
            _eventAggregator.GetEvent<GameEvent<ResourceDecreaseEvent>>().Publish(new ResourceDecreaseEvent { ResourceTypeId = resourceId, Amount = count });
            return true;
        }

        public void Increase(int resourceId, int count)
        {
            var resource = _storage.Get<Resource>(resourceId);
            if (resource == null)
                throw new ArgumentOutOfRangeException($"unknown resource with id {resourceId}");

            resource.Value += count;
            _storage.Update(resource);
            _eventAggregator.GetEvent<GameEvent<ResourceIncreaseEvent>>().Publish(new ResourceIncreaseEvent { ResourceTypeId = resourceId, Amount = count });
        }

        public void AddResource(int resourceId, int initialValue)
        {
            var exists = _storage.Exists<Resource>(r => r.Id == resourceId);
            if (exists)
                return;

            var resources = _storage.Find<Resource>(r => true).ToArray();
            var nextId = resources.Any() ? resources.Max(r => r.Id) + 1 : 1;
            _storage.Add(new Resource { Id = resourceId, Value = initialValue });
            _eventAggregator.GetEvent<GameEvent<ResourceAddedEvent>>().Publish(new ResourceAddedEvent { ResourceType = resourceId, Value = initialValue });
        }
    }
}