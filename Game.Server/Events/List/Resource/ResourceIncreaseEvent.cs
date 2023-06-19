namespace Game.Server.Events.List.Resource
{
    public class ResourceIncreaseEvent
    {
        public int ResourceTypeId { get; set; }

        public int Amount { get; set; }
    }
}