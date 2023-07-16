using Game.Server.Logic.Objects._Interactions;
using Game.Server.Logic.Objects.Characters;
using Game.Server.Logic.Resources;
using Game.Server.Models.Constants;

namespace Game.Server.Logic.Objects.CoalMine.Interaction
{
    internal class CoalMineInteraction : InteractableBuildingInteraction
    {
        public CoalMineInteraction(IResourceManager resourceManager, ICharacterDamageService characterDamageService) : base(resourceManager, characterDamageService)
        {
            AddRequiredResource(ResourceType.Energy, 0.1f);

            AddTargetResource(ResourceType.Coal, 1);
        }
    }
}