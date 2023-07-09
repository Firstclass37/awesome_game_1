using Game.Server.Events.Core;
using Game.Server.Events.List.Character;
using Game.Server.Logic.Objects._Interactions;
using Game.Server.Logic.Resources;
using Game.Server.Models.Constants;
using Game.Server.Models.GameObjects;
using Game.Server.Models.GamesObjectList;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects.PowerStations.Interaction
{
    internal class PowerStatitionInteraction : ICharacterInteraction
    {
        private readonly IResourceManager _resourceManager;
        private const int _uranusResourceRequiredCount = 1;
        private const int _electrociryResourceProfit = 3;

        public PowerStatitionInteraction(IResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
        }

        public void Interact(GameObjectAggregator gameObject, Character character, Coordiante interactionPoint)
        {
            // kill plaery
            //_eventAggregator.GetEvent<GameEvent<TakeDamageCharacterEvent>>().Publish(new TakeDamageCharacterEvent { CharacterId = gameObject.Id, Damage = 1000 });
            if (_resourceManager.TrySpend(ResourceType.Uranus, _uranusResourceRequiredCount))
                _resourceManager.Increase(ResourceType.Electricity, _electrociryResourceProfit);
        }
    }
}
