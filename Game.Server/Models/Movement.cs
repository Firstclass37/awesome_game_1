using Game.Server.Models.Maps;

namespace Game.Server.Models
{
    internal record Movement : IEntityObject
    {
        public Movement(Guid gameObjectId, Coordiante[] path)
        {
            Id = Guid.NewGuid();
            GameObjectId = gameObjectId;
            Path = path;
            LastMovementTime = 0;
        }

        public Guid Id { get; }

        public Guid GameObjectId { get; }

        public Coordiante[] Path { get; }

        public bool Active { get; init; }

        public double LastMovementTime { get; init; }
    }
}