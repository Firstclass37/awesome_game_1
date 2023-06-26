namespace Game.Server.Events.List.Character
{
    internal class TakeDamageCharacterEvent
    {
        public Guid CharacterId { get; set; }

        public double Damage { get; set; }
    }
}
