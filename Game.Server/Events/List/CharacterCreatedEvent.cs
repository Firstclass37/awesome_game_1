using Game.Server.Models.Maps;

namespace Game.Server.Events.List
{
    public class CharacterCreatedEvent
    {
        public Guid CharacterId { get; init; }

        public Coordiante Position { get; init; }

        public int PlayerId { get; init; }
    }
}