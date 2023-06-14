using System;

namespace My_awesome_character.Core.Game.Events.Homes
{
    internal class BuildingDelayedActionEvent
    {
        public int BuidlingId { get; set; }

        public double DelaySec { get; set; }

        public Action Event { get; set; }
    }
}