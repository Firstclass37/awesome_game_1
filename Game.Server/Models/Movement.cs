using Game.Server.Models.Maps;

namespace Game.Server.Models
{
    internal record Movement : IEntityObject
    {
        public Movement(Guid gameObjectId, Coordiante[] path, Guid initiator)
        {
            Id = Guid.NewGuid();
            GameObjectId = gameObjectId;
            Path = path;
            Initiator = initiator;
            LastMovementTime = 0;
            Active = true;
        }

        public Guid Id { get; }

        public Guid GameObjectId { get; }

        public Coordiante CurrentMoveToPosition { get; init; }

        public Coordiante[] Path { get; }

        public Guid Initiator { get; }

        public bool Active { get; init; }

        public double LastMovementTime { get; init; }
    }
}