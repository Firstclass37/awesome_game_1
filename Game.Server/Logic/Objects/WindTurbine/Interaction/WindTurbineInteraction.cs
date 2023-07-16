using Game.Server.Logic.Objects._Interactions;
using Game.Server.Logic.Objects.Characters;
using Game.Server.Logic.Resources;
using Game.Server.Models.Constants;

namespace Game.Server.Logic.Objects.WindTurbine.Interaction
{
    internal class WindTurbineInteraction : InteractableBuildingInteraction
    {
        public WindTurbineInteraction(IResourceManager resourceManager, ICharacterDamageService characterDamageService) : base(resourceManager, characterDamageService)
        {
            //todo: ТОЛЬКО ПРИ ВЕТРЕ
            AddTargetResource(ResourceType.Energy, 1.2f);
        }
    }
}