using Game.Server.Logic.Objects._Interactions;
using Game.Server.Logic.Objects.Characters;
using Game.Server.Logic.Resources;
using Game.Server.Models.Constants;

namespace Game.Server.Logic.Objects.MoistureTrap.Interaction
{
    internal class MoistureTrapInteraction : InteractableBuildingInteraction
    {
        public MoistureTrapInteraction(IResourceManager resourceManager, ICharacterDamageService characterDamageService) : base(resourceManager, characterDamageService)
        {
            AddRequiredResource(ResourceType.Energy, 0.6f);

            AddTargetResource(ResourceType.Water, 1);
        }
    }
}