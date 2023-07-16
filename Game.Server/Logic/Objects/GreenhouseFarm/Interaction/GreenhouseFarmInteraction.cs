using Game.Server.Logic.Objects._Interactions;
using Game.Server.Logic.Objects.Characters;
using Game.Server.Logic.Resources;
using Game.Server.Models.Constants;

namespace Game.Server.Logic.Objects.GreenhouseFarm.Interaction
{
    internal class GreenhouseFarmInteraction : InteractableBuildingInteraction
    {
        public GreenhouseFarmInteraction(IResourceManager resourceManager, ICharacterDamageService characterDamageService) : base(resourceManager, characterDamageService)
        {
            AddRequiredResource(ResourceType.Energy, 1);
            AddRequiredResource(ResourceType.Water, 0.1f);

            AddTargetResource(ResourceType.Food, 1);
        }
    }
}
