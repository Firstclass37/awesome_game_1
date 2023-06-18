using My_awesome_character.Core.Game.Constants;
using System.Collections.Generic;

namespace My_awesome_character.Core.Game.Models
{
    internal class TrafficLightModel
    {
        public int Id { get; set; }

        public Coordiante Position { get; set; }

        public Dictionary<Direction, Coordiante> Tracking { get; set; }

        public Dictionary<Direction, int> Sizes { get; set; }

        public Dictionary<Direction, int> CurrentValues { get; set; }

        public HashSet<int> AlreadySkipped { get; set; }
    }
}