using Game.Server.Common;
using Game.Server.Logic.Objects.Characters;
using Game.Server.Logic.Resources;
using Game.Server.Models.GameObjects;
using Game.Server.Models.GamesObjectList;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects._Interactions
{
    internal abstract class InteractableBuildingInteraction: CharacterInteraction
    {
        private readonly IResourceManager _resourceManager;
        private readonly ICharacterDamageService _characterDamageService;

        private readonly Dictionary<int, float> _resourcesRequired = new();
        private readonly Dictionary<int, float> _resourceTarget = new();
        private readonly List<IRequirement<GameObjectAggregator>> _requirements = new();

        public InteractableBuildingInteraction(IResourceManager resourceManager, ICharacterDamageService characterDamageService)
        {
            _resourceManager = resourceManager;
            _characterDamageService = characterDamageService;
        }

        protected override void OnInteract(GameObjectAggregator gameObject, Character character, Coordiante interactionPoint)
        {
            _characterDamageService.InstantKill(character);

            if (_requirements.All(r => r.Satisfy(gameObject)))
                SwapResources();
        }

        protected internal void AddRequiredResource(int resourceId, float amout) 
        {
            _resourcesRequired.Add(resourceId, amout);
        }

        protected internal void AddTargetResource(int resourceId, float amout) 
        {
            _resourceTarget.Add(resourceId, amout);
        }

        protected internal void AddAdditionalRequirements(IRequirement<GameObjectAggregator> requirement)
        {
            _requirements.Add(requirement);
        }

        private bool SwapResources()
        {
            if (_resourcesRequired.All(r => _resourceManager.GetAmount(r.Key) > r.Value))
            {
                foreach (var resource in _resourcesRequired)
                    _resourceManager.TrySpend(resource.Key, resource.Value);

                foreach (var resource in _resourceTarget)
                    _resourceManager.Increase(resource.Key, resource.Value);

                return true;
            }
            return false;
        }
    }
}