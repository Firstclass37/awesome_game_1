namespace Game.Server.Logic.Objects.Weather
{
    internal interface IWeatherService
    {
        WeatheState GetCurrent();
    }


    public enum WeatheState
    {
        Rainy,
        Windy,
        Night,
        Sunny
    }
}