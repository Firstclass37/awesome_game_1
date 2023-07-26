using Game.Server.Models.Maps;

namespace Game.Server.Events.List.Character
{
    internal class CharacterPositionChanged
    {
        public Guid CharacterId { get; init; }

        public string ObjectType { get; init; }

        public Coordiante Position { get; init; }
    }
}