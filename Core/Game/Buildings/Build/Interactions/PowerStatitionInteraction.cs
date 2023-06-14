using My_awesome_character.Core.Game.Constants;
using My_awesome_character.Core.Game.Events.Character;
using My_awesome_character.Core.Game.Resources;
using My_awesome_character.Core.Infrastructure.Events;

namespace My_awesome_character.Core.Game.Buildings.Build.Interactions
{
    internal class PowerStatitionInteraction : IInteractionAction
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IResourceManager _resourceManager;
        private const int _uranusResourceRequiredCount = 1;
        private const int _electrociryResourceProfit = 3;

        public PowerStatitionInteraction(IEventAggregator eventAggregator, IResourceManager resourceManager)
        {
            _eventAggregator = eventAggregator;
            _resourceManager = resourceManager;
        }

        public void Interacte(character character)
        {
            _eventAggregator.GetEvent<GameEvent<TakeDamageCharacterEvent>>().Publish(new TakeDamageCharacterEvent { CharacterId = character.Id, Damage = 1000 });
            if (_resourceManager.TrySpend(ResourceType.Uranus, _uranusResourceRequiredCount))
                _resourceManager.Increse(ResourceType.Electricity, _electrociryResourceProfit);
        }
    }
}