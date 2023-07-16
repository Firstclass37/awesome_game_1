using Game.Server.Logic.Objects._Interactions;
using Game.Server.Logic.Objects.Characters;
using Game.Server.Logic.Resources;
using Game.Server.Models.Constants;

namespace Game.Server.Logic.Objects.ElectrolysisReactor.Interactions
{
    internal class ElectrolysisReactorInteraction : InteractableBuildingInteraction
    {
        public ElectrolysisReactorInteraction(IResourceManager resourceManager, ICharacterDamageService characterDamageService) : base(resourceManager, characterDamageService)
        {
            AddRequiredResource(ResourceType.Energy, 1);
            AddRequiredResource(ResourceType.Water, 1);

            AddTargetResource(ResourceType.Fuel, 0.5f);
        }
    }
}