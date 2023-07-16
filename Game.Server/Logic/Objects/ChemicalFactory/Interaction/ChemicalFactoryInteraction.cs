using Game.Server.Logic.Objects._Interactions;
using Game.Server.Logic.Objects.Characters;
using Game.Server.Logic.Resources;
using Game.Server.Models.Constants;

namespace Game.Server.Logic.Objects.ChemicalFactory.Interaction
{
    internal class ChemicalFactoryInteraction : InteractableBuildingInteraction
    {
        public ChemicalFactoryInteraction(IResourceManager resourceManager, ICharacterDamageService characterDamageService) : base(resourceManager, characterDamageService)
        {
            AddRequiredResource(ResourceType.Steel, 30);
            AddRequiredResource(ResourceType.Coal, 20);

            AddTargetResource(ResourceType.Chemicals, 0.6f);
        }
    }
}