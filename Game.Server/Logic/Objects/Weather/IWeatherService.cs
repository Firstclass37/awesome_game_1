using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects.Weather
{
    internal interface IWeatherService
    {
        public bool IsWindyThere(IReadOnlyCollection<Coordiante> coordiantes, bool fullCoverage = false);
    }
}