using Game.Server.Events.Core;
using Game.Server.Events.List.Resource;
using Game.Server.Models.Constants;
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

            AddifNotExists(ResourceType.Money, "K$", 0);
            AddifNotExists(ResourceType.Water, "Water", 0);
            AddifNotExists(ResourceType.Food, "Food", 0);
            AddifNotExists(ResourceType.Electricity, "Electricity", 0);
            AddifNotExists(ResourceType.Steel, "Steel", 0);
            AddifNotExists(ResourceType.Uranus, "Uranus", 0);
            AddifNotExists(ResourceType.Aluminum, "Aluminum", 0);
            AddifNotExists(ResourceType.Microchip, "Microchip", 100);
        }

        public int GetAmount(int resourceType) => _storage.Find<Resource>(r => r.ResourceType == resourceType).First().Value;

        public bool TrySpend(int resourceType, int count)
        {
            var resource = _storage.Find<Resource>(r => r.ResourceType == resourceType).FirstOrDefault();
            if (resource == null)
                throw new ArgumentOutOfRangeException($"unknown resource with id {resourceType}");

            if (resource.Value < count)
                return false;

            resource.Value = resource.Value - count;
            _storage.Update(resource);
            _eventAggregator.GetEvent<GameEvent<ResourceDecreaseEvent>>().Publish(new ResourceDecreaseEvent { ResourceTypeId = resourceType, Amount = count });
            return true;
        }

        public void Increase(int resourceType, int count)
        {
            var resource = _storage.Find<Resource>(r => r.ResourceType == resourceType).FirstOrDefault();
            if (resource == null)
                throw new ArgumentOutOfRangeException($"unknown resource with id {resourceType}");

            resource.Value += count;
            _storage.Update(resource);
            _eventAggregator.GetEvent<GameEvent<ResourceIncreaseEvent>>().Publish(new ResourceIncreaseEvent { ResourceTypeId = resourceType, Amount = count });
        }

        private void AddifNotExists(int resourceType, string name, int initialValue)
        {
            var exists = _storage.Exists<Resource>(r => r.ResourceType == resourceType);
            if (exists)
                return;
            
            _storage.Add(new Resource { ResourceType = resourceType, Value = initialValue, Name = name });
            _eventAggregator.GetEvent<GameEvent<ResourceAddedEvent>>().Publish(new ResourceAddedEvent { ResourceType = resourceType, Value = initialValue });
        }

        public IReadOnlyCollection<Resource> GetList()
        {
            return _storage.Find<Resource>(r => true).ToArray();
        }
    }
}