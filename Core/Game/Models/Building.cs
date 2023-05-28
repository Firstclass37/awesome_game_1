using My_awesome_character.Core.Constatns;

namespace My_awesome_character.Core.Game.Models
{
    internal class Building
    {
        public int Id { get; set; }

        public BuildingTypes BuildingType { get; set; }

        public IPeriodicAction PeriodicAction { get; set; }

        public MapCell RootCell { get; set; }

        public MapCell[] Cells { get; set; }

        public IInteractionAction InteractionAction { get; set; }
    }
}