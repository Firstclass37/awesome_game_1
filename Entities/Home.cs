using Godot;
using My_awesome_character.Core.Constatns;

namespace My_awesome_character.Entities
{
    public partial class Home: Node2D
    {
        public int Id { get; set; }

        public BuildingTypes BuildingType { get; set; }
    }
}