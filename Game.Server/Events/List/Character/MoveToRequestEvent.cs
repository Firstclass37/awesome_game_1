using Game.Server.Models.Maps;

namespace Game.Server.Events.List.Character
{
    internal class MoveToRequestEvent
    {
        public int CharacterId { get; set; }

        public Coordiante TargetCell { get; set; }
    }
}