using Game.Server.Logic.Objects._Interactions;
using Game.Server.Logic.Objects.Characters;
using Game.Server.Logic.Resources;
using Game.Server.Models.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Server.Logic.Objects.ElectronicPlant.Interactions
{
    internal class ElectronicPlantInteraction : InteractableBuildingInteraction
    {
        public ElectronicPlantInteraction(IResourceManager resourceManager, ICharacterDamageService characterDamageService) : base(resourceManager, characterDamageService)
        {
            AddRequiredResource(ResourceType.Energy, 0.2f);
            AddRequiredResource(ResourceType.Aluminum, 0.25f);
            AddRequiredResource(ResourceType.Coal, 0.5f);
            AddRequiredResource(ResourceType.Silicon, 0.25f);

            AddTargetResource(ResourceType.Microchip, 0.3f);
        }
    }
}