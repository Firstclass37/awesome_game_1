using System;

namespace My_awesome_character.Core.Game.Events.Character
{
    internal class StopMovingRequest
    {
        public Guid CharacterId { get; set; }
    }
}