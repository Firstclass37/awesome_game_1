namespace My_awesome_character.Core.Game.Events.Homes
{
    internal class BuildingDelayedActionEvent
    {
        public int BuidlingId { get; set; }

        public double DelaySec { get; set; }

        public object Event { get; set; }
    }
}