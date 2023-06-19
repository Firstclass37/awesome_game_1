namespace Game.Server.Events.List.Character
{
    internal class TakeDamageCharacterEvent
    {
        public int CharacterId { get; set; }

        public double Damage { get; set; }
    }
}
