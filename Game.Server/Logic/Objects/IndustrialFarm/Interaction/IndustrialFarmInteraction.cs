using Game.Server.Logic.Objects._Interactions;
using Game.Server.Logic.Objects.Characters;
using Game.Server.Logic.Resources;
using Game.Server.Models.Constants;

namespace Game.Server.Logic.Objects.IndustrialFarm.Interaction
{
    internal class IndustrialFarmInteraction : InteractableBuildingInteraction
    {
        public IndustrialFarmInteraction(IResourceManager resourceManager, ICharacterDamageService characterDamageService) : base(resourceManager, characterDamageService)
        {
            AddRequiredResource(ResourceType.Energy, 1);
            AddRequiredResource(ResourceType.Chemicals, 0.1f);

            AddTargetResource(ResourceType.Food, 2);
        }
    }
}
