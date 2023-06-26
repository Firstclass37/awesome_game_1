namespace Game.Server.Models.Maps
{
    internal class EntityPosition: IEntityObject
    {
        public EntityPosition()
        {
            Id = Guid.NewGuid(); 
        }

        public Guid Id { get; set; }

        public Guid EntityId { get; set; }

        public Guid PositionId { get; set; }
    }
}