namespace My_awesome_character.Core.Game.Events.Resource
{
    public class ResourceIncreaseEvent
    {
        public int ResourceTypeId { get; set; }

        public int Amount { get; set; }
    }
}