namespace Game.Server.Logic.Objects.Weather
{
    internal interface IWeatherService
    {
        bool IsRainy();

        bool IsWindy();

        bool IsNight();

        bool IsSunny();
    }
}