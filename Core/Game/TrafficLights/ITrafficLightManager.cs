using My_awesome_character.Core.Game.Constants;
using My_awesome_character.Core.Game.Models;

namespace My_awesome_character.Core.Game.TrafficLights
{
    internal interface ITrafficLightManager
    {
        void AddTrafficLight(TrafficLightModel trafficLightModel);
        void DecreaseSize(TrafficLightModel trafficLight, Direction direction, int decrement = 1);
        TrafficLightModel Get(int id);
        void IncreaseSize(TrafficLightModel trafficLight, Direction direction, int increment = 1);
        void UpdateValue(TrafficLightModel trafficLight, Direction direction, int value);
    }
}