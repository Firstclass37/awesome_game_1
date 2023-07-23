namespace Game.Server.Models
{
    internal class GameObjectInteraction : IEntityObject
    {
        public GameObjectInteraction(Guid gameObjectId, string interactionType)
        {
            Id = Guid.NewGuid();
            GameObjectId = gameObjectId;
            InteractionType = interactionType;
        }

        public Guid Id { get; }

        public Guid GameObjectId { get; }

        public string InteractionType { get; }
    }
}