namespace Game.Server.Models.Resources
{
    internal class Resource: IEntityObject
    {
        public int Id { get; internal set; }

        public string Name { get; internal set; }

        public int Value { get; internal set; }
    }
}