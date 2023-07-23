using System;

namespace My_awesome_character.Core.Game.Events
{
    public class MovementCharacterPathEvent
    {
        public Guid CharacterId { get; set; }

        public MapCell[] Path { get; set; }
    }
}