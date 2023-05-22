namespace My_awesome_character.Core.Game.Events.Character
{
    internal class TakeDamageCharacterEvent
    {
        public int CharacterId { get; set; }

        public double Damage { get; set; }
    }
}
