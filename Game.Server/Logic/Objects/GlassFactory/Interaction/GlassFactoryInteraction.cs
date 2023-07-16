using Game.Server.Logic.Objects._Interactions;
using Game.Server.Logic.Objects.Characters;
using Game.Server.Logic.Resources;
using Game.Server.Models.Constants;

namespace Game.Server.Logic.Objects.GlassFactory.Interaction
{
    internal class GlassFactoryInteraction : InteractableBuildingInteraction
    {
        public GlassFactoryInteraction(IResourceManager resourceManager, ICharacterDamageService characterDamageService) : base(resourceManager, characterDamageService)
        {
            AddRequiredResource(ResourceType.Energy, 0.2f);
            AddRequiredResource(ResourceType.Fuel, 0.25f);
            AddRequiredResource(ResourceType.Silicon, 1);

            AddTargetResource(ResourceType.Glass, 0.5f);
        }
    }
}