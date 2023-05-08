using System;

namespace My_awesome_character.Core.Game.Events
{
    public class MovementEvent
    {
        public int MovementId { get; set; }

        public int ObjectId { get; set; }

        public Type ObjectType { get; set; }

        public MapCell To { get; set; }
    }
}