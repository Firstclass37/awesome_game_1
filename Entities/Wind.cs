using Godot;
using My_awesome_character.Core.Game;
using My_awesome_character.Core.Game.Constants;
using System;

namespace My_awesome_character.Entities
{
    public partial class Wind: Node
    {
        public Guid Id { get; set; }

        public CoordianteUI[] Area { get; set; }

        public DirectionUI DirectionTo { get; set; }

        public void AddDirection(Winddirection direction)
        {
            this.AddChild(direction);
        }
    }
}