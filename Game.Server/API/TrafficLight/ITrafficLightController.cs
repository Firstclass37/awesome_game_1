using Game.Server.Models.Constants;

namespace Game.Server.API.TrafficLight
{
    public interface ITrafficLightController
    {
        void Decrease(Guid trafficLightId, Direction direction);
        void Increase(Guid trafficLightId, Direction direction);
    }
}