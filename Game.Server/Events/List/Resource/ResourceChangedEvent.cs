namespace Game.Server.Events.List.Resource
{
    public class ResourceChangedEvent
    {
        public int ResourceTypeId { get; set; }

        public float Amount { get; set; }
    }
}