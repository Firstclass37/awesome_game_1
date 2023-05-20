using Godot;
using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Game;
using My_awesome_character.Core.Systems._Core;

namespace My_awesome_character.Entities
{
    public partial class Home: Node2D, IPeriodicActionOwner
    {
        public int Id { get; set; }

        public BuildingTypes BuildingType { get; set; }

        public IPeriodicAction PeriodicAction { get; set; }

        public MapCell RootCell { get; set; }

        public MapCell[] Cells { get; set; }
    }
}