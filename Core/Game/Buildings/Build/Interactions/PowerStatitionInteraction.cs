using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Game.Constants;
using My_awesome_character.Core.Game.Events.Character;
using My_awesome_character.Core.Game.Events.Resource;
using My_awesome_character.Core.Infrastructure.Events;
using My_awesome_character.Core.Ui;

namespace My_awesome_character.Core.Game.Buildings.Build.Interactions
{
    internal class PowerStatitionInteraction : IInteractionAction
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ISceneAccessor _sceneAccessor;

        public PowerStatitionInteraction(IEventAggregator eventAggregator, ISceneAccessor sceneAccessor)
        {
            _eventAggregator = eventAggregator;
            _sceneAccessor = sceneAccessor;
        }

        public void Interacte(character character)
        {
            _eventAggregator.GetEvent<GameEvent<TakeDamageCharacterEvent>>().Publish(new TakeDamageCharacterEvent { CharacterId = character.Id, Damage = 1000 });

            var uranusResource = _sceneAccessor.FindFirst<Resource>(SceneNames.ResourceInfo(ResourceType.Uranus));
            if (uranusResource.Amount > 1)
            {
                _eventAggregator.GetEvent<GameEvent<ResourceIncreaseEvent>>().Publish(new ResourceIncreaseEvent { Amount = 3, ResourceTypeId = ResourceType.Electricity });
                _eventAggregator.GetEvent<GameEvent<ResourceDecreaseEvent>>().Publish(new ResourceDecreaseEvent { Amount = 1, ResourceTypeId = ResourceType.Uranus });
            }
        }
    }
}