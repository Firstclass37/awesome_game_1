namespace Game.Server.Events.List.Resource
{
    internal class ResourceDecreaseEvent
    {
        public int ResourceTypeId { get; set; }

        public int Amount { get; set; }
    }
}