using Godot;
using System.Collections.Generic;
using System.Linq;

namespace My_awesome_character.Entities
{
    public partial class Home: Node2D
    {
        public int Id { get; set; }

        public double LastFireTime { get; set; }

        public double SpawnEverySecond { get; set; }

        public MapCell[] Cells { get; set; }

        public MapCell SpawnCell { get; set; }
    }
}