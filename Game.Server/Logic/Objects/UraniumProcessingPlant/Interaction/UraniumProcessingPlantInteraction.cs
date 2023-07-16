using Game.Server.Logic.Objects._Interactions;
using Game.Server.Logic.Objects.Characters;
using Game.Server.Logic.Resources;
using Game.Server.Models.Constants;

namespace Game.Server.Logic.Objects.UraniumProcessingPlant.Interaction
{
    internal class UraniumProcessingPlantInteraction : InteractableBuildingInteraction
    {
        public UraniumProcessingPlantInteraction(IResourceManager resourceManager, ICharacterDamageService characterDamageService) : base(resourceManager, characterDamageService)
        {
            AddRequiredResource(ResourceType.Energy, 0.5f);
            AddRequiredResource(ResourceType.Uranus, 1);

            AddTargetResource(ResourceType.Fuel, 1.25f);
        }
    }
}