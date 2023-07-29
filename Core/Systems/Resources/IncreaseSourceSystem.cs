using Game.Server.Events.Core;
using Game.Server.Events.List.Resource;
using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Ui;
using System;

namespace My_awesome_character.Core.Systems.Resources
{
    internal class IncreaseSourceSystem : ISystem
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ISceneAccessor _sceneAccessor;

        public IncreaseSourceSystem(IEventAggregator eventAggregator, ISceneAccessor sceneAccessor)
        {
            _eventAggregator = eventAggregator;
            _sceneAccessor = sceneAccessor;
        }

        public void OnStart()
        {
            _eventAggregator.GetEvent<GameEvent<ResourceChangedEvent>>().Subscribe(OnIncrease);
        }

        public void Process(double gameTime)
        {
        }

        private void OnIncrease(ResourceChangedEvent @event)
        {
            var resource = _sceneAccessor.FindFirst<Resource>(SceneNames.ResourceInfo(@event.ResourceTypeId));
            if (resource == null)
                throw new ArgumentException($"resource {@event.ResourceTypeId} was not found");

            resource.Amount = (int)@event.Amount;
        }
    }
}