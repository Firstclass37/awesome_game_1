using Game.Server.Models.Maps;
using Game.Server.Models.Weather;
using Game.Server.Storage;

namespace Game.Server.Logic.Objects.Weather
{
    internal class WeatherService : IWeatherService
    {
        private readonly IStorage _storage;

        public WeatherService(IStorage storage)
        {
            _storage = storage;
        }

        public bool IsWindyThere(IReadOnlyCollection<Coordiante> coordiantes, bool fullCoverage = false)
        {
            var wind = _storage.Find<Wind>(w => true).FirstOrDefault();
            if (wind == null)
                return false;

            return fullCoverage
                ? coordiantes.Intersect(wind.Area).Count() == coordiantes.Count
                : coordiantes.Intersect(wind.Area).Any();
        }
    }
}