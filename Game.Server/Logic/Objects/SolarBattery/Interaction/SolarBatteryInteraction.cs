using Game.Server.Logic.Objects._Interactions;
using Game.Server.Logic.Objects.Characters;
using Game.Server.Logic.Resources;
using Game.Server.Models.Constants;

namespace Game.Server.Logic.Objects.SolarBattery.Interaction
{
    internal class SolarBatteryInteraction : InteractableBuildingInteraction
    {
        public SolarBatteryInteraction(IResourceManager resourceManager, ICharacterDamageService characterDamageService) : base(resourceManager, characterDamageService)
        {

            //todo: ТОЛЬКО ДНЕМ
            AddTargetResource(ResourceType.Energy, 1.44f);
        }
    }
}