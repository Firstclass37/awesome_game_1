namespace Game.Server.Events.List.Game
{
    public record TimeChangedEvent
    {
        public int Hours { get; init; }
    }
}