using Game.Server.Models.Maps;

namespace Game.Server.Models.GameObjects
{
    internal class GameObject : IEntityObject
    {
        public GameObject(string objectType, bool isVisible = true)
        {
            Id = Guid.NewGuid();
            ObjectType = objectType;
            IsVisible = isVisible;
        }

        public Guid Id { get; set; }

        public string ObjectType { get; }

        public bool IsVisible { get; }
    }

    internal class GameObjectToAttribute : IEntityObject
    {
        public GameObjectToAttribute(Guid gameObjectId, string attributeType, object value)
        {
            Id = Guid.NewGuid();
            GameObjectId = gameObjectId;
            AttributeType = attributeType;
            Value = value;
        }

        public Guid Id { get; set; }

        public Guid GameObjectId { get; }

        public string AttributeType { get; }

        public object Value { get; }
    }

    internal class GameObjectPosition : IEntityObject
    {
        public GameObjectPosition(Guid entityId, Coordiante coordiante, bool isRoot = false, bool isBlock = true)
        {
            Id = Guid.NewGuid();
            EntityId = entityId;
            IsRoot = isRoot;
            IsBlock = isBlock;
            Coordiante = coordiante;
        }

        public Guid Id { get; set; }

        public Guid EntityId { get; }

        public bool IsRoot { get; }

        public bool IsBlock { get; }

        public Coordiante Coordiante { get; }
    }
}