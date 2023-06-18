using My_awesome_character.Core.Game.Constants;
using My_awesome_character.Core.Game.Models;

namespace My_awesome_character.Core.Game.TrafficLights
{
    internal interface IPointsman
    {
        Direction SelectDirection(TrafficLightModel trafficLight, Direction from);
    }
}