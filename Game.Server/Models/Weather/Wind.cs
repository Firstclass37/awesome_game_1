using Game.Server.Models.Constants;
using Game.Server.Models.Maps;

namespace Game.Server.Models.Weather
{
    internal record Wind: IEntityObject
    {
        public Wind()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }

        public Coordiante[] Area { get; init; } = Array.Empty<Coordiante>();

        public Direction Direction { get; init; }

        public double StartedAt { get; init; }

        public double EndAt { get; init; }
    }
}