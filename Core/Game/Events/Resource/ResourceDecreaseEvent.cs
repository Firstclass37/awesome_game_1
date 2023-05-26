namespace My_awesome_character.Core.Game.Events.Resource
{
    internal class ResourceDecreaseEvent
    {
        public int ResourceTypeId { get; set; }

        public int Amount { get; set; }
    }
}