using Game.Server.Models.Constants;

namespace Game.Server.Events.List.Game
{
    public record TimeChangedEvent
    {
        public int Hours { get; init; }

        public TimeOfDay TimeOfDay { get; init; }
    }
}