using Game.Server.Logic.Objects.Characters;
using Game.Server.Logic.Resources;
using Game.Server.Models.GameObjects;
using Game.Server.Models.GamesObjectList;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects._Interactions
{
    internal abstract class InteractableBuildingInteraction: ICharacterInteraction
    {
        private readonly IResourceManager _resourceManager;
        private readonly ICharacterDamageService _characterDamageService;

        private readonly Dictionary<int, float> _resourcesRequired = new();
        private readonly Dictionary<int, float> _resourceTarget = new();

        public InteractableBuildingInteraction(IResourceManager resourceManager, ICharacterDamageService characterDamageService)
        {
            _resourceManager = resourceManager;
            _characterDamageService = characterDamageService;
        }

        public void Interact(GameObjectAggregator gameObject, Character character, Coordiante interactionPoint)
        {
            if (SwapResources())
                _characterDamageService.InstantKill(character);
        }

        protected internal void AddRequiredResource(int resourceId, float amout) 
        {
            _resourcesRequired.Add(resourceId, amout);
        }

        protected internal void AddTargetResource(int resourceId, float amout) 
        {
            _resourceTarget.Add(resourceId, amout);
        }

        private bool SwapResources()
        {
            if (_resourcesRequired.All(r => _resourceManager.GetAmount(r.Key) > r.Value))
            {
                foreach (var resource in _resourcesRequired)
                    _resourceManager.TrySpend(resource.Key, (int)resource.Value);

                foreach (var resource in _resourceTarget)
                    _resourceManager.Increase(resource.Key, (int)resource.Value);

                return true;
            }
            return false;
        }
    }
}