namespace Game.Server.Events.List.Resource
{
    public class ResourceIncreaseEvent
    {
        public int ResourceTypeId { get; set; }

        public float Amount { get; set; }
    }
}