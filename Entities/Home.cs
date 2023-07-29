using Godot;
using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Game;

namespace My_awesome_character.Entities
{
    public partial class Home: Node2D, IInteractable
    {
        public int Id { get; set; }

        public BuildingTypes BuildingType { get; set; }

        public IPeriodicAction PeriodicAction { get; set; }

        public MapCell RootCell { get; set; }

        public MapCell[] Cells { get; set; }

        public IInteractionAction InteractionAction { get; set; }
    }
}