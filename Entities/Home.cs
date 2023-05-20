using Godot;
using My_awesome_character.Core.Constatns;
using System.Collections.Generic;
using System.Linq;

namespace My_awesome_character.Entities
{
    public partial class Home: Node2D
    {
        public int Id { get; set; }

        public BuildingTypes BuildingType { get; set; }

        public double LastFireTime { get; set; }

        public double? SpawnEverySecond { get; set; }

        public MapCell RootCell { get; set; }

        public MapCell[] Cells { get; set; }

        public MapCell SpawnCell { get; set; }
    }
}