using Game.Server.Models.Buildings;
using Game.Server.Models.Constants;

namespace Game.Server.Logic.Objects.TrafficLights
{
    internal interface IPointsman
    {
        Direction SelectDirection(TrafficLight trafficLight, Direction from);
    }
}