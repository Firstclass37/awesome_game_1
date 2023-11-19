using Game.Server.Models.Maps;

namespace Game.Server.Events.List.Homes
{
    public class ProduceStartEvent
    {
        public Guid BuildingId { get; init; }

        public Coordiante Root { get; init; }

        public double Speed { get; init; }

        public int QueueSize { get; init; }
    }
}