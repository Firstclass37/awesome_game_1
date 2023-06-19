namespace Game.Server.Events.List.Resource
{
    internal class ResourceAddedEvent
    {
        public int ResourceType { get; set; }

        public int Value { get; set; }
    }
}