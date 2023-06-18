using My_awesome_character.Core.Game.Models;
using My_awesome_character.Core.Game.Movement;

namespace My_awesome_character.Core.Game.TrafficLights
{
    internal interface ITrafficLightInteraction
    {
        void Interact(TrafficLightModel trafficLight, character character, INeighboursAccessor neighboursAccessor);
    }
}