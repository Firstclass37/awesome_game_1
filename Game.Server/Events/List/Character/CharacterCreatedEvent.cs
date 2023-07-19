using Game.Server.Models.Maps;

namespace Game.Server.Events.List.Character
{
    internal class CharacterCreatedEvent
    {
        public Guid Id { get; init; }

        public Coordiante Position { get; init; }
    }
}