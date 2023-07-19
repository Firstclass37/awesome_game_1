using Game.Server.Models.Maps;

namespace Game.Server.Events.List.Homes
{
    public class ObjectCreatedEvent
    {
        public Guid Id { get; init; }

        public string ObjectType { get; init; }

        public Coordiante Root { get; init; }

        public Coordiante[] Area { get; init; }
    }
}