using Game.Server.Logic.Objects._Interactions;
using Game.Server.Logic.Objects.Characters;
using Game.Server.Logic.Resources;
using Game.Server.Models.Constants;

namespace Game.Server.Logic.Objects.Storage.Interaction
{
    internal class StorageInteraction : InteractableBuildingInteraction
    {
        public StorageInteraction(IResourceManager resourceManager, ICharacterDamageService characterDamageService) : base(resourceManager, characterDamageService)
        {
            //todo: ТОЛЬКО ПРИ ОСАДКАХ

            AddTargetResource(ResourceType.Water, 1);
        }
    }
}