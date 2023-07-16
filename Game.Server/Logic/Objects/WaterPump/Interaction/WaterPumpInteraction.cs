using Game.Server.Logic.Objects._Interactions;
using Game.Server.Logic.Objects.Characters;
using Game.Server.Logic.Resources;
using Game.Server.Models.Constants;

namespace Game.Server.Logic.Objects.WaterPump.Interaction
{
    internal class WaterPumpInteraction : InteractableBuildingInteraction
    {
        public WaterPumpInteraction(IResourceManager resourceManager, ICharacterDamageService characterDamageService) : base(resourceManager, characterDamageService)
        {
            AddRequiredResource(ResourceType.Energy, 0.2f);

            AddTargetResource(ResourceType.Water, 1);
        }
    }
}
