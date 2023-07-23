using My_awesome_character.Core.Game.Constants;
using System;
using System.Collections.Generic;

namespace My_awesome_character.Core.Game.Models
{
    internal class TrafficLightModel
    {
        public int Id { get; set; }

        public CoordianteUI Position { get; set; }

        public Dictionary<Direction, CoordianteUI> Tracking { get; set; }

        public Dictionary<Direction, int> Sizes { get; set; }

        public Dictionary<Direction, int> CurrentValues { get; set; }

        public HashSet<Guid> AlreadySkipped { get; set; }
    }
}