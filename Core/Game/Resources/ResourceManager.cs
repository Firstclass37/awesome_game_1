using My_awesome_character.Core.Game.Constants;
using My_awesome_character.Core.Game.Events.Resource;
using My_awesome_character.Core.Infrastructure.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace My_awesome_character.Core.Game.Resources
{
    internal class ResourceManager : IResourceManager
    {
        private readonly Dictionary<int, int> _resourses = new Dictionary<int, int>();
        private readonly IEventAggregator _eventAggregator;

        public ResourceManager(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            _resourses.Add(ResourceType.Money, 0);
            _resourses.Add(ResourceType.Water, 0);
            _resourses.Add(ResourceType.Food, 0);
            _resourses.Add(ResourceType.Electricity, 0);
            _resourses.Add(ResourceType.Steel, 0);
            _resourses.Add(ResourceType.Uranus, 0);
            _resourses.Add(ResourceType.Microchip, 100);
        }

        public int[] GetList() => _resourses.Keys.ToArray();

        public int GetAmount(int resourceId) => _resourses.ContainsKey(resourceId) ? _resourses[resourceId] : throw new ArgumentOutOfRangeException($"unknown resource with id {resourceId}");

        public bool TrySpend(int resourceId, int count)
        {
            if (!_resourses.ContainsKey(resourceId))
                throw new ArgumentOutOfRangeException($"unknown resource with id {resourceId}");

            var currentValue = _resourses[resourceId];
            if (currentValue < count)
                return false;

            _resourses[resourceId] = currentValue - count;
            _eventAggregator.GetEvent<GameEvent<ResourceDecreaseEvent>>().Publish(new ResourceDecreaseEvent { ResourceTypeId = resourceId, Amount = count });
            return true;
        }

        public void Increse(int resourceId, int count)
        {
            if (!_resourses.ContainsKey(resourceId))
                throw new ArgumentOutOfRangeException($"unknown resource with id {resourceId}");

            _resourses[resourceId] += count;
            _eventAggregator.GetEvent<GameEvent<ResourceIncreaseEvent>>().Publish(new ResourceIncreaseEvent { ResourceTypeId = resourceId, Amount = count });
        }
    }
}