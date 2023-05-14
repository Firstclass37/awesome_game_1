namespace My_awesome_character.Core.Game.Events.Homes
{
    public class HomeCreateRequestEvent
    {
        public int HomeType { get; set; }

        public MapCell TargetCell { get; set; }
    }
}