using Game.Server.Logic.Objects._Interactions;
using Game.Server.Logic.Objects.Characters;
using Game.Server.Logic.Resources;
using Game.Server.Models.Constants;

namespace Game.Server.Logic.Objects.NuclearReactor.Interaction
{
    internal class NuclearReactorInteraction : InteractableBuildingInteraction
    {
        public NuclearReactorInteraction(IResourceManager resourceManager, ICharacterDamageService characterDamageService) : base(resourceManager, characterDamageService)
        {
            AddRequiredResource(ResourceType.Uranus, 1);
            AddRequiredResource(ResourceType.Coal, 0.5f);
            AddRequiredResource(ResourceType.Uranus, 0.5f);

            AddTargetResource(ResourceType.Energy, 4);
        }
    }
}