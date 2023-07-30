using Game.Server.Models.Constants;

namespace Game.Server.API.TrafficLight
{
    internal class TrafficLightController : ITrafficLightController
    {
        public void Increase(Guid trafficLightId, Direction direction)
        {
            throw new NotImplementedException();
        }

        public void Decrease(Guid trafficLightId, Direction direction)
        {
            throw new NotImplementedException();
        }
    }
}