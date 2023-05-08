namespace My_awesome_character.Core.Game.Events
{
    public class MovementCharacterPathEvent
    {
        public int CharacterId { get; set; }

        public MapCell[] Path { get; set; }
    }
}