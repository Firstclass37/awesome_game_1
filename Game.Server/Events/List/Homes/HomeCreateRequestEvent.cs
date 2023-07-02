using Game.Server.Models.Constants;
using Game.Server.Models.Maps;

namespace Game.Server.Events.List.Homes
{
    public class HomeCreateRequestEvent
    {
        public string BuildingType { get; set; }

        public Coordiante TargetCell { get; set; }
    }
}