using Game.Server.Models.Constants;
using Game.Server.Models.Maps;

namespace Game.Server.Events.List.TrafficLights
{
    public class TrafficLightChangedEvent
    {
        public Guid Id { get; init; }

        public Coordiante Position { get; init; }

        public IReadOnlyDictionary<Direction, int> Values { get; init; }

        public IReadOnlyDictionary<Direction, int> Capasities { get; init; }
    }
}