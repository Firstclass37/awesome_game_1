using Game.Server.Events.Core;
using Game.Server.Events.List.Character;
using Game.Server.Logic.Resources;
using Game.Server.Models.Constants;
using Game.Server.Models.Maps;
using Game.Server.Models.Temp;

namespace Game.Server.Logic.Building.Build.Interactions
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

        public void Interacte(GameObject gameObject, Coordiante interactionCoordinate)
        {
            if (gameObject.ObjectType != GameObjectType.Character)
                return;

            _eventAggregator.GetEvent<GameEvent<TakeDamageCharacterEvent>>().Publish(new TakeDamageCharacterEvent { CharacterId = gameObject.Id, Damage = 1000 });
            if (_resourceManager.TrySpend(ResourceType.Uranus, _uranusResourceRequiredCount))
                _resourceManager.Increase(ResourceType.Electricity, _electrociryResourceProfit);
        }
    }
}