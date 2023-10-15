using Game.Server.Models.Constants;
using Game.Server.Models.Maps;

namespace Game.Server.Events.List.Weather
{
    public class WindStartEvent
    {
        public Guid Id { get; init; }

        public Coordiante[] Area { get; init; } = Array.Empty<Coordiante>();

        public Direction Direction { get; init; }
    }
}