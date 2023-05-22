namespace My_awesome_character.Core.Game.Events.Character
{
    internal class CharacterPositionChangedEvent
    {
        public int CharacterId { get; set; }

        public MapCell NewPosition { get; set; }
    }
}