using Game.Server.Logic.Objects._Interactions;
using Game.Server.Logic.Objects.Characters;
using Game.Server.Logic.Resources;
using Game.Server.Models.Constants;

namespace Game.Server.Logic.Objects.UranusMine.Interaction
{
    internal class UranusMineInteraction : InteractableBuildingInteraction
    {
        public UranusMineInteraction(IResourceManager resourceManager, ICharacterDamageService characterDamageService) : base(resourceManager, characterDamageService)
        {
            AddRequiredResource(ResourceType.Energy, 0.1f);

            AddTargetResource(ResourceType.Uranus, 1);
        }
    }
}