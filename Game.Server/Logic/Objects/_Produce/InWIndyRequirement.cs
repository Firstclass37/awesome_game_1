using Game.Server.Logic.Objects.Weather;
using Game.Server.Models.Constants.Attributes;
using Game.Server.Models.GameObjects;

namespace Game.Server.Logic.Objects._Produce
{
    internal class InWIndyRequirement : IProduceRequirement
    {
        private readonly IWeatherService _weatherService;

        public InWIndyRequirement(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public bool Can(GameObjectAggregator gameObjectAggregator)
        {
            return _weatherService.IsWindyThere(gameObjectAggregator.Area.Select(a => a.Coordiante).ToArray());
        }
    }
}
