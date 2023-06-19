using System;

namespace Game.Server.Events.List.Homes
{
    internal class BuildingDelayedActionEvent
    {
        public int BuidlingId { get; set; }

        public double DelaySec { get; set; }

        public Action Event { get; set; }
    }
}