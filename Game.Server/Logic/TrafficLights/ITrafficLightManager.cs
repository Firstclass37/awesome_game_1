using Game.Server.Models.Buildings;
using Game.Server.Models.Constants;

namespace Game.Server.Logic.TrafficLights
{
    internal interface ITrafficLightManager
    {
        void AddTrafficLight(TrafficLight trafficLightModel);
        void DecreaseSize(TrafficLight trafficLight, Direction direction, int decrement = 1);
        TrafficLight Get(int id);
        void IncreaseSize(TrafficLight trafficLight, Direction direction, int increment = 1);
        void UpdateValue(TrafficLight trafficLight, Direction direction, int value);
    }
}