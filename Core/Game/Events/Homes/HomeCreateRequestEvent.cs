using My_awesome_character.Core.Constatns;

namespace My_awesome_character.Core.Game.Events.Homes
{
    public class HomeCreateRequestEvent
    {
        public BuildingTypes BuildingType { get; set; }

        public MapCell TargetCell { get; set; }
    }
}