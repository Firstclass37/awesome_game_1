using Game.Server.Models.Constants;
using Game.Server.Models.Maps;

namespace Game.Server.Models.GameObjects
{
    internal class GameObject : IEntityObject
    {
        public GameObject(string objectType, bool isVisible = true, int playerId = Players.System, Guid? creator = null)
        {
            Id = Guid.NewGuid();
            ObjectType = objectType;
            IsVisible = isVisible;
            CreatedDate = DateTime.UtcNow;
            PlayerId = playerId;
            Creator = creator;
        }

        public Guid Id { get; set; }

        public string ObjectType { get; }

        public DateTime CreatedDate { get; }

        public int PlayerId { get; set; }

        public Guid? Creator { get; set; }

        public bool IsVisible { get; }
    }

    internal record GameObjectToAttribute : IEntityObject
    {
        public GameObjectToAttribute(Guid gameObjectId, string attributeType, object value)
        {
            Id = Guid.NewGuid();
            GameObjectId = gameObjectId;
            AttributeType = attributeType;
            Value = value;
        }

        public Guid Id { get; set; }

        public Guid GameObjectId { get; init; }

        public string AttributeType { get; init; }

        public object Value { get; init; }
    }

    internal record GameObjectPosition : IEntityObject
    {
        public GameObjectPosition(Guid entityId, Coordiante coordiante, bool isRoot = false, bool isBlock = true)
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.UtcNow;
            EntityId = entityId;
            IsRoot = isRoot;
            IsBlock = isBlock;
            Coordiante = coordiante;
        }

        public Guid Id { get; set; }

        public Guid EntityId { get; }

        public bool IsRoot { get; }

        public bool IsBlock { get; }

        public Coordiante Coordiante { get; init; }

        public DateTime CreatedDate { get; set; }
    }
}