namespace Game.Server.Events.List.Resource
{
    internal class ResourceDecreaseEvent
    {
        public int ResourceTypeId { get; set; }

        public float Amount { get; set; }
    }
}