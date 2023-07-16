using Game.Server.Logic.Objects._Interactions;
using Game.Server.Logic.Objects.Characters;
using Game.Server.Logic.Resources;
using Game.Server.Models.Constants;

namespace Game.Server.Logic.Objects.SteelPlant.Interaction
{
    internal class SteelPlantInteraction : InteractableBuildingInteraction
    {
        public SteelPlantInteraction(IResourceManager resourceManager, ICharacterDamageService characterDamageService) : base(resourceManager, characterDamageService)
        {
            AddRequiredResource(ResourceType.Energy, 0.3f);
            AddRequiredResource(ResourceType.Iron, 0.5f);
            AddRequiredResource(ResourceType.Coal, 0.5f);

            AddTargetResource(ResourceType.Steel, 0.6f);
        }
    }
}