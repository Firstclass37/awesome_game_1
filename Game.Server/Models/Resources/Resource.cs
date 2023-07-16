namespace Game.Server.Models.Resources
{
    internal class Resource: IEntityObject
    {
        public Resource()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; internal set; }

        public int ResourceType { get; set; }

        public string Name { get; internal set; }

        public float Value { get; internal set; }
    }
}