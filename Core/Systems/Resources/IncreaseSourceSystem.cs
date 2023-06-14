using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Game.Events.Resource;
using My_awesome_character.Core.Game.Resources;
using My_awesome_character.Core.Infrastructure.Events;
using My_awesome_character.Core.Ui;
using System;

namespace My_awesome_character.Core.Systems.Resources
{
    internal class IncreaseSourceSystem : ISystem
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ISceneAccessor _sceneAccessor;
        private readonly IResourceManager _resourceManager;

        public IncreaseSourceSystem(IEventAggregator eventAggregator, ISceneAccessor sceneAccessor, IResourceManager resourceManager)
        {
            _eventAggregator = eventAggregator;
            _sceneAccessor = sceneAccessor;
            _resourceManager = resourceManager;
        }

        public void OnStart()
        {
            _eventAggregator.GetEvent<GameEvent<ResourceIncreaseEvent>>().Subscribe(OnIncrease);
            _eventAggregator.GetEvent<GameEvent<ResourceDecreaseEvent>>().Subscribe(OnDecrease);
        }

        public void Process(double gameTime)
        {
        }

        private void OnIncrease(ResourceIncreaseEvent @event)
        {
            var resource = _sceneAccessor.FindFirst<Resource>(SceneNames.ResourceInfo(@event.ResourceTypeId));
            if (resource == null)
                throw new ArgumentException($"resource {@event.ResourceTypeId} was not found");

            resource.Amount = _resourceManager.GetAmount(@event.ResourceTypeId);
        }

        private void OnDecrease(ResourceDecreaseEvent @event)
        {
            var resource = _sceneAccessor.FindFirst<Resource>(SceneNames.ResourceInfo(@event.ResourceTypeId));
            if (resource == null)
                throw new ArgumentException($"resource {@event.ResourceTypeId} was not found");

            resource.Amount = _resourceManager.GetAmount(@event.ResourceTypeId);
        }
    }
}