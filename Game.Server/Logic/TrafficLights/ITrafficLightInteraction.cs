using Game.Server.Logic.Characters;
using Game.Server.Models.Buildings;
using Game.Server.Models.Characters;

namespace Game.Server.Logic.TrafficLights
{
    internal interface ITrafficLightInteraction
    {
        void Interact(TrafficLight trafficLight, Character character, INeighboursAccessor neighboursAccessor);
    }
}