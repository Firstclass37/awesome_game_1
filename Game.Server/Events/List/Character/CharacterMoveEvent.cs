using Game.Server.Models.Maps;

namespace Game.Server.Events.List.Character
{
    public class CharacterMoveEvent
    {
        public Guid CharacterId { get; set; }

        public Coordiante NewPosition { get; set; }

        public float Speed { get; set; }
    }
}