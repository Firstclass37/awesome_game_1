using Game.Server.Models.Constants;

namespace Game.Server.Events.List.TrafficLights
{
    internal class TrafficLightDirectionChangedEvent
    {
        public int TrafficLightId { get; set; }

        public Direction Direction { get; set; }

        public int Value { get; set; }

        public int Size { get; set; }
    }
}
