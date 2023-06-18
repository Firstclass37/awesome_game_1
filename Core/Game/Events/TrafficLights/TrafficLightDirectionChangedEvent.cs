using My_awesome_character.Core.Game.Constants;

namespace My_awesome_character.Core.Game.Events.TrafficLights
{
    internal class TrafficLightDirectionChangedEvent
    {
        public int TrafficLightId { get; set; }

        public Direction Direction { get; set; }

        public int Value { get; set; }

        public int Size { get; set; }
    }
}
