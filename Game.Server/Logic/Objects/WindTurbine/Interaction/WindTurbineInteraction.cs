using Game.Server.Common;
using Game.Server.Logic.Objects._Interactions;
using Game.Server.Logic.Objects.Characters;
using Game.Server.Logic.Objects.Weather;
using Game.Server.Logic.Resources;
using Game.Server.Models.Constants;
using Game.Server.Models.GameObjects;

namespace Game.Server.Logic.Objects.WindTurbine.Interaction
{
    internal class WindTurbineInteraction : InteractableBuildingInteraction
    {
        public WindTurbineInteraction(IResourceManager resourceManager, ICharacterDamageService characterDamageService, IWeatherService weatherService) : base(resourceManager, characterDamageService)
        {
            AddTargetResource(ResourceType.Energy, 1.2f);
            AddAdditionalRequirements(new WindRequirement(weatherService));
        }

        private class WindRequirement : IRequirement<GameObjectAggregator>
        {
            private readonly IWeatherService _weatherService;

            public WindRequirement(IWeatherService weatherService)
            {
                _weatherService = weatherService;
            }

            public bool Satisfy(GameObjectAggregator item) => 
                _weatherService.IsWindyThere(item.Area.Select(a => a.Coordiante).ToArray());
        }
    }
}