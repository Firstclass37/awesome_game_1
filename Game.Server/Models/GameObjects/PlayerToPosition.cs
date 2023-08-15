using Game.Server.Models.Maps;

namespace Game.Server.Models.GameObjects
{
    internal record PlayerToPosition: IEntityObject
    {
        public PlayerToPosition()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; init; }

        public Coordiante Coordinate { get; init; }

        public int PlayerNumber { get; init; }
    }
}