using Game.Server.Models.Maps;

namespace Game.Server.Events.List
{
    public class MovementCharacterPathEvent
    {
        public int CharacterId { get; set; }

        public Coordiante[] Path { get; set; }
    }
}