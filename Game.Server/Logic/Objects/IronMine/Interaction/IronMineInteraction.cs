using Game.Server.Logic.Objects._Interactions;
using Game.Server.Logic.Objects.Characters;
using Game.Server.Logic.Resources;
using Game.Server.Models.Constants;

namespace Game.Server.Logic.Objects.IronMine.Interaction
{
    internal class IronMineInteraction : InteractableBuildingInteraction
    {
        public IronMineInteraction(IResourceManager resourceManager, ICharacterDamageService characterDamageService) : base(resourceManager, characterDamageService)
        {
            AddRequiredResource(ResourceType.Energy, 1);

            AddTargetResource(ResourceType.Iron, 1);
        }
    }
}