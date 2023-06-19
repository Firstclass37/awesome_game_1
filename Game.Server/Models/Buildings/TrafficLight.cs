using Game.Server.Models.Constants;
using Game.Server.Models.Maps;

namespace Game.Server.Models.Buildings
{
    internal class TrafficLight : IBuilding
    {
        public int Id { get; internal set; }

        public BuildingTypes BuildingType { get; internal set; }

        public Coordiante RootCell { get; internal set; }

        public Coordiante[] Cells { get;  internal set; }

        public Dictionary<Direction, Coordiante> Tracking { get; internal set; }

        public Dictionary<Direction, int> Sizes {  get; internal set; }

        public Dictionary<Direction, int> CurrentValues { get; internal set; }

        public HashSet<int> AlreadySkipped { get; internal set; }
    }
}