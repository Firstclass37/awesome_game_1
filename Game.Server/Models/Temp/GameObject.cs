using Game.Server.Models.Constants;
using Game.Server.Models.Maps;

namespace Game.Server.Models.Temp
{
    internal class GameObject: IEntityObject
    {
        public GameObject()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public GameObjectType ObjectType { get; set; }

        public bool IsVisible { get; set; }
    }

    internal class GameObjectToAttribute : IEntityObject
    {
        public GameObjectToAttribute()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public Guid GameObjectId { get; set; }

        public AttributeType AttributeType { get; set; }

        public string Value { get; set; }
    }

    internal class GameObjectPosition: IEntityObject
    {
        public GameObjectPosition()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public Guid EntityId { get; set; }

        public Guid PositionId { get; set; }

        public bool IsRoot { get; set; }

        public bool IsBlock { get; set; }

        public Coordiante Coordiante { get; set; }
    }
}