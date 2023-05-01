namespace My_awesome_character.Core.Game.Unknown
{
    internal class CharacterMovement
    {
        public int CharacterId { get; set; }

        public MapCell StartCell { get; set; }

        public MapCell[] Path { get; set; }

        public bool Actual { get; set; }
    }
}