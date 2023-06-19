using Game.Server.Models.Maps;

namespace Game.Server.Events.List.Character
{
    internal class CharacterPositionChangedEvent
    {
        public int CharacterId { get; set; }

        public Coordiante NewPosition { get; set; }
    }
}